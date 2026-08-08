# Five Nights at Lidl — Demo Design Brief

> Status: Draft — creator fields marked `TODO` still need decisions.
>
> Scope: One complete playable night with one original animatronic-style
> character. The demo proves the core loop before additional characters, nights,
> story, or final polish are added.

## 1. High concept

### Working title

**Five Nights at Lidl**

### One-sentence pitch

`TODO: Describe the game in one sentence.`

Example structure:

> The player must survive a night in ______ by monitoring ______ while managing
> a limited supply of ______ and defending against ______.

### Setting

`TODO: Where does the game take place, and what makes the location interesting?`

### Player role

`TODO: Who is the player, why are they there, and why can they not simply leave?`

### Tone and visual direction

`TODO: Describe the desired mood and art style.`

Possible considerations:

- Realistic, stylized, illustrated, low-resolution, retro, VHS, or another style.
- Clean surveillance footage or heavily degraded footage.
- Serious horror, surreal horror, dark comedy, or another tone.
- How graphic the scares should be.

## 2. Demo objective

The player begins at 12 AM and must survive until 6 AM. They monitor camera feeds,
track one hostile character, operate two doors and two corresponding lights, and
manage a finite battery supply.

The player wins when the clock reaches 6 AM. The player loses if the character
successfully enters the office and completes its attack.

The demo must be understandable and completable without developer assistance. It
may use placeholder media during development, but its mechanics must form one
complete playable loop.

## 3. Demo scope

### Included

- A title screen with Start Demo, basic volume controls, and Quit.
- One playable night from 12 AM to 6 AM.
- One office/control-room view.
- A left door and a right door.
- A left light and a right light.
- A camera monitor that can be raised and lowered.
- A camera map with multiple selectable feeds.
- A finite battery/power system.
- A visible clock.
- One original hostile character.
- Character movement between predefined locations.
- A successful defense against the character.
- A loss sequence and retry flow.
- A 6 AM victory sequence and return-to-menu flow.
- Essential sound, feedback, and placeholder visuals.
- A macOS build that runs outside the Unity editor.

### Explicitly excluded from the first demo

- Additional characters.
- Additional nights.
- Difficulty selection or custom-night mode.
- Large story sequences or extensive voice acting.
- Achievements, cloud saves, or online features.
- Windows packaging.
- Mobile or console versions.
- Real-time 3D unless an approved mechanic requires it.
- Final storefront integration.

New ideas should be recorded under **Future ideas** rather than added immediately
to the demo scope.

## 4. Core gameplay loop

1. Observe the office and listen for cues.
2. Raise the camera monitor to locate the hostile character.
3. Infer which side of the office the character is approaching.
4. Lower the monitor when office controls are needed.
5. Use the corresponding light to check the doorway.
6. Close the appropriate door before the character can enter.
7. Reopen doors and switch off unnecessary systems to conserve battery.
8. Repeat while the character becomes increasingly threatening.
9. Reach 6 AM before the battery is exhausted or the character attacks.

The tension should come from incomplete information and competing demands on the
player's attention and battery, not from complicated controls.

## 5. Office controls

### Left side

- **Left door:** Toggles between open and closed.
- **Left light:** Reveals whether the character is immediately outside the left
  entrance.
- Closing the left door consumes additional battery.
- Activating the left light consumes additional battery.

### Right side

- **Right door:** Toggles between open and closed.
- **Right light:** Reveals whether the character is immediately outside the right
  entrance.
- Closing the right door consumes additional battery.
- Activating the right light consumes additional battery.

### Interaction decisions

- Light behavior: `TODO: Hold to use, toggle on/off, or short timed activation?`
- Can both lights be active together? `TODO`
- Can lights be used while the camera monitor is raised? Default: **No**.
- Can doors be operated while the camera monitor is raised? Default: **No**.
- Does the office pan horizontally with mouse position? `TODO`
- Do doors animate or switch between static states? `TODO`

Controls will remain data/configuration driven where reasonable so these choices
can be tuned after playtesting.

## 6. Camera system

The player raises a monitor overlay and selects feeds from a camera map. Only one
feed is shown at a time. Camera content may be made from static backgrounds,
character overlays, short image sequences, and effects rather than continuous
video.

### Camera map

Number of feeds: **8** for the current floor plan.

| Camera ID | Location name | Purpose in route | Required view/notes |
|---|---|---|---|
| CAM 01 | Dairy | Store-floor route | Creator-provided still needed. |
| CAM 02 | Bakery | Store-floor route | Creator-provided still needed. |
| CAM 03 | Pastry | Store-floor route | Creator-provided still needed. |
| CAM 04 | Biscuits | Store-floor route | Creator-provided still needed. |
| CAM 05 | Ice Cream | Store-floor route beside stockroom | Creator-provided still needed. |
| CAM 06 | Checkouts | Left-side approach to office | Creator-provided still needed. |
| CAM 07 | Stockroom | Right-side route origin | Creator-provided still needed. |
| CAM 08 | Employee Room | Right-side approach to office | Creator-provided still needed. |

Add or remove rows once the character's route is defined.

### Camera behavior

- Raising the monitor consumes additional battery.
- Switching feeds produces immediate visual and audio feedback.
- The currently selected camera is clearly identified.
- Camera noise/static disguises image changes when appropriate.
- The character is visible only when the current feed and its state allow it.
- The monitor cannot prevent an attack merely by remaining open unless that is an
  intentional character mechanic.

Current prototype status: a camera dashboard and floor-plan map are implemented
with eight selectable placeholder camera nodes. All map labels are in English,
and the office is marked as the player's position rather than a camera. The
layout follows the creator's sketch: the checkouts lead to the left office side,
while the stockroom connects through the employee room to the right side.
Every provisional route uses the same visual treatment. The current connections
are CAM 01–02, CAM 01–03, CAM 01–04, CAM 01–05, CAM 02–03, CAM 02–06,
CAM 03–04, CAM 04–05, CAM 05–07, CAM 06–Office, CAM 07–08, and CAM 08–Office. These links
are non-directional visual planning aids and will be finalized alongside the
hostile character's movement design. No camera-feed artwork or feed behavior is
connected yet. Left and right
door/light controls toggle their respective generated office states. A closed
door visually takes priority over an active light on the same side. Additional
power drain for doors and lights is not yet connected.

The open left side leads into the supermarket checkout section. When its light
is active, ruined checkout lanes, abandoned equipment, and a heavily decayed
store interior become visible beyond the doorway.

The open right side leads into a neglected employee break room. When its light
is active, battered lockers, broken staff furniture, old appliances, abandoned
belongings, and a heavily decayed staff area become visible beyond the doorway.
A rear service doorway connects this room directly to the stockroom, establishing
the right-side threat route from stockroom to employee room to office.

Open decisions:

- Does viewing the character delay, encourage, or not affect movement? `TODO`
- Can cameras temporarily fail? Default for the demo: **No**, unless central to
  the character's design.
- Does the monitor remember the last selected feed? Default: **Yes**.

## 7. Battery and power

The night begins with a finite battery value displayed as a percentage. Power
drains continuously and drains faster when systems are active.

The current demo uses the relative power balance of FNAF 1 Night 1, scaled from
its approximately 90-second hours to this demo's 60-second hours. The battery
starts at 100%. Baseline office operation drains 0.15% per second (1% every
6.67 seconds). Each closed door and the single active doorway light adds another
0.15% per second. Raising the monitor adds only 0.075% per second (1% every
13.33 seconds), half the standard device penalty, to encourage camera use and
exploration of camera-feed details.

Power is stored and drained continuously as a fractional value while the HUD
shows whole percentages. Rapidly toggling a device therefore cannot avoid its
energy cost. As in FNAF 1, only one doorway light can be active at a time, and
raising the camera monitor switches both lights off.

The battery percentage is green from 71–100%, yellow from 21–70%, and red from
0–20%.

### Power consumers

- Baseline office operation.
- Closed left door.
- Closed right door.
- Active left light.
- Active right light.
- Raised camera monitor.

These initial values should be revisited through playtesting after the hostile
character's timing and required camera-check frequency are implemented.

### Power failure

When the battery reaches zero:

1. Doors can no longer remain closed.
2. Lights and camera controls stop functioning.
3. The office enters a powerless state with appropriate visual/audio feedback.
4. The player faces a short, uncertain delay before the character can attack.
5. Reaching 6 AM during this sequence still counts as a victory.

Exact power-out timing and presentation: `TODO after the character and tone are
defined.`

## 8. Clock and night pacing

- The displayed time begins at 12 AM.
- It advances through 1 AM, 2 AM, 3 AM, 4 AM, 5 AM, and 6 AM.
- Reaching 6 AM immediately prevents further attacks and starts the victory
  sequence.
- The clock uses game time, not the computer's real clock.
- Pausing the game pauses the night timer.

Target real-world night duration: **6 minutes**. Each displayed in-game hour lasts
60 real-world seconds for the first demo.

Character aggression may increase at configured hours. The first balancing goal
is a tense but consistently understandable demo, not extreme difficulty.

## 9. Character design

### Identity

- Name: `TODO`
- Type/form: `TODO`
- Visual concept: `TODO`
- Personality or behavioral theme: `TODO`
- Signature audio cue: `TODO`

### Gameplay behavior

- Starting camera/location: `TODO`
- Possible route or routes: `TODO`
- Can it approach both doors? `TODO`
- Movement trigger: `TODO: timed opportunities, player behavior, or another rule`
- Information available to the player: `TODO`
- Immediate pre-attack warning: `TODO`
- Correct defensive response: `TODO`
- What causes it to retreat: `TODO`
- What causes the player to lose: `TODO`
- How its aggression changes during the night: `TODO`

### Technical starting model

Unless the final design calls for something different, the first implementation
will use a location graph:

```text
Start location
      ↓
Intermediate camera locations
      ↓
Left or right approach
      ↓
Doorway check
      ├── Correct door closed → retreat
      └── Door open → enter office and attack
```

At configured intervals, the game gives the character a movement opportunity.
Difficulty and the current hour influence the chance of movement. Each transition
is logged in development builds, making behavior testable even before final art
exists.

This is a technical default, not a creative restriction. The creator's completed
behavior description takes precedence.

## 10. Win and loss presentation

### Victory

- Trigger: the clock reaches 6 AM.
- Player controls are disabled.
- A short 6 AM transition plays.
- The result screen offers return to menu or replay.
- Final visual/audio concept: `TODO`

### Loss

- Trigger: the character completes its attack condition.
- Player controls are disabled.
- A short scare/attack sequence plays.
- The result screen offers retry or return to menu.
- Final visual/audio concept: `TODO`

The demo must also handle quitting or returning to the menu without being treated
as a win or loss.

## 11. Required media inventory

Final media production should begin only after the grey-box systems establish the
exact needs. The likely minimum inventory is listed below.

### Art

- Main-menu background and title treatment.
- Office background or office view segments.
- Left door: open and closed states, plus animation frames if desired.
- Right door: open and closed states, plus animation frames if desired.
- Left doorway: unlit, lit-empty, and lit-character states.
- Right doorway: unlit, lit-empty, and lit-character states.
- One background for every camera feed.
- Character state/overlay for every location where it can be visible.
- Monitor frame, camera map, camera buttons, and static/noise treatment.
- Battery, usage, clock, and menu UI artwork.
- Character attack/jumpscare frames.
- 6 AM/victory presentation.
- Application icon.

### Audio

- Main ambience loop.
- Camera monitor raise/lower.
- Camera feed change/static.
- Left and right door sounds.
- Left and right light sounds if appropriate.
- Character movement or location cues.
- Doorway warning cue if part of the character design.
- Power-low and power-out feedback.
- Attack/jumpscare sound.
- 6 AM/victory sound.
- Menu selection and confirmation sounds.

Codex will provide filenames, dimensions, formats, frame counts, and loop details
before each final asset is requested.

## 12. User interface information

During gameplay, the player must be able to understand:

- Current time.
- Remaining battery percentage.
- Relative current power usage.
- Door states.
- Light states.
- Whether the camera monitor is active.
- Which camera feed is selected.

The UI should communicate state without revealing hidden character logic.

Menu text, typography, colors, and final layout: `TODO after the tone and visual
direction are chosen.`

## 13. Basic settings for the demo

Required:

- Master volume.
- Fullscreen/windowed mode.
- Resolution behavior appropriate for macOS displays.
- Pause/resume.
- Return to menu.
- Quit application.

Optional after the core loop works:

- Separate music and sound-effect volume.
- Brightness/gamma control.
- Reduced screen shake or flashing-effects option.
- Subtitles if the demo adds spoken dialogue.

## 14. Grey-box acceptance criteria

The mechanical demo is successful when all of the following work with placeholder
media:

- The player can start a night from the menu.
- Both doors open and close independently.
- Both lights reveal the correct doorway state.
- The camera monitor opens, closes, and switches between every configured feed.
- Battery drain reflects active systems and reaches zero correctly.
- The clock advances from 12 AM to 6 AM.
- The character moves through valid locations and can be tracked.
- Closing the correct door prevents the character's attack.
- Leaving the required door open permits a loss.
- The player can win by surviving until 6 AM.
- Retry and return-to-menu flows work after both victory and loss.
- The application can be built and played on macOS outside Unity.
- The Unity Console has no recurring exceptions during a complete playthrough.

## 15. Decisions needed from the creator

These do not all need to be answered before coding begins.

- [ ] Working title.
- [ ] Setting and player role.
- [ ] Tone and visual direction.
- [ ] Camera-map layout and location names.
- [ ] Office layout and whether it pans.
- [ ] Light interaction behavior.
- [ ] Target night duration.
- [ ] Character identity and appearance.
- [ ] Character route, cues, counterplay, and loss condition.
- [ ] The first original/new mechanic.
- [ ] Victory and loss presentation concepts.
- [ ] Whether spoken dialogue is included in the demo.

## 16. Future ideas

Record ideas here without expanding the current demo scope.

- `TODO`
