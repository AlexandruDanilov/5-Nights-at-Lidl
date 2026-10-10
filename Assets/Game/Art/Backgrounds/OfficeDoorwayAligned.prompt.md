# Aligned office doorway assets

Backup before replacement: Git commit `c75e359`, pushed to `origin/main`.

Generated with the built-in image-generation tool, then resized, positioned,
and aperture-masked with `Tools/OfficeDoorwayAssets.swift` using CoreGraphics.
The approved standard office and Night 4 bottle wallpaper are unchanged.

## Runtime assets

All six files are **1672 × 941 RGBA PNG doorway-only overlays**; transparency
outside the opening is intentional. Existing paths and Unity GUIDs are retained:

- `Assets/Game/Art/Backgrounds/OfficeLightLeftGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeLightRightGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeDoorLeftGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeDoorRightGenerated_v3.png`
- `Assets/Game/Art/Characters/Cabral/Cabral_LeftDoor_Integrated.png`
- `Assets/Game/Art/Characters/Selly/Selly_RightDoor_Integrated.png`

Both layers cover the same full canvas as the static office and use
`ScaleAndCrop`. There are no side clips or separately scaled frames. Only the
opening contents change; the base owns every metal jamb, threshold, wall,
poster and desk prop. The Night 4 bottle and girls retain their original layers.

Pixel masks in top-left source-image coordinates:

- Left: `(123,65), (316,170), (316,697), (123,746)`.
- Right: `(1351,176), (1551,72), (1551,742), (1351,702)`.

Mask antialiasing is confined inward to one pixel. The character variants reuse
exactly the empty-light room draft; generated character-only alpha silhouettes
are composited over it before masking. No background fixture shifts when a
character arrives. Cabral's alpha bounds are positioned to `(123,207,191,525)`
and Selly's to `(1371,208,180,523)` (x, y, width, height), with the existing
jamb/threshold providing occlusion.

Character-only RGB grading restores dim office lighting after extraction:
Cabral `(0.85,0.85,0.80)` and Selly `(0.75,0.75,0.72)`. Alpha is unchanged.

All eleven office/base/doorway/girls/bottle textures use native non-power-of-two
size (`nPOTScale: 0`), no mipmaps, clamp, bilinear filtering and uncompressed
imports. This prevents independent resampling/aspect/compression seams.

## Exact generation prompts

# Left doorway draft prompts

Built-in image_gen; three independent master-based edits.

## Draft 1

```text
Use case: precise-object-edit
Asset type: perfectly registered fixed-camera horror game office lighting frame, LEFT LIGHT ON.
Input images: Image 1 is the sole immutable edit target and controls the exact office geometry, camera and framing. Image 2 is supporting content reference ONLY for the supermarket checkout area seen through the left aperture; do not copy its shifted office or doorway frame.
Primary request: edit Image 1 only within the dark INSIDE aperture of its left doorway to reveal the same illuminated supermarket checkout room as Image 2, with checkout counters, shopping cart, tiled floor and overhead fluorescent lamps. Do not alter the aperture itself.
Composition: same entire 1672x941 wide 16:9 frame as Image 1, same pixels and viewpoint. Left inner opening polygon in Image 1 coordinates: top-left (123,63), top-right (345,168), bottom-right (345,692), bottom-left (123,744). This polygon is the entire permitted edit area; the original metal jambs and border outside it must remain untouched. Align the checkout perspective naturally behind that exact aperture.
Constraints: ONLY contents of this left inner opening may change. Outside the polygon, preserve the master Image 1 identically: doorway frame, black edge, wall texture, ceiling, floor, threshold, right dark doorway, desk telephone, clipboard, coffee mug, receipt papers, cassette player, cabinets, posters and lighting. No moved edges, no newly introduced inner doorway frame, no crop, no camera shift, no image scale change, no local warping, no office relighting, no light spilling onto walls or floor outside the aperture. No people. No door buttons. No new text or watermark. Keep the master image appearance as a still photo.
```

## Draft 2

```text
Use case: precise-object-edit
Asset type: perfectly registered fixed-camera horror game office CLOSED LEFT DOOR frame.
Input images: Image 1 is sole immutable edit target and exact geometry/framing source. Image 2 is supporting material/design reference ONLY for the dark horizontally corrugated industrial metal shutter.
Primary request: close ONLY the left opening in Image 1 by filling its inside aperture with a lowered industrial metal security shutter like Image 2. Place the shutter behind the original unchanged metal doorway frame, precisely flush with the existing aperture. Match dark metal texture and dim office ambient light.
Composition: same entire 1672x941 wide 16:9 frame as Image 1. Permitted edit polygon at left doorway inner opening: (123,63), (345,168), (345,692), (123,744). Shutter is confined inside it with horizontal corrugations following the established perspective; do not add another inset frame.
Constraints: EVERYTHING outside the exact inner opening remains pixel-identical to Image 1: metal jambs, wall edges, ceiling, desk, floor, all props, posters, light and right dark doorway. Keep exact camera position, framing, scale, original frame boundary. No wall or threshold movement, no office relighting, no shifted phone or mug, no new frame inside aperture, no characters, no buttons, no new text, no watermark.
```

## Draft 3

```text
Use case: compositing
Asset type: perfectly registered fixed-camera horror game office LEFT LIGHT ON WITH CABRAL frame.
Input images: Image 1 is sole immutable office edit target and exact geometry/framing source. Image 2 is supporting reference for Cabral's exact face, bald head, expression, body proportions, pose and outfit, plus checkout background beyond doorway. Image 2 must not replace any office pixels outside the left inner aperture.
Primary request: reveal the illuminated checkout area ONLY through the left aperture of Image 1 and place the same approved man Cabral from Image 2 standing at the left doorway, beyond the office threshold. Preserve his identity EXACTLY: same bald head, face, trimmed moustache/goatee, knowing slight smile, blue blazer, white open-collar shirt, dark trousers, black shoes, hands clasped at waist and current broad but not obese proportions. Same pose and recognisable face, no reinterpretation. Full body including feet. He stays outside, never half-inside the office.
Composition: same entire 1672x941 wide 16:9 frame as Image 1, unchanged viewpoint. ONLY allowed edit polygon is inside the left doorway opening: top-left (123,63), top-right (345,168), bottom-right (345,692), bottom-left (123,744). Cabral stands fully within this aperture, head near (226,207), feet on checkout floor near (203,710), filling much of the aperture like Image 2 but absolutely no overlapping the master door frame or crossing threshold. Reuse Image 2 subject face and body faithfully rather than generating a different man.
Constraints: preserve Image 1 identically everywhere outside polygon, including original metal door jambs, wall edges, all office lighting, right doorway, floor, desk phone, mug, clipboard, papers, cassette player, posters and cabinets. No door frame enlargement or movement, no reframing/crop, no changed floor or props, no extra inner frame, no buttons, no additional people, no new text or watermark. The checkout room behind him matches Image 2 and fits the exact unchanged master aperture.
```



## Right light

```text
Use case: precise-object-edit / lighting-weather. Asset type: fixed-camera photorealistic Unity horror office state background. Image 1 is the immutable full office master edit target; Image 2 is supporting reference only for the employees break room visible beyond the RIGHT doorway. Remake ONLY the interior of the right open doorway into that same shabby staff room, with lockers, break table/chairs, microwave and worn yellow-green fluorescent illumination. Preserve the exact camera, perspective, full 1672x941 landscape canvas and every metal doorway-frame edge from Image 1. The INNER aperture vertices on this canvas are (1324,167),(1550,64),(1550,744),(1324,692). Fit room content entirely BEHIND this existing aperture. Do not paint a new door frame or change its geometry. The left doorway stays unlit black. Outside that inner opening retain the exact office from Image 1: posters, text including meriti sa fii surprins, ceiling lamp, cabinet, fan, telephone/cord, mug, clipboard, papers, desk, floor, walls and lighting remain perfectly registered and unchanged. No people, buttons, spray bottle, UI, border or watermark. This is a toggled animation frame; no camera movement, lens change, architecture shift, prop movement or extra framing. Existing dark distressed photographic game appearance, nearly same old employee room look. Return one landscape full-frame image.
```

## Right shutter

```text
Use case: precise-object-edit. Asset type: fixed-camera photorealistic Unity horror office closed-RIGHT-security-door state. Image 1 is the immutable master edit target. Image 2 is a supporting reference for the existing RIGHT horizontal steel shutter appearance only. Lower a dark grimy horizontal steel shutter entirely across the inner RIGHT door aperture, following its exact perspective. Preserve Image 1's outer metal frame, casing, threshold and all surrounding architecture. Full canvas 1672x941, exactly same viewpoint/crop/lens. Inner aperture vertices (1324,167),(1550,64),(1550,744),(1324,692), no rendered shutter or new door frame outside this polygon. Maintain outer-frame pixels, wall textures, posters and exact text, fan, cabinetry, floor, ceiling lighting and ALL foreground desk/telephone/cord/mug/clipboard/papers unchanged from Image 1. Left opening remains black and open. Match the old dark worn metal shutter texture and fine restrained reflections. No side light, person, buttons, bottle, UI, watermarks or border. One landscape full-frame image.
```

## Right Selly draft

```text
Use case: identity-preserve / compositing. Asset type: fixed-camera photorealistic Unity horror office RIGHT light-on threat frame. Image 1 is immutable office master edit target. Image 2 is supporting character identity/pose/outfit and employee-break-room content reference, NOT replacement office framing. Recreate the same Selly from Image 2 standing just outside the right threshold, full head/body/shoes within the right door aperture. Retain exactly his face, swept dark hair, navy open overshirt, white T-shirt, dark trousers, light shoes, slightly leaning stern confrontational pose and size. Likeness must remain identical to approved Image 2. Right doorway has same dirty yellow-green fluorescent employees room, lockers, table, folding chair, microwave behind him. Keep him behind the doorway plane, not inside office. The exact INNER right aperture on master 1672x941 canvas is vertices (1324,167),(1550,64),(1550,744),(1324,692); its metal frame remains Image 1's unchanged existing frame, do not repaint/reposition or add frame. Left opening black. All office geometry, viewpoint, crop/lens, desks/phone/cord/papers/clipboard/mug/fan/cabinet/posters/exact text/floor/walls/ceiling lamp outside inner right aperture unchanged from Image 1. Subdued photorealistic horror-game photographic grain and same darkness as approved office. No other people, buttons, spray bottle, UI, watermarks or border. One full landscape frame.
```

## Cabral character-only extraction

# Cabral cutout prompt

Built-in image_gen, transparent_background=true.

```text
Use case: background-extraction
Asset type: exact-position transparent full-canvas game character overlay.
Input image 1: edit target, the approved full-width office frame with Cabral at the left doorway.
Primary request: remove every background pixel and retain ONLY Cabral as a clean cutout on a genuinely transparent alpha background. Do not redraw, reinterpret, move or resize him. Preserve his exact approved face, bald head, expression, blue blazer, white open collar shirt, clasped hands, dark trousers and black shoes, body build and full silhouette from the input.
Composition: keep the input's entire 1672x941 canvas size and framing with huge transparent empty region to the right. Cabral remains in exactly his original left-side pixel position and scale: top of head near (224,205), feet near (203,725); full-body silhouette approximately x=123..312 and y=205..730. No tight bounding-box crop, no centering, no enlarged portrait. All canvas outside his silhouette is transparent alpha=0, including gaps between his legs and arms.
Constraints: preserve the character's precise original silhouette and details, without changing pose, identity, expression, proportions, lighting or clothes. Delete ALL room, doorway frame, fluorescent lights, checkout fixtures, office wall, floor, desk and shadows. No cast shadow, no halo, no white or black matte, no rendered checkerboard, no background or ground plane, no text, no watermark. This must be a transparent PNG overlay, with Cabral placed exactly where he is in the source so it can be composited directly over a 1672x941 matching room.
```



## Selly character-only extraction

```text
Use case: background-extraction / identity-preserve. Image 1 is the edit target. Remove EVERY pixel of environment, office, room, doors, furniture and backgrounds from this full office image, retaining ONLY the person Selly standing on its far RIGHT. Output genuinely transparent RGBA full-canvas 1672x941, preserving his exact full body, pose, facial likeness, hair, expression, navy overshirt/white T-shirt/dark pants/shoes, illumination and current size and coordinates. His head is around (1445,208), torso around (1450,400), feet near (1490,716). Keep original large transparent padding on left, top, and bottom; no tight crop or recentering. Preserve anti-aliased body edges. No background, room, frame, wall, objects, floor, contact shadow, text, border or extra people. The result is a positioned full-frame character overlay to composite with a fixed office. Do not repaint or beautify face or change body.
```

