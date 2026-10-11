// Post-process imagegen doorway drafts into fixed-aperture RGBA game overlays.
// Usage: swift Tools/OfficeDoorwayAssets.swift left|right draft.png output.png
// Add base.png preview.png to also render a verification composition.
// Optionally add character-cutout.png last to put a positioned character over
// the same empty-light draft before applying the aperture mask.
// Add --preserve-character-lighting after it for cutouts already shadow-lit.
import Foundation
import CoreGraphics
import ImageIO
import UniformTypeIdentifiers

let width = 1672
let height = 941
let args = CommandLine.arguments
guard [4, 6, 7, 8].contains(args.count), ["left", "right"].contains(args[1]),
      args.count != 8 || args[7] == "--preserve-character-lighting" else {
    fatalError("Usage: left|right draft.png output.png [base.png preview.png [character-cutout.png [--preserve-character-lighting]]]")
}
let points: [CGPoint] = args[1] == "left"
    ? [CGPoint(x: 123, y: 65), CGPoint(x: 316, y: 170), CGPoint(x: 316, y: 697), CGPoint(x: 123, y: 746)]
    : [CGPoint(x: 1351, y: 176), CGPoint(x: 1551, y: 72), CGPoint(x: 1551, y: 742), CGPoint(x: 1351, y: 702)]
let aperture = CGMutablePath()
aperture.addLines(between: points)
aperture.closeSubpath()

func read(_ path: String) -> CGImage {
    guard let source = CGImageSourceCreateWithURL(URL(fileURLWithPath: path) as CFURL, nil),
          let image = CGImageSourceCreateImageAtIndex(source, 0, nil) else {
        fatalError("Cannot read image: \(path)")
    }
    return image
}

let colorSpace = CGColorSpace(name: CGColorSpace.sRGB)!
let info = CGImageAlphaInfo.premultipliedLast.rawValue | CGBitmapInfo.byteOrder32Big.rawValue
func pixels(_ image: CGImage, canvasWidth: Int = width, canvasHeight: Int = height) -> [UInt8] {
    var data = [UInt8](repeating: 0, count: canvasWidth * canvasHeight * 4)
    data.withUnsafeMutableBytes { bytes in
        let context = CGContext(data: bytes.baseAddress, width: canvasWidth, height: canvasHeight,
            bitsPerComponent: 8, bytesPerRow: canvasWidth * 4, space: colorSpace, bitmapInfo: info)!
        context.interpolationQuality = .high
        context.draw(image, in: CGRect(x: 0, y: 0, width: canvasWidth, height: canvasHeight))
    }
    return data
}

func write(_ data: [UInt8], _ path: String) {
    let provider = CGDataProvider(data: Data(data) as CFData)!
    let image = CGImage(width: width, height: height, bitsPerComponent: 8, bitsPerPixel: 32,
        bytesPerRow: width * 4, space: colorSpace, bitmapInfo: CGBitmapInfo(rawValue: info),
        provider: provider, decode: nil, shouldInterpolate: true, intent: .defaultIntent)!
    let destination = CGImageDestinationCreateWithURL(URL(fileURLWithPath: path) as CFURL,
        UTType.png.identifier as CFString, 1, nil)!
    CGImageDestinationAddImage(destination, image, nil)
    precondition(CGImageDestinationFinalize(destination), "PNG export failed")
}

let draft = read(args[2])
var overlay = pixels(draft)
if args.count >= 7 {
    let character = read(args[6])
    let sourceWidth = character.width
    let sourceHeight = character.height
    let characterPixels = pixels(character, canvasWidth: sourceWidth, canvasHeight: sourceHeight)
    var minX = sourceWidth, minY = sourceHeight, maxX = -1, maxY = -1
    for y in 0..<sourceHeight {
        for x in 0..<sourceWidth where characterPixels[(y * sourceWidth + x) * 4 + 3] > 8 {
            minX = min(minX, x); maxX = max(maxX, x)
            minY = min(minY, y); maxY = max(maxY, y)
        }
    }
    precondition(maxX >= minX && maxY >= minY, "Character cutout is empty")
    let normalizedProvider = CGDataProvider(data: Data(characterPixels) as CFData)!
    let normalizedImage = CGImage(width: sourceWidth, height: sourceHeight, bitsPerComponent: 8,
        bitsPerPixel: 32, bytesPerRow: sourceWidth * 4, space: colorSpace,
        bitmapInfo: CGBitmapInfo(rawValue: info), provider: normalizedProvider,
        decode: nil, shouldInterpolate: true, intent: .defaultIntent)!
    let silhouette = normalizedImage.cropping(to: CGRect(x: minX, y: minY,
        width: maxX - minX + 1, height: maxY - minY + 1))!
    // Pin the generator's alpha silhouette to the approved doorway placement.
    let target = args[1] == "left"
        ? CGRect(x: 123, y: 207, width: 191, height: 525)
        : CGRect(x: 1371, y: 208, width: 180, height: 523)
    // Uniformly size the new portraits. Any shoulders beyond the opening are
    // naturally occluded by the fixed jamb, rather than squeezing their build.
    let fittedWidth = target.height * CGFloat(silhouette.width) / CGFloat(silhouette.height)
    let fitted = CGRect(x: target.midX - fittedWidth / 2, y: target.minY,
        width: fittedWidth, height: target.height)
    var positioned = [UInt8](repeating: 0, count: width * height * 4)
    positioned.withUnsafeMutableBytes { bytes in
        let context = CGContext(data: bytes.baseAddress, width: width, height: height,
            bitsPerComponent: 8, bytesPerRow: width * 4, space: colorSpace, bitmapInfo: info)!
        context.interpolationQuality = .high
        context.draw(silhouette, in: CGRect(x: fitted.minX,
            y: CGFloat(height) - fitted.maxY, width: fitted.width, height: fitted.height))
    }
    let grade = args.count == 8 ? [1.0, 1.0, 1.0, 1.0]
        : (args[1] == "left" ? [0.85, 0.85, 0.80, 1.0] : [0.75, 0.75, 0.72, 1.0])
    for index in stride(from: 0, to: overlay.count, by: 4) {
        let alpha = Double(positioned[index + 3]) / 255.0
        if alpha == 0 { continue }
        // Extraction can brighten the subject; match the dim doorway lighting.
        for channel in 0..<4 {
            overlay[index + channel] = UInt8(min(255.0,
                Double(positioned[index + channel]) * grade[channel] + Double(overlay[index + channel]) * (1.0 - alpha)).rounded())
        }
    }
    print("Character normalized from [\(minX),\(minY)]-[\(maxX),\(maxY)] into \(fitted)")
}
var visiblePixels = 0
for y in 0..<height {
    for x in 0..<width {
        let index = (y * width + x) * 4
        // Work in PNG's top-left coordinates. Leave all jambs/thresholds on the
        // base; the one-pixel boundary is antialiased strictly inside the aperture.
        let point = CGPoint(x: Double(x) + 0.5, y: Double(y) + 0.5)
        var coverage = 0.0
        if aperture.contains(point) {
            var distance = Double.greatestFiniteMagnitude
            for edge in 0..<points.count {
                let a = points[edge]
                let b = points[(edge + 1) % points.count]
                let dx = b.x - a.x
                let dy = b.y - a.y
                let t = max(0.0, min(1.0, ((point.x - a.x) * dx + (point.y - a.y) * dy) / (dx * dx + dy * dy)))
                distance = min(distance, hypot(point.x - (a.x + t * dx), point.y - (a.y + t * dy)))
            }
            coverage = min(1.0, distance)
        }
        for channel in 0..<4 {
            overlay[index + channel] = UInt8((Double(overlay[index + channel]) * coverage).rounded())
        }
        if overlay[index + 3] > 0 { visiblePixels += 1 }
    }
}
write(overlay, args[3])
if args.count >= 6 {
    let master = read(args[4])
    precondition(master.width == width && master.height == height, "Master must be 1672x941")
    let original = pixels(master)
    var composed = original
    for index in stride(from: 0, to: composed.count, by: 4) {
        let alpha = Double(overlay[index + 3]) / 255.0
        for channel in 0..<3 {
            composed[index + channel] = UInt8(min(255.0,
                Double(overlay[index + channel]) + Double(original[index + channel]) * (1.0 - alpha)).rounded())
        }
    }
    write(composed, args[5])
}
print("\(args[1]): \(width)x\(height), \(visiblePixels) aperture pixels -> \(args[3])")
