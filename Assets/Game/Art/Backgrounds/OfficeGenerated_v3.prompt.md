# OfficeGenerated_v3 generation prompts

Mode: built-in image-generation edit mode

Final assets:

- `OfficeBaseGenerated_v3.png`
- `OfficeLightLeftGenerated_v3.png`
- `OfficeLightRightGenerated_v3.png`
- `OfficeDoorLeftGenerated_v3.png`
- `OfficeDoorRightGenerated_v3.png`

The base was corrected from `OfficeBaseGenerated_v2.png`. Each state draft was
generated from the corrected base. The light states additionally use the legacy
checkout and employees' room frames as content references. The final state PNGs
were then assembled with an exact pixel mask: only the relevant left or right
doorway region comes from the generated state draft; every pixel outside that
region comes from the same corrected base. This keeps the telephone, mug,
papers, clipboard, posters, cabinet, and all other unaffected details identical
in every frame.

## Corrected master

```text
Use case: text-localization
Asset type: corrected 16:9 Unity horror-game office master background
Input images: Image 1 is the approved edit target and immutable composition.
Primary request: Correct only the motto on the small Lidl-logo produce poster on the back wall so it reads exactly: "meriti sa fii surprins". The word "fii" must contain exactly two lowercase i characters: f-i-i. Render that exact motto once on the existing poster in the same distressed condensed black type treatment and line layout. Do not change any other poster wording.
Constraints: Change only the incorrect motto letters on that one poster. Preserve the exact 1672x941 framing, camera, architecture, open black doorways, lighting, shadows, vignette, desk, telephone and cord, clipboard, pen, loose papers, mug, right-side device, cabinets, fan, binders, cleaning tools, floor marks, every other poster, logos, textures, object positions, object sizes, and every other pixel-level scene detail. Do not move, redraw, resize, relight, clean up, or replace any prop. No new text, UI, buttons, doors, people, watermark, or border.
```

## Left light

```text
Use case: compositing
Asset type: exact-resolution Unity horror-game office state frame — LEFT CHECKOUTS light on
Output requirement: exactly 1672 pixels wide by 941 pixels high, identical canvas, crop, and aspect ratio to Image 1.
Input images: Image 1 is the approved immutable office master and edit target. Image 2 is a content reference only for the supermarket checkout area visible through the left doorway; do not copy Image 2's office, wall control panels, buttons, posters, desk, or foreground.
Primary request: Change only the interior visible through Image 1's LEFT doorway from black to a dimly lit supermarket CHECKOUTS area matching the function and mood of Image 2. Clearly show recognizable checkout lanes immediately beyond the opening: dark checkout counters, cash-register terminals, conveyor stations, nested shopping carts, and distant store shelving. Keep it abandoned, grimy, empty, and readable as checkouts rather than a stockroom or storage aisle. Use a weak dirty fluorescent yellow-green light inside the checkout area. Keep the doorway fully open. The RIGHT doorway remains the exact same unlit black void as Image 1.
Composition/framing: fit the checkout scene naturally behind Image 1's exact left doorframe and perspective, as a continuous room beyond the threshold.
Constraints: Preserve Image 1's exact 1672x941 camera, crop, architecture, doorway geometry and frame, central ceiling light, back wall, exact poster text including "meriti sa fii surprins", desk, telephone and every key/button/cord, clipboard, pen, loose papers, mug, right-side device, cabinets, fan, binders, cleaning tools, floor marks, color grading, grain, and every object's exact position, scale, shape, orientation, texture, and lighting outside the left doorway interior. Do not add or copy physical wall controls, red/yellow buttons, labels, characters, threats, UI, text changes, watermark, border, shutter, closed door, storage racks, warehouse shelving, chemical shelves, or employee-break-room furniture. No people. Change only the left doorway interior and its physically justified threshold light.
```

## Right light

```text
Use case: compositing
Asset type: exact-resolution Unity horror-game office state frame — RIGHT EMPLOYEES' ROOM light on
Output requirement: exactly 1672 pixels wide by 941 pixels high, identical canvas, crop, and aspect ratio to Image 1.
Input images: Image 1 is the approved immutable office master and edit target. Image 2 is a content reference only for the employees' break room visible through the right doorway; do not copy Image 2's office, wall control panels, buttons, posters, desk, or foreground.
Primary request: Change only the interior visible through Image 1's RIGHT doorway from black to a dimly lit supermarket EMPLOYEES' BREAK ROOM matching the function and mood of Image 2. Clearly show a recognizable shabby staff room immediately beyond the opening: dented employee lockers with one slightly open and a high-visibility work vest hanging inside, a small battered break table, mismatched folding chairs, a microwave, noticeboard papers, mugs, and worn staff-room walls. Keep it abandoned, grimy, empty, and readable as an employees' room rather than a stockroom, warehouse, or chemical storage area. Use a weak dirty fluorescent yellow-green light inside the room. Keep the doorway fully open. The LEFT doorway remains the exact same unlit black void as Image 1.
Composition/framing: fit the employees' room naturally behind Image 1's exact right doorframe and perspective, as a continuous room beyond the threshold.
Constraints: Preserve Image 1's exact 1672x941 camera, crop, architecture, doorway geometry and frame, central ceiling light, back wall, exact poster text including "meriti sa fii surprins", desk, telephone and every key/button/cord, clipboard, pen, loose papers, mug, right-side device, cabinets, fan, binders, cleaning tools, floor marks, color grading, grain, and every object's exact position, scale, shape, orientation, texture, and lighting outside the right doorway interior. Do not add or copy physical wall controls, red/yellow buttons, labels, characters, threats, UI, text changes, watermark, border, shutter, closed door, warehouse shelving, product shelves, chemical containers, or checkout counters. No people. Change only the right doorway interior and its physically justified threshold light.
```

## Left door

```text
Use case: precise-object-edit
Asset type: Unity horror-game office state frame — LEFT security shutter closed
Input images: Image 1 is the corrected immutable 1672x941 master frame.
Primary request: Change only the LEFT doorway by lowering a heavy, grimy, dark steel roll-up security shutter fully across the existing left opening. The shutter must fit inside the exact left doorframe, follow its perspective, run from the top track to the floor threshold, and use worn horizontal metal slats with restrained highlights matching the room. The shutter is closed and unlit. Keep the RIGHT doorway the exact same open, unlit black void as Image 1.
Constraints: Preserve Image 1's exact camera, crop, architecture, doorframe geometry, lighting, central ceiling light, back wall, exact poster text including "meriti sa fii surprins", desk, telephone and every key/button/cord, clipboard, pen, loose papers, mug, right-side device, cabinets, fan, binders, cleaning tools, floor marks, shadows, color grading, grain, and every object's exact position, scale, shape, orientation, and texture. Do not move, redraw, replace, resize, or relight any prop or any region outside the left doorway/shutter. No side light, people, characters, threats, UI, wall control buttons, text changes, watermark, border, hinged door, or changes to the right doorway.
```

## Right door

```text
Use case: precise-object-edit
Asset type: Unity horror-game office state frame — RIGHT security shutter closed
Input images: Image 1 is the corrected immutable 1672x941 master frame.
Primary request: Change only the RIGHT doorway by lowering a heavy, grimy, dark steel roll-up security shutter fully across the existing right opening. The shutter must fit inside the exact right doorframe, follow its perspective, run from the top track to the floor threshold, and use worn horizontal metal slats with restrained highlights matching the room. The shutter is closed and unlit. Keep the LEFT doorway the exact same open, unlit black void as Image 1.
Constraints: Preserve Image 1's exact camera, crop, architecture, doorframe geometry, lighting, central ceiling light, back wall, exact poster text including "meriti sa fii surprins", desk, telephone and every key/button/cord, clipboard, pen, loose papers, mug, right-side device, cabinets, fan, binders, cleaning tools, floor marks, shadows, color grading, grain, and every object's exact position, scale, shape, orientation, and texture. Do not move, redraw, replace, resize, or relight any prop or any region outside the right doorway/shutter. No side light, people, characters, threats, UI, wall control buttons, text changes, watermark, border, hinged door, or changes to the left doorway.
```
