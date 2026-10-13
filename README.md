# Five Nights at Lidl

A Unity survival-horror game set in a supermarket, built around office doors,
lights, CCTV, battery management, and character-specific threats.

**Current version: 0.1.1 — development build.** This version reflects the major
office and doorway asset revisions, including dimmer lighting and redrawn
doorway characters with stronger shadows and consistent frame alignment.

## Current status

**All implemented nights are playable, but none are finished or release-ready.**
Audio assets are still missing, and every night needs further balancing,
playtesting, and polish. Playable does not mean complete.

| Night | Playable | Ready for release |
| --- | --- | --- |
| 1 | Yes | No |
| 2 | Yes | No |
| 3 | Yes | No |
| 4 | Yes | No |
| 5 | Yes | No |
| 6 and later | Yes | No |

The game currently includes:

- Office doors and lights, camera switching, battery drain, power failure,
  jumpscares, and the 6 AM shift-end sequence.
- Bolojan, Selly, Cabral, Dorian, and the Bostanica/Beregoi office threat.
- Saved night progression, progress reset, unlocked night replay, and an
  adjustable aggression reduction setting.
- Background music, Night 1 and Night 4 phone calls, door/light sounds,
  camera open/close and feed-switch sounds, spray bottle audio, doorway
  warnings, blender/alarm audio, the girls' presence audio, and a
  shared jumpscare sound.

## Roadmap

### 1. Complete audio

- [ ] Source and integrate the remaining gameplay sounds from
  [AUDIO_ASSET_TODO.md](AUDIO_ASSET_TODO.md).
- [x] Add camera monitor open/close, feed switching, and spray bottle sounds.
- [ ] Finish CCTV ambience and remaining camera interference cues.
- [ ] Add low-power, power-out, and 6 AM audio feedback.
- [ ] Review night-specific phone-call coverage and add the remaining calls
  where needed.
- [ ] Balance volumes and verify mute, looping, and night-end behavior.

### 2. Finish each night

- [ ] Playtest and tune Night 1 as the introduction.
- [ ] Playtest and tune Nights 2 and 3 as Dorian's mechanic enters the game.
- [ ] Playtest and tune Night 4 with all threats, including the office fight.
- [ ] Playtest and tune Night 5's difficulty and pacing.
- [ ] Playtest and tune Night 6 and later-night scaling.
- [ ] Check battery capacity, hourly aggression, spawn probabilities, and
  recovery windows together across the available difficulty settings.

### 3. Presentation and stability

- [ ] Review character likeness, lighting, and visual continuity across feeds.
- [ ] Verify office alignment and controls at different window aspect ratios.
- [ ] Polish camera transitions, jumpscares, menus, and win/loss presentation.
- [ ] Verify saves, resets, replay unlocks, and progression through full runs.
- [ ] Fix issues found during manual gameplay testing.

### 4. Prepare a release

- [ ] Mark nights complete only after their assets, audio, balance, and testing
  are approved.
- [ ] Create and test a standalone macOS build.
- [ ] Package the build with controls and player instructions.

## Development

Open the project in the Unity version recorded in
[ProjectVersion.txt](ProjectSettings/ProjectVersion.txt), then open
`Assets/Game/Scenes/MainMenu/MainMenu.unity` and enter Play mode.

Gameplay tuning lives in
[DemoPlaceholderController.cs](Assets/Game/Scripts/UI/DemoPlaceholderController.cs).
The girls' camera-opening spawn chance is code-only; other serialized tuning
fields use their saved Inspector values. The in-game aggression reduction is
saved through Settings.

Further project documentation:

- [Design brief](DESIGN_BRIEF.md)
- [Development plan](DEVELOPMENT_PLAN.md)
- [Missing audio checklist](AUDIO_ASSET_TODO.md)
- [Doorway art prompts and assembly notes](Assets/Game/Art/Backgrounds/OfficeDoorwayCreepy.prompt.md)
