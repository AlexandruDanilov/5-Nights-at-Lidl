# Selly asset generation notes

The live doorway frame has been redrawn using the approved CAM 04/05/07/08
likeness, with a stern stare, dim overhead shadows and his gold left-wrist watch.
It is now a 1672×941 RGBA aperture overlay over the same dim employees room as
the empty-light state. See `../../Backgrounds/OfficeDoorwayCreepy.prompt.md`
for current prompts. The following records the original asset generation.

Generated with the built-in image-generation tool in identity-preserving compositing mode. Final assets are 1672 x 941 RGB PNGs and follow the planned route:

`CAM 04 -> CAM 05 -> CAM 07 -> CAM 08 -> right office door -> jumpscare`

## Identity references

- `andrei selaru selly.jpeg`
- `selly 2.jpeg`
- `selly 3.jpeg` (used for open-mouth expression anatomy in the jumpscare)
- `selly 4.jpeg`
- `selly 5.jpg`

The camera and doorway frames consistently use Selly's navy open overshirt, plain white T-shirt, dark trousers, and light natural stubble. The likeness specification emphasizes his broad rounded face, full cheeks, softly rounded jaw, thick straight brows, brown almond-shaped eyes, broad rounded nose, fuller lower lip, and long swept curtain-like fringe.

## Final assets and prompts

### `Selly_Cam04_Integrated.png`

Base: `Assets/Game/Art/CameraFeeds/Cam04_Detergents.png`

```text
Use case: precise-object-edit and identity-preserve.

Image 1 is the CAM 04 draft edit target. Images 2–5 are identity references for the same person, Selly.

Primary request: change only Selly's scale and placement. He is currently far too large and close. Scale his entire body down substantially and move him deeper into the left-center aisle so he occupies approximately 45 percent of the total image height from head to lower legs. Show him from at least mid-shin upward, with clear floor space beneath/around him. He should be visibly closer than a distant background figure but remain a midground security-camera subject, not dominate the frame. Preserve his current broad rounded Selly likeness, face, expression, navy open overshirt, white T-shirt, dark trousers, and natural anatomy.

Watch: preserve exactly one conspicuous oversized polished yellow-gold luxury chronograph on his LEFT wrist, with a large round black dial and gold bracelet. Keep his left wrist slightly exposed and the watch visibly readable with a controlled gold glint. No watch on the right wrist and no other jewelry.

Integration: re-ground his shoes naturally on the wet aisle floor and match the dark fluorescent perspective, contact shadow, ambient occlusion, grain, and lens softness at the new scale.

Constraints: preserve the CAM 04 background, camera framing, shelves, products, spills, posters, logos, text, lighting, and every unrelated object exactly. Do not redesign Selly or the watch. No extra people, props, text, watermark, border, or UI.
```

### `Selly_Cam05_Integrated.png`

Base: `Assets/Game/Art/CameraFeeds/Cam05_IceCream.png`

```text
Use case: compositing
Asset type: full-frame 16:9 photorealistic surveillance-camera game feed.

Input images: Image 1 is the immutable CAM 05 Ice Cream edit target. Images 2–5 are photographs of the same real person, Selly, and together are the identity reference set.

Primary request: insert a closer, recognizable Selly into Image 1. Place him in the midground walkway on the right side of the central freezer island, between the island and the right-hand shelves, at approximately 48 percent of the image height. Show his full head and body down to at least his lower legs. He is slightly closer than his CAM 04 state but still physically inside the store, with shelving and the freezer establishing scale. Turn his shoulders subtly three-quarter while his face stares directly at the security camera. His posture is rigid, still, and unnervingly calm. Let his left forearm hang a little forward so the wrist is unobstructed.

Identity lock: reproduce Selly specifically: medium warm skin; broad rounded face and full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown slightly almond-shaped eyes; broad rounded nose; full lips with fuller lower lip; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part; light natural stubble only. Do not make him narrow-faced, angular, hollow-cheeked, or heavily bearded. He wears the navy open overshirt over a plain white T-shirt and dark trousers.

Watch: place exactly one conspicuous oversized luxury watch snugly on Selly's LEFT wrist, identical to CAM 04: polished yellow-gold metal bracelet and case, large round black dial, substantial premium chronograph styling, tasteful bright gold highlights, no visible brand name or logo. The watch must be clearly readable at camera distance. No watch on the right wrist, bracelets, or rings.

Lighting/mood: match the magenta freezer light, dirty CCTV contrast, dim aisle shadows, grain, lens softness, exact perspective, contact shadow, and ambient occlusion. Pink light catches one edge of his real rounded face and navy overshirt; a small warm-magenta highlight reveals the gold watch. Ground his feet naturally and use the freezer island/shelves for realistic depth and slight occlusion; never pasted-on.

Constraints: change only Selly, his watch, and minimal integration light/shadow. Preserve Image 1's exact framing, camera, architecture, freezers, center island, shelves, products, poster, all existing text/logos, floor reflections, and every unrelated object. Remove microphones and reference backgrounds. No extra people, props, captions, watermark, border, UI, or gore.
```

### `Selly_Cam07_Integrated.png`

Base: `Assets/Game/Art/CameraFeeds/Cam07_Stockroom.png`

```text
Use case: compositing
Asset type: full-frame 16:9 photorealistic surveillance-camera game feed.

Input images: Image 1 is the immutable CAM 07 Stockroom edit target. Images 2–5 are photographs of the same real person, Selly, and together are the identity reference set.

Primary request: insert a closer and clearly recognizable Selly into Image 1 while keeping CAM 07 on the original route. Place him in the right midground of the central walking path, physically emerging from behind the existing right-hand carton stacks. Show him approximately from mid-thigh upward at about 55 to 60 percent of the image height. The cartons naturally occlude the outside of one leg and a small part of his lower torso. He is closer and more threatening than CAM 05 without filling the whole screen. Turn his shoulders three-quarter, lean slightly around the boxes, and stare directly into the camera with a restrained closed-mouth expression. Keep both arms natural; angle his left wrist slightly toward the camera.

Identity lock: reproduce Selly specifically: medium warm skin; broad rounded face with full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown slightly almond-shaped eyes; broad rounded nose; full lips with fuller lower lip; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part; light natural stubble only. Do not make him narrow-faced, gaunt, angular, hollow-cheeked, generic, or heavily bearded. He wears the navy open overshirt over a plain white T-shirt and dark trousers.

Watch: place exactly one conspicuous oversized luxury watch snugly on Selly's LEFT wrist, identical to CAM 04 and CAM 05: polished yellow-gold metal bracelet and case, large round black dial, substantial premium chronograph styling, tasteful controlled gold highlights, no visible brand or logo. Make the watch clearly visible despite the low light. No watch on the other wrist, bracelets, or rings.

Lighting/mood: match the stockroom's dim overhead fluorescent light, deep cold shadows, dirty concrete, surveillance grain, lens softness, exact perspective, contact shadow, and ambient occlusion. Let the overhead light weakly define his recognizable rounded face, hair, and one shoulder, plus a small believable glint from the gold watch. Integrate him behind the carton edges so he appears physically present, never pasted on.

Constraints: change only Selly, his watch, and minimal integration light/shadow. Preserve Image 1's exact framing, camera, room geometry, door, shelves, cartons, pallet jack, Larry poster, existing text/logos, floor reflections, darkness, and every unrelated object. Remove microphones and reference-photo backgrounds. No extra people, props, captions, watermark, border, UI, or gore.
```

Final full-body correction pass:

```text
Use case: precise-object-edit and identity-preserve.

Image 1 is the current CAM 07 Selly frame and edit target. Images 2–5 are identity references for Selly.

Primary request: change Selly's scale and placement so his FULL BODY is visible. The current cartons hide his legs. Scale him down moderately and move him a little left into the open central aisle, immediately beside the right-hand carton stack rather than behind it. Show his entire head, torso, both arms, both complete legs, and both shoes from head to toe. Leave visible floor beneath and around both shoes. Selly should occupy approximately 58 to 62 percent of the total image height: still closer and more threatening than CAM 05, but fully contained in the frame. At most, let one carton edge overlap a tiny outer edge of one trouser leg; do not cover his knees, shins, ankles, or shoes.

Identity and pose: preserve Selly's recognizable broad rounded face, full cheeks, rounded jaw, thick brows, brown eyes, broad nose, full lips, dark curtain-like swept hair, light stubble, navy open overshirt, white T-shirt, dark trousers, closed-mouth stare, and slightly forward three-quarter posture. Keep his gaze directly at the camera and both arms hanging naturally.

Watch: preserve exactly one conspicuous oversized polished yellow-gold luxury chronograph on his LEFT wrist, with a large round black dial and gold metal bracelet. Keep the left wrist exposed and the watch clearly readable with a controlled highlight. No right-wrist watch or other jewelry.

Integration: re-ground both shoes naturally on the wet stockroom floor and match the existing overhead fluorescent perspective, contact shadow, ambient occlusion, cold darkness, grain, and lens softness at the new scale.

Constraints: preserve the CAM 07 background, exact framing, camera, shelves, cartons, pallet jack, door, Larry poster, logos, text, floor reflections, and every unrelated object. Change only Selly and minimal integration light/shadow. No extra people, props, captions, watermark, border, UI, or gore.
```

### `Selly_Cam08_Integrated.png`

Base: `Assets/Game/Art/CameraFeeds/Cam08_EmployeeRoom.png`

```text
Use case: high-fidelity identity-preserving photo compositing for a horror game surveillance feed.

Image 1 is the immutable CAM 08 Employee Room base scene and edit target. Images 2–5 are photographs of the same real person, Selly, and together form the identity reference set. Reproduce Selly specifically and consistently: Romanian young adult male; medium warm skin; broad rounded face with full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown slightly almond-shaped eyes; broad rounded nose; full lips with a fuller lower lip; subtle smile-line structure; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part. Keep only light natural stubble, without a pronounced moustache or beard. Do not make his face narrow, gaunt, sharply angular, hollow-cheeked, or generic.

Insert Selly in the open doorway at the far right edge of Image 1, the doorway beneath the EMPLOYEE ROOM sign that leads toward the office. He is the closest camera-stage so far: stand him just inside that doorway, roughly knees-up or three-quarter body visible, partly occluded by the right doorframe. Turn his shoulders toward the exit as if he was leaving, but twist his recognizable face back to stare directly into the security camera. Add a slightly deeper head tilt and subtly hunched shoulders, motionless and threatening. He wears the navy open overshirt/jacket and plain white T-shirt from Image 2, dark trousers, empty hands. Remove all microphones, text overlays, studios, other people, and reference-photo backgrounds.

Match the employee room's sick dim fluorescent lighting, dirty blue-yellow walls, surveillance grain, lens softness, exact perspective, contact shadow, and ambient occlusion. Let cold corridor light catch the edge of his face and the long front strands of his hair while most of his body stays in doorway shadow. His actual broad rounded facial proportions must remain recognizable. Integrate him behind the doorway frame, never pasted on top.

Change only Selly and the minimal immediate light/shadow required to integrate him. Preserve Image 1's exact 1672x941 framing, camera position, room geometry, lockers, table, chairs, food containers, refrigerator, microwave, bulletin board, open stockroom doorway, right doorway, signage, existing text/logos, stains, and every unrelated object. No extra people, new props, captions, watermark, border, or UI.
```

Final interior-placement revision:

```text
Use case: compositing
Asset type: full-frame 16:9 photorealistic surveillance-camera game feed.

Input images: Image 1 is the immutable CAM 08 Employee Room edit target. Images 2–5 are photographs of the same real person, Selly, and together are the identity reference set.

Gameplay requirement: CAM 08 represents Selly still inside the employee room. He must be clearly and substantially AWAY from the office doorway at the far right edge. When Selly later reaches the right office door he disappears from all cameras, so do not place him in, beside, leaning through, approaching, or overlapping the right doorway. Leave the entire far-right doorway, its threshold, and its visible corridor completely empty.

Primary request: place Selly well inside the room on the LEFT side of the dirty central table, in the open floor area in front of the lockers. Position him between the lockers and the nearest left table chair, with at least several body-widths of visible room separating him from the far-right office doorway. Show his entire body from head to both shoes, about 58 percent of the image height, with visible floor beneath both feet. He is closer and more threatening than CAM 07 but still fully contained in the feed. Face his body slightly three-quarter toward the table while turning his head and eyes directly toward the security camera. Rigid, motionless, unnervingly calm closed-mouth stare.

Identity lock: reproduce Selly specifically: medium warm skin; broad rounded face and full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown slightly almond-shaped eyes; broad rounded nose; full lips with fuller lower lip; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part; light natural stubble only. Do not make him narrow-faced, angular, hollow-cheeked, generic, or heavily bearded. He wears the navy open overshirt over a plain white T-shirt, dark trousers, and simple dark shoes.

Watch continuity: place exactly one conspicuous oversized polished yellow-gold luxury chronograph on Selly's LEFT wrist, matching CAM 04, CAM 05, and CAM 07: gold metal bracelet and case, large round black dial, premium chronograph styling, controlled metallic highlight, no visible brand or logo. Keep the left wrist exposed and readable. No other jewelry and no right-wrist watch.

Lighting/mood: match the sick dim fluorescent employee-room lighting, dirty blue-yellow walls, surveillance grain, lens softness, exact perspective, contact shadows, and ambient occlusion. Integrate both shoes on the stained floor and let the overhead light reveal his recognizable rounded face and a small gold watch glint. Use nearby lockers/table/chair only for depth; do not hide his legs.

Constraints: change only Selly, his watch, and minimal integration light/shadow. Preserve Image 1's exact framing, camera, lockers, table, chairs, food containers, refrigerator, microwave, bulletin board, central stockroom doorway, far-right office doorway, signage, all text/logos, stains, and every unrelated object. The right doorway must remain empty. Remove microphones and reference-photo backgrounds. No extra people, props, captions, watermark, border, UI, or gore.
```

### `Selly_RightDoor_Integrated.png`

Base: `Assets/Game/Art/Backgrounds/OfficeLightRightGenerated_v3.png`

```text
Use case: high-fidelity identity-preserving photo compositing for a horror game's office doorway state.

Image 1 is the immutable office scene with the RIGHT doorway light on and is the edit target. Images 2–5 are photographs of the same real person, Selly, and together form the identity reference set. Reproduce Selly specifically and consistently: Romanian young adult male; medium warm skin; broad rounded face with full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown slightly almond-shaped eyes; broad rounded nose; full lips with a fuller lower lip; subtle smile-line structure; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part. Keep only light natural stubble, without a pronounced moustache or beard. Do not make his face narrow, gaunt, angular, hollow-cheeked, or generic.

Insert Selly only inside the brightly lit RIGHT office doorway on the far right of Image 1. He has reached the player: stand him just across the threshold, large and close but fully contained within the doorway, with his entire head, neck, shoulders, torso, hands, and legs down to at least the knees visible. Slightly hunch his shoulders and lean his upper body into the office while staring directly at the player camera with an unnervingly calm, closed-mouth expression and a modest head tilt. He wears the navy open overshirt/jacket and plain white T-shirt from Image 2, dark trousers, empty hands. Remove all microphones, text overlays, studios, other people, and reference-photo backgrounds.

Match the harsh warm fluorescent doorway light, deep office shadows, dirty realism, subtle sensor grain, lens softness, exact perspective, contact shadow, and ambient occlusion. Light the right edge of his recognizable broad rounded face and hair while retaining threatening shadow across the other side. His feet/body must be naturally behind the metal threshold and doorway frame, never pasted over it.

Change only Selly and the minimal immediate shadow/light in the right doorway required to integrate him. Preserve Image 1's exact 1672x941 framing, camera, crop, left dark doorway, right doorway architecture, visible employee room, walls, posters and all exact poster text, cabinet, fan, telephone, clipboard, pen, papers, mug, recorder, desk, and every unrelated object pixel-stable. No door buttons added into the image, no extra people, new props, captions, watermark, border, or UI.
```

### `Selly_Jumpscare.png`

Base: `Assets/Game/Art/Backgrounds/OfficeBaseGenerated_v3.png`

```text
Use case: identity-preserving photorealistic horror-game jumpscare frame.

Image 1 is the immutable 16:9 office background and edit target. Images 2–5 are all photographs of the same real person, Selly, and together form the identity reference set. Image 4 is especially useful for how his real cheeks, nose, mouth, teeth, jaw, and eyes behave during a wide open-mouth expression. Reproduce Selly specifically even while frightening: Romanian young adult male; medium warm skin; broad rounded face with full cheeks; softly rounded jaw and chin; thick dark mostly straight eyebrows; brown almond-shaped eyes; broad rounded nose; full lips with a fuller lower lip; thick dark hair with long loose curtain-like front sections swept up and away from a near-center side part. Keep only light natural stubble, no pronounced moustache or beard. The result must unmistakably remain Selly, not a generic monster or different man.

Create the single peak jumpscare frame: Selly has lunged out from the right side toward the seated player, filling most but not all of the game screen. Center his face slightly right of center. Show his ENTIRE head including all hair, both ears, entire chin, full neck, both shoulders, and upper chest inside the 1672x941 frame with breathing room around the skull; do not crop the forehead, hair, cheeks, mouth, or chin. His shoulders are thrust forward and slightly twisted. His mouth is open in a violent scream based on Image 4's real anatomy, eyes wide and locked directly on the player, eyebrows tense, nostrils flared. Make the expression unnaturally intense and frightening without changing his identity or turning him into a creature. He wears the same navy overshirt/jacket open over a plain white T-shirt.

Lighting and horror treatment: the office behind him remains dark and recognizable but is heavily defocused. Add a harsh intermittent pale fluorescent flash from below and the right, deep black facial shadows, tiny bright eye catchlights, realistic skin detail, slight sweat, directional lunge motion smear around the outer hair and shoulders, red/cyan chromatic separation on motion edges, horizontal CCTV scanline tearing, coarse sensor noise, and a narrow dark vignette. Keep the central eyes, nose, and mouth sharply readable beneath the distortion. Scary, abrupt, grimy, photorealistic, no gore.

Preserve Image 1's exact 1672x941 landscape framing and recognizable office composition behind him. Do not move or regenerate office props beyond unavoidable blur/lighting. Remove all microphones, text overlays, studios, other people, and reference backgrounds. No caption, no words, no UI, no border, no watermark, no blood, no exposed wounds, no extra limbs, no duplicated facial features.
```
