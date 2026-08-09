# Bolojan camera composite generation

The two integrated camera feeds were created with the built-in image-generation
edit mode. Each original camera feed was the edit target and
`ilie bolojan.jpeg` was the identity reference.

## CAM 01

```text
Use case: compositing
Asset type: Unity game surveillance-camera feed, full-frame 16:9 replacement image
Input images: Image 1 is the CAM 01 dairy-aisle edit target and must remain the compositional anchor. Image 2 is the sole identity reference for Ilie Bolojan.
Primary request: Insert the man from Image 2 naturally into Image 1 as if he was physically present when this exact security-camera frame was recorded. He is an average-height adult in the same dark navy suit, white shirt, and dark tie. Preserve his recognizable bald head, facial structure, heavy eyebrows, and identity.
Scene/backdrop: Preserve the same dim supermarket dairy aisle, central freezer, wall refrigerators, shelves, pallets, signage, high ceiling, floor, grime, and security-camera angle from Image 1 as closely as practical.
Composition/framing: Keep the exact wide framing and perspective of Image 1. Place Bolojan at believable human scale in the open midground aisle on the right side, just behind the rear-right area of the central freezer, fully grounded on the tiled floor. His whole figure should be readable but occupy only about one quarter to one third of the frame height. Pose him standing unnervingly still, mostly front-facing toward the camera, arms relaxed at his sides, head subtly tilted upward toward the high camera. Do not make him the dominant foreground subject.
Lighting/mood: Match the weak overhead fluorescent lighting, deep murky shadows, green-brown supermarket color cast, vignette, low contrast, grime, surveillance softness, and subtle sensor noise of Image 1. His suit and skin must receive the same direction and intensity of light, with a realistic soft contact shadow on the floor.
Materials/textures: realistic worn suit cloth, real skin texture, dirty reflective floor, natural shadow integration. Allow believable partial occlusion by nearby shelving or freezer edges if the geometry calls for it.
Constraints: Change only what is necessary to integrate Bolojan. Preserve Image 1's room layout, camera angle, framing, aspect ratio, existing objects, and existing environmental text/signage as closely as practical. The result must be one complete opaque camera-feed image, not a cutout and not a person on a transparent layer. Bolojan must look baked into the same photographed scene, with matched perspective, depth, blur, color cast, grain, occlusion, and shadows. No additional people, no new objects, no UI, no captions, no tax text, no watermark, no border, no new logos.
Avoid: studio portrait lighting, clean sharp cutout edges, floating feet, oversized person, foreground close-up, brightly lit face, altered store layout, cinematic spotlight, poster-like composition.
```

## CAM 07

```text
Use case: compositing
Asset type: Unity game surveillance-camera feed, full-frame 16:9 replacement image
Input images: Image 1 is the CAM 07 stockroom edit target and must remain the compositional anchor. Image 2 is the sole identity reference for Ilie Bolojan.
Primary request: Insert the man from Image 2 naturally into Image 1 as if he was physically present when this exact security-camera frame was recorded. He is an average-height adult in the same dark navy suit, white shirt, and dark tie. Preserve his recognizable bald head, facial structure, heavy eyebrows, and identity.
Scene/backdrop: Preserve the same dim stockroom, concrete floor, central aisle, metal shelves, stacked cartons, pallet jack, rear exit door and lamp, wanted-cat poster, ceiling, grime, and high security-camera viewpoint from Image 1 as closely as practical.
Composition/framing: Keep the exact wide framing and perspective of Image 1. Place Bolojan at believable human scale in the central midground aisle, slightly left of the rear exit door and partly emerging from behind the inner edge of the left-side shelving. His full figure should remain mostly readable, with a shelf edge or cartons naturally occluding a small part of one side of his body. He should occupy only about one quarter to one third of the frame height. Give him a subtly different pose from a rigid front-facing stance: a quiet three-quarter turn into the aisle, one shoulder leading, one hand loosely near the shelf, his face turned upward to stare toward the high camera. Do not make him the dominant foreground subject.
Lighting/mood: Match the sparse cold fluorescent ceiling light, very deep stockroom shadows, brown-green dirty color cast, vignette, low contrast, surveillance softness, and subtle sensor noise of Image 1. Keep most of him in the same low light; his face must not be brightly spotlighted. Add realistic contact shadow and ambient occlusion where his shoes meet the concrete and where shelving blocks light.
Materials/textures: realistic worn suit cloth, real skin texture, dusty concrete, cardboard, natural shadow integration and environmental occlusion.
Constraints: Change only what is necessary to integrate Bolojan. Preserve Image 1's room layout, camera angle, framing, aspect ratio, existing objects, poster, and existing environmental text/signage as closely as practical. The result must be one complete opaque camera-feed image, not a cutout and not a person on a transparent layer. Bolojan must look baked into the same photographed scene, with matched perspective, depth, blur, color cast, grain, occlusion, and shadows. No additional people, no new objects, no UI, no captions, no tax text, no watermark, no border, no new logos.
Avoid: studio portrait lighting, clean sharp cutout edges, floating feet, oversized person, foreground close-up, brightly lit face, altered stockroom layout, cinematic spotlight, poster-like composition.
```

## Jumpscare

The full-screen jumpscare was created with the built-in image-generation edit
mode. The previous jumpscare supplied the expression and pose, `ilie
bolojan.jpeg` supplied the identity anchor, and `OfficeBaseGenerated_v3.png`
supplied the office environment.

```text
Use case: identity-preserve
Asset type: exact-resolution 16:9 Unity horror-game jumpscare frame
Output requirement: exactly 1672 pixels wide by 941 pixels high, full landscape frame.
Input images: Image 1 is the current jumpscare pose/expression reference. Image 2 is the identity anchor for Ilie Bolojan's recognizable face, bald head, eyebrows, nose, jaw, suit, white shirt, and dark tie. Image 3 is the visual reference for the game's grimy supermarket security office; use it only as the dark background environment.
Primary request: Make the Bolojan jumpscare substantially more frightening. Show the same recognizable suited man lunging straight toward the player from inside the dark office, staring directly into the camera with a violently alarmed, hostile expression, mouth open in a raw scream, eyes unnaturally wide with tiny sharp pupils and asymmetric cold catchlights. His face should feel inches from the lens, but keep his entire bald head, both ears, chin, neck, shoulders, suit lapels, and upper chest visible. Add restrained horizontal camera-shake ghosting at the outer edges of his head and shoulders, a slight forward perspective distortion, subtle security-camera scanlines/noise, and a faint double-image smear suggesting sudden motion.
Composition/framing: centered head-and-shoulders lunge, entire head comfortably inside frame with margin above the scalp; face occupies roughly 60–70% of frame height, large and immediate but never cropped down to only mouth/chin. Eyes sit slightly above center. Shoulders can approach the side edges. Landscape composition designed to fill a 16:9 game screen.
Lighting/mood: almost-black office behind him, recognizable only in fragments; harsh sickly overhead fluorescent rim light, deep eye sockets and cheek shadows, cold green-gray skin cast with a very subtle dirty red emergency bounce from below, strong vignette, abrupt flash-frame feeling. Photorealistic cinematic survival-horror, grimy and unsettling rather than polished studio portrait.
Constraints: preserve his identity and adult human anatomy; maintain the dark navy business suit, white shirt, and dark tie; no text because the game adds BOLOJAN TAX separately; no UI, no logos, no watermark, no border. Keep both eyes, full skull, full mouth, chin, and shoulders visible. No blood, gore, wounds, exposed bone, monster teeth, horns, fantasy creature features, zombie redesign, melting face, extra eyes, extra limbs, missing facial parts, comedic expression, fisheye-circle border, or extreme crop.
```
