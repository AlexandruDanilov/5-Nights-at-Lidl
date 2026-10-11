# Creepier office doorway revision

Generated with the built-in image-generation tool, using the existing room
images and approved character camera frames as references. This replaces the
previous overly bright doorway art while retaining the fixed aperture masks.

## Saved game assets

- `Assets/Game/Art/Backgrounds/OfficeLightLeftGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeLightRightGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeDoorLeftGenerated_v3.png`
- `Assets/Game/Art/Backgrounds/OfficeDoorRightGenerated_v3.png`
- `Assets/Game/Art/Characters/Cabral/Cabral_LeftDoor_Integrated.png`
- `Assets/Game/Art/Characters/Selly/Selly_RightDoor_Integrated.png`

All six are 1672×941 RGBA overlays, with original Unity paths and GUIDs.
OfficeBaseGenerated_v3.png and the Night 4 bottle wallpaper remain unchanged.
The lit checkout and employees room use weaker sickly overhead fluorescents,
deep shadows and darker distant details. Closed shutters use darker worn steel
and restrained olive highlights.

Cabral's likeness/outfit were checked against CAM 01, 02 and 06 and his existing
jumpscare. Selly's likeness/outfit were checked against CAM 04, 05, 07 and 08.
They have stern closed-mouth stares and correctly directed brow, cheek, neck,
and clothing shadows, rather than bright portrait lighting. Faces, age, build,
hair, clothes, and Selly's gold left-wrist watch are kept recognizable.

## Assembly

`Tools/OfficeDoorwayAssets.swift` uses the existing source-coordinate openings:
left `(123,65),(316,170),(316,697),(123,746)`;
right `(1351,176),(1551,72),(1551,742),(1351,702)`.

Native-resolution character alpha bounds are cropped and scaled uniformly to
525px high for Cabral and 523px for Selly; body proportions are not squeezed.
Their centers remain at x218.5 and x1461. Any shoulder beyond the opening is
occluded by the fixed jamb. Feet remain behind the threshold. Use the
`--preserve-character-lighting` flag to preserve the generated directional
lighting and avoid the older flat RGB darkening pass.

Each character is composited over the exact corresponding empty-light room
before applying the common aperture mask. Everything outside the opening
remains transparent, leaving every office/frame/prop pixel on the static base.

## Exact built-in generation prompts

## Right employees-room dim-light edit

```text
Use case: lighting-weather. Asset: locked-camera horror-game office RIGHT-light-on frame. Image 1 is the current approved office with right employees-room light on, EDIT TARGET; Image 2 is the immutable dark master frame for architecture. Change ONLY illumination INSIDE the existing right doorway to make the employees break room dramatically dimmer and creepier, roughly one third of previous brightness: dying low-power sickly yellow-green overhead fluorescent, narrow pools of weak light, deep black shadow behind locker doors, table/chairs mostly in darkness, faint murky reflections. Keep recognizable locker, break table, microwave and fluorescent fixtures in EXACT positions and sizes as Image 1. No rearranging, new objects or architecture. The right aperture boundaries on the 1672x941 canvas are (1351,176),(1551,72),(1551,742),(1351,702); metal jambs, threshold, whole office/desk/phone/mug/papers/posters/fan/ceiling and left black door outside it stay EXACTLY unchanged. Full canvas same 1672x941 composition/camera/crop. Do NOT dim the entire office. Lighting-on is still a readable lit room, not empty solid black. Authentic degraded photorealistic FNAF-style supermarket horror environment. No character, ghosts, buttons, text changes, glow fog, UI or watermarks.
```

## Selly full-body shadow-lit redraw

```text
Use case: identity-preserve. Asset: photorealistic FULL-BODY transparent character overlay for a dark office right doorway in Five Nights at Lidl. Images 1-4 are approved identity/outfit/build references of the SAME man, Selly, across surveillance rooms; use them to retain exact face, not a new person. Image 1 CAM07 is strongest frontal face reference; Image2 CAM05 closest well-lit face; Image3 CAM04 for hair/body, Image4 CAM08 for full build. Redraw the SAME Selly full-body looking unnervingly threatening. Match broad rounded face, full cheeks, softly rounded jaw, thick eyebrows, almond brown eyes, broad nose, fuller lower lip, swept dark curtain fringe, slight natural stubble; same youth/age, proportions and characteristic face. Same navy open overshirt, white T-shirt, dark trousers, light trainers, oversized gold luxury watch LEFT wrist. No handsome/smooth/skinny-face redesign. Pose: still human anatomy, rigid stance standing just OUTSIDE an office doorway, subtle forward lean and chin lowered, arms tense at sides, closed mouth with cold hostile stare up toward player. No smile. Creepy through expression and lighting, not disfigurement. Lighting physically matches a DIM dirty employees room: single weak sickly yellow-green fluorescent above and slightly behind him, heavy occlusion shadows beneath brow/eye sockets, nose/cheek plane and chin/neck, much of lower body lost in black; only small highlights along hair/shoulder and selected facial planes so his face remains identifiable. No strong frontal light/studio portrait fill; shirt muted grey in shadow not glowing white. Tiny realistic light catchlights in eyes, normal dark pupils; no glowing/red eyes, no monster face, gore or makeup. Full head, shoulders, arms, legs and both shoes visible with padding. Genuine transparent RGBA background, ONLY character, no walls/doorframe/room/floor/contact shadow/objects/text/borders; preserve generated directional shadow pattern on his body. Final intended placement narrow right door width180px height523px, do not crop feet/hair.
```

# Cabral creepy doorway drafts

Built-in image_gen. No tracked assets modified by subagent.

Selected empty-light draft: exec-e9be8953-34df-433c-aef3-737f81bbddba.png

Initial Cabral cutout: exec-bbaf67d6-b8d7-4d1d-ac26-b8c51b07edcb.png

Selected final Cabral relit transparent cutout: exec-f0bfd3dd-a4fa-4d25-b7c1-f5eb08cb5269.png

## Left empty-light exact prompt

```text
Use case: lighting-weather
Asset type: fixed-camera photorealistic horror game LEFT doorway empty-light state.
Input images: Image 1 is the unchanged office master, controlling exact camera viewpoint and surrounding geometry. Image 2 is the edit target/content reference, the current transparent LEFT doorway-only room overlay: use EXACTLY its same checkout counters, shopping cart, tiled floor, shelving, ceiling beams, fixture positions and perspective; its transparent blank regions are not black walls.
Primary request: remake only the checkout-room content seen through the existing left doorway substantially darker and creepier. Keep every checkout object and structural line in the same position. Lower fluorescent intensity radically; a faint sickly muted yellow-green overhead glow reveals the nearest worn tiles and checkout counter while far shelves and ceiling fade almost to black. Long dark shadows, realistic underexposure, abandoned supermarket at night. The lamps must not be glaring bright white strips. Preserve recognizable near counter/cart details but avoid a brightly lit cheerful store.
Composition/framing: return the exact full 1672x941 wide frame of Image 1 with the same viewpoint, perspective, camera height and crop. Contents visible only through the exact LEFT INNER APERTURE polygon (123,65),(316,170),(316,697),(123,746). Retain Image 1 unchanged outside this opening; it controls every door frame, threshold, ceiling, wall, desk and prop. Right doorway remains black. Do not add an inner frame.
Lighting/mood: near-black abandoned checkout room with restrained dim sickly overhead fluorescent light, realistic grime and photographic texture, subtle visibility sufficient for game navigation; darker than Image 2, approximately two stops less bright.
Constraints: change lighting/shadow only inside left opening. NO displaced doors, checkout counter, cart, shelves, beams, light fixtures, objects, perspective, lens, office pixels or floor edges. NO people, silhouettes, UI, buttons, spray bottles, extra props, fog, smoke, text, watermark. No changes to office surroundings. A full landscape photo frame, not a crop.
```

## Cabral cutout exact prompt

```text
Use case: identity-preserve
Asset type: transparent full-body Cabral character sprite for a photorealistic horror game's left office doorway.
Input images: Images 1, 2 and 3 are the approved camera-route identity/outfit/build references for the SAME man Cabral. Image 4 is supporting close-up facial identity reference ONLY from the jumpscare; do NOT copy its open mouth or scream pose.
Primary request: redraw that EXACT same recognizable man, genuinely transparent background, full body from scalp to shoes. Preserve approved bald head, dark-brown skin tone, face shape, nose, ears, neatly trimmed black moustache and short pointed chin goatee. Preserve broad strong shoulders and sturdy natural body build without making him obese or changing facial identity. Same light-blue blazer, white open-collar shirt, dark navy trousers and polished black shoes as reference camera frames.
Pose/composition: stands just outside a narrow security-office doorway and faces the player nearly straight on. Upright unsettlingly rigid posture, shoulders set, arms hanging straight down close to body, hands relaxed; feet relatively close, no wide stance or reaching into office. Full legs and both shoes visible. Whole full-body silhouette centered on portrait canvas with generous transparent padding around head, hands and feet. Narrow upright proportions, scale suitable to fit within a 191x525 doorway slot when normalized; natural anatomy and broad shoulders, not a pinched skinny figure.
Expression: closed mouth, no smile, hostile stern fixed unblinking gaze at viewer, brows slightly lowered. Maintain his real facial features; fear comes from stillness, expression and shadows. Dark natural pupils with tiny physically plausible catchlights, no glowing or red eyes, no supernatural features.
Lighting/mood: physically coherent DIM YELLOW-GREEN overhead fluorescent light from high above/front-left, NO frontal fill or portrait lighting. Forehead and upper shoulder planes catch a restrained low highlight; brow casts strong dark shadows into the eye sockets, cheeks fall into uneven shadow, under nose/chin/neck near black. One side of face and blazer substantially darker, trousers and hands largely deep shadow. Realistic skin pores and fabric; face remains identifiable, unsettling horror-photography mood. No bright flat suit and no studio gloss.
Constraints: actual transparent PNG RGBA alpha around ONLY the character, including between legs/arms. No background, wall, doorway, floor, props, rendered checkerboard, matte, contact/cast shadow, smoke, border, watermark or text. Do not include the camera environments. Do not alter identity, skin tone, haircut, clothes, body build. Do not copy grin or clasped hands from an older doorway draft.
```

## Cabral lighting refinement exact prompt

```text
Use case: lighting-weather / identity-preserve
Asset type: final transparent full-body horror-game character Cabral standing in a dim doorway.
Input image 1 is the sole exact full-body identity, outfit, anatomy, pose, silhouette and composition target. Keep his appearance, face, closed-mouth menacing gaze, body, bald head, facial hair, arms-down pose, clothing, pants, feet, and full portrait framing unchanged. Image 2 is lighting reference only, for the dim yellowish night checkout-room illumination beyond left door; do not add any of that background.
Primary request: ONLY relight the exact man in Image 1 dramatically more dimly with one weak yellow-green overhead fluorescent practical from above and slightly behind his left shoulder; remove the current bright flat front illumination. The result should look like he belongs standing in the dark doorway of Image 2. His top of bald scalp and upper blazer shoulders catch sparse muted low highlights; deep crisp shadows from brow cover most eye sockets, keeping actual dark pupils and two subtle catchlights. One half of face falls into deep shadow. The nose/chin cast strong shadow downward; neck goes almost black. Blazer becomes shadowy muted dark blue rather than a bright light-blue studio blazer while retaining its blue fabric identity. Shirt mostly grey under dim light, trousers and hands nearly black with only sufficient texture to distinguish silhouette. No broad softbox, frontal fill, rimlight halo or beauty lighting. Realistic shadowing and underexposure, creepy still unblinking stare.
Constraints: preserve exact face, facial identity, expression, skin tone identity, silhouette and proportions of Image 1. Full body feet included with generous transparency padding; same canvas. ONLY illumination/shadows change. Actual transparent RGBA alpha background everywhere outside man, no rendered checkerboard, black/white matte, room, door, floor, objects, ground plane or cast/contact shadow. Do not generate background or change pose. No red/glowing eyes, demon alterations, gore, text or watermark.
```


# Creepy closed-shutter lighting edits

Built-in imagegen, transparent_background=true, two independent edits.

- Left draft: exec-d72dbf6e-f13c-468d-a4ff-4fb458442448.png
- Right draft: exec-3ab1a626-c225-42d0-a9f5-69f03dc59faa.png

## Left prompt

```text
Use case: lighting-weather
Asset type: transparent RGBA closed-LEFT-security-shutter game overlay, fixed camera office state.
Input images: Image 1 is the EDIT TARGET, an existing full-canvas transparent doorway-only shutter overlay. Image 2 is REFERENCE ONLY for the permanent office camera, scale, aperture and subdued photorealistic horror style.
Primary request: change only the lighting and surface wear of the exact existing left steel shutter from Image 1, making it creepier, noticeably dimmer and less flatly illuminated. Preserve its exact horizontal corrugations, all straight slat edges, number of slats, perspective, and original silhouette. Keep the exact same full 1672x941 wide canvas, with the shutter at the far left in the SAME pixel coordinates and SAME scale. Everything outside the shutter stays genuinely transparent, including the huge empty area on the right.
Lighting/mood: near-black worn industrial steel, subdued dirty olive-gray highlights touching only a few thin slat ridges, deep black occlusion in the troughs, uneven ingrained grime and faint scratches. Atmospheric abandoned-supermarket horror realism, subtle grain; unsettling darkness but enough restrained slat detail to read as a closed shutter. No bright spotlight, red lighting or dramatic new light source.
Exact geometry invariants: left aperture polygon is (123,65), (316,170), (316,697), (123,746) on 1672x941 top-left coordinates. Do not move, widen, tilt, expand, repaint a frame, or recenter the shutter. ONLY edit existing nontransparent shutter pixels. Preserve the transparent alpha outside this shape exactly. There are NO office walls, frame, threshold, room, background or floor in the output.
Avoid: any new objects, people, buttons, signage, blood, symbols, text, watermark, white/black background matte, checkerboard pixels, camera movement, crop, wide new reflections, changed geometry or a new door frame. Output a truly transparent PNG.
```

## Right prompt

```text
Use case: lighting-weather
Asset type: transparent RGBA closed-RIGHT-security-shutter game overlay, fixed camera office state.
Input images: Image 1 is the EDIT TARGET, an existing full-canvas transparent doorway-only shutter overlay. Image 2 is REFERENCE ONLY for the permanent office camera, scale, aperture and subdued photorealistic horror style.
Primary request: change only the lighting and surface wear of the exact existing right steel shutter from Image 1, making it creepier, noticeably dimmer and less flatly illuminated. Preserve its exact horizontal corrugations, all straight slat edges, number of slats, perspective, and original silhouette. Keep the exact same full 1672x941 wide canvas, with the shutter at the far right in the SAME pixel coordinates and SAME scale. Everything outside the shutter stays genuinely transparent, including the huge empty area on the left.
Lighting/mood: near-black worn industrial steel, subdued dirty olive-gray highlights touching only a few thin slat ridges, deep black occlusion in the troughs, uneven ingrained grime and faint scratches. Atmospheric abandoned-supermarket horror realism, subtle grain; unsettling darkness but enough restrained slat detail to read as a closed shutter. No bright spotlight, red lighting or dramatic new light source.
Exact geometry invariants: right aperture polygon is (1351,176), (1551,72), (1551,742), (1351,702) on 1672x941 top-left coordinates. Do not move, widen, tilt, expand, repaint a frame, or recenter the shutter. ONLY edit existing nontransparent shutter pixels. Preserve the transparent alpha outside this shape exactly. There are NO office walls, frame, threshold, room, background or floor in the output.
Avoid: any new objects, people, buttons, signage, blood, symbols, text, watermark, white/black background matte, checkerboard pixels, camera movement, crop, wide new reflections, changed geometry or a new door frame. Output a truly transparent PNG.
```
