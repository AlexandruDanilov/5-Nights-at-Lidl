# Cabral asset generation notes

The live doorway frame has been redrawn using the approved CAM 01/02/06
likeness, with a stern gaze and dim overhead shadows. It is now a 1672×941 RGBA
aperture overlay over the same dim checkout room as the empty-light state.
See `../../Backgrounds/OfficeDoorwayCreepy.prompt.md` for current prompts.
The following records the original camera/jumpscare and doorway generation.

Generated with the built-in image generation tool in identity-preserving edit/
composite mode. The three supplied Cabral photographs were used together as
identity references. Each camera frame also used its matching stock feed as the
composition reference. The final doorway frame instead uses a locked-background
workflow: Cabral was generated as an isolated chroma-keyed full-body subject,
keyed to transparency, graded for the doorway light, and mechanically composited
over the approved office left-light frame. Pixels outside Cabral's silhouette are
therefore identical to that approved frame.

All final files are 1672 x 941 RGB PNGs.

## Shared identity and continuity prompt

Preserve Cabral as a completely bald, exceptionally broad and heavy-set
middle-aged Black Romanian man with a broad round face, full cheeks, deep-set
dark-brown eyes, wide rounded nose, and a neatly trimmed moustache joined to a
short boxed goatee. Keep the same powder-blue suit jacket, open-collar white
dress shirt, navy trousers, and polished black shoes in every route frame.
Integrate him photorealistically into the existing Five Nights at Lidl image:
match its perspective, lens, darkness, colored light, grain, grime, compression,
and security-camera texture. Preserve the room, props, architecture, signage,
and camera composition outside the requested character addition.

## Final frame prompts

- **CAM 01:** place his full body in the right midground dairy aisle, distant
  but recognizable, rigidly facing the camera, grounded behind the foreground
  fixtures with plausible floor contact and aisle occlusion.
- **CAM 02:** place his full body in the left-center bakery area, one route step
  closer than CAM 01, motionless and directly watching the camera, with natural
  counter/shelf occlusion and matching warm bakery spill.
- **CAM 06:** place his full body in the central-left checkout lanes, closer and
  more imposing than CAM 02, while keeping him away from the far-right office
  security doorway; preserve every checkout and sign.
- **Left door:** using the office left-light frame, place Cabral fully outside
  the office, immediately beyond the open left doorway and one step behind the
  metal threshold. Keep both shoes and his entire body behind the door plane,
  with natural frame occlusion and a visible threshold gap. He remains imposing
  and nearly door-height, but his realistic broad shoulders occupy only about
  70–75 percent of the opening. The office, desk, controls, posters, mug, phone,
  framing, and crop remain unchanged. Generate Cabral alone against perfectly
  uniform `#00FF00`, with generous padding and no background, floor, cast shadow,
  green spill, text, or extra objects. Preserve his frontal hands-clasped pose and
  the supplied identity, then composite only the keyed subject over
  `OfficeLightLeftGenerated_v3.png` without regenerating any part of the room.
- **Jumpscare:** make a frightening close attack toward the office camera, with
  an open-mouth roar and hostile eyes. Keep his complete bald head, mouth, chin,
  and broad shoulders visible so the runtime shake does not crop away his face.
  Match the same dim office-horror lighting and degraded photographic texture.
