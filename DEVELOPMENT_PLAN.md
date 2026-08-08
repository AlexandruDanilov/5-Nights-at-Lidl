# Five Nights at Lidl — Development and macOS Shipping Plan

## 1. Project goal

Build an original, fixed-camera survival-horror game inspired by the structure of
FNAF 1, with new mechanics and original characters, artwork, audio, writing, and
branding.

The first supported platform will be macOS. The project will be structured so a
Windows version can be added later without rewriting the game.

The first release target is a build that can be sent to friends and played on a
Mac without installing Unity or opening the project.

## 2. Creative and technical ownership

### The creator decides and supplies

- The game's title, premise, setting, and tone.
- Character identities, appearances, personalities, and names.
- Character movement rules at a design level: what each character does and how
  the player is expected to counter it.
- Final background art, character art, camera images, animations, icons, and UI
  artwork.
- Final music, ambience, voices, and sound effects.
- Written content such as menus, introductions, phone calls, instructions, and
  endings.
- Which proposed mechanics belong in the game.
- Final approval of pacing, balance, presentation, and release content.

All final media must be original, properly licensed, or created by the creator.
FNAF names, characters, artwork, audio, logos, and other copyrighted assets will
not be included.

### Codex implements and maintains

- The Unity project and its folder structure.
- Gameplay architecture and C# code.
- Office controls, cameras, doors, lights, power, time, enemy logic, win/loss
  conditions, and new mechanics that we agree on.
- Menus, settings, saving, loading, pause behavior, and input handling.
- Systems for importing and displaying creator-supplied media.
- Temporary neutral placeholders when final media is not ready.
- Debugging tools that make enemy behavior and balance easier to inspect.
- Automated tests where they provide useful protection.
- macOS build configuration, release builds, and packaging instructions.
- Technical documentation and keeping this plan current as decisions change.

Codex will not choose final characters, story, artwork, voices, music, or visual
identity on the creator's behalf. When code needs an asset that does not yet
exist, a clearly named placeholder will be used and an asset request will be
documented.

## 3. Technical baseline

- Engine: Unity 6000.5.7f1, pinned to the version installed on the development Mac.
- Language: C#.
- Initial rendering setup: lightweight core project using UI Toolkit and 2D
  content. URP will be added only if the approved visual direction needs its 2D
  lighting or shader features.
- Initial platform: macOS.
- Intended macOS release architecture: Universal, supporting Apple Silicon and
  Intel where Unity's selected build configuration permits it.
- Reference presentation: 16:9, initially designed around 1920x1080.
- Version control: Git with Unity text serialization and visible `.meta` files.
- Large media: Git LFS for large source images, audio, video, and other binary
  assets when needed.
- Input: Unity Input System, with mouse as the first required input method.

The game will primarily use 2D/pre-rendered media. Unity animation, shaders, and
code-driven motion will provide effects such as fades, camera noise, monitor
movement, flicker, screen shake, and jumpscare playback. Real-time 3D will only
be added when a specific approved mechanic benefits from it.

## 4. Planned project layout

```text
Assets/
  Game/
    Art/
      Backgrounds/
      Characters/
      CameraFeeds/
      UI/
      Effects/
    Audio/
      Ambience/
      Music/
      SFX/
      Voice/
    Data/
      Characters/
      Nights/
      Balance/
    Prefabs/
    Scenes/
      Bootstrap/
      MainMenu/
      Gameplay/
      Test/
    Scripts/
      Core/
      Gameplay/
      Characters/
      Cameras/
      UI/
      Audio/
      Persistence/
      Debugging/
    Settings/
    Tests/
Builds/
Documentation/
```

Unity-generated directories such as `Library`, `Temp`, `Logs`, `obj`, and local
build output will not be committed. Unity `.meta` files must be committed because
they contain asset identities and references.

## 5. Development stages

### Stage 0: Define the first playable version

Before producing final art, we will write a short design specification covering:

- The player's objective.
- What begins and ends a night.
- The office controls available to the player.
- The camera map and number of locations.
- The initial power/resource rules.
- One character's route, behavior, and counterplay.
- The first new mechanic that distinguishes the game.
- Loss conditions and the 6 AM/win condition.

The first playable version should contain one night, one enemy, and one complete
gameplay loop. Additional content comes after that loop is proven enjoyable.

### Stage 1: Create the production foundation

Codex will:

- Create the Unity project using the pinned Unity 6.3 LTS version.
- Configure the repository and Unity `.gitignore`.
- Configure text-based assets, input, resolution behavior, and macOS settings.
- Create a bootstrap scene, main-menu scene, gameplay scene, and test scene.
- Establish naming conventions and assemblies if the project benefits from them.
- Add an in-game build/version label for test builds.
- Verify that a blank macOS application builds and starts.

This stage ends with a committed project that can be cloned, opened, and built.

### Stage 2: Build a grey-box vertical slice

Codex will use simple temporary shapes, labels, and placeholder audio to build:

- Main menu and start-game flow.
- Office view and interaction zones.
- Monitor open/close behavior.
- Camera-map navigation.
- A night clock.
- Power/resource consumption.
- Doors, lights, or their eventual equivalents.
- One data-driven enemy.
- Win, loss, retry, and return-to-menu flows.
- Pause and focus-loss behavior.

The creator will evaluate whether the loop is understandable and enjoyable before
final media production begins.

### Stage 3: Establish the content pipeline

For each required asset, Codex will provide an asset request containing:

- Asset ID and intended filename.
- What it depicts or communicates.
- Where and when it appears.
- Required pixel dimensions or duration.
- File format and transparency requirements.
- Required variants or animation frames.
- Whether it must loop.
- Any technical safe areas or alignment points.

The creator supplies the final asset and chooses its content. Codex imports it,
configures it in Unity, connects it to the game, and verifies it in a build.

### Stage 4: Implement the complete gameplay systems

After the vertical slice is approved, Codex will add:

- Remaining character behaviors.
- Additional nights and difficulty progression.
- The approved original mechanics.
- Data-driven balance values so tuning does not require code changes.
- Audio routing and volume controls.
- Save data and progression.
- Settings and accessibility options selected for the release.
- Tutorial/instruction presentation.
- Final win, loss, and ending sequences.

Each character will have a separate configuration asset and behavior module where
practical. One character's changes should not silently alter another character.

### Stage 5: Replace placeholders and polish

- Import and integrate final artwork and audio.
- Remove or clearly exclude unused placeholder assets.
- Tune animation timing, transitions, feedback, and sound mixing.
- Verify legibility at supported resolutions.
- Add loading, transition, and error handling where needed.
- Profile memory, CPU usage, frame pacing, and load times.
- Compress assets without unacceptable quality loss.
- Conduct repeated balance and playability passes.

### Stage 6: macOS release preparation

- Set the final product name, company/creator name, version, and bundle identifier.
- Add final application icons.
- Configure a non-development release build.
- Build the macOS `.app` using the intended CPU architecture.
- Test on at least one clean Mac that does not have Unity installed.
- Test save creation, relaunching, fullscreen/windowed behavior, audio devices,
  common display resolutions, and first-run behavior.
- Remove development cheats, debug overlays, and verbose logging from release
  builds unless intentionally retained.
- Create release notes and a short player-facing README.
- Package the application as a ZIP or DMG.

## 6. Media delivery guidelines

These are defaults, not creative restrictions. Codex will provide more precise
requirements for individual assets when their use is known.

### Images

- Preferred delivery: PNG for lossless images and transparency.
- Preserve layered source files separately when useful; Unity receives exported
  flattened assets unless layers are specifically required.
- Full-screen backgrounds should normally be created at 1920x1080 or at a higher
  resolution with the same aspect ratio.
- Characters intended as overlays should use transparent backgrounds.
- Keep important content away from the extreme edges so modest aspect-ratio
  differences do not crop critical information.
- Use consistent filenames, for example:
  `cam_kitchen_character_a_01.png`.

### Animation

Animation may be supplied as:

- A numbered PNG sequence.
- A sprite sheet with documented frame dimensions and order.
- Separate layered pieces intended for code/Unity animation.
- Video only when its advantages outweigh size, codec, and platform concerns.

Codex will specify the desired frame rate, size, alpha requirement, and loop
behavior before final animation work begins.

### Audio

- Preferred master delivery: WAV, 48 kHz, 16-bit or 24-bit.
- Keep an uncompressed master outside Unity even if Unity compresses the runtime
  version.
- Provide clean loop boundaries for ambience and music intended to loop.
- Avoid baking volume differences into files when they should be controlled by
  the game's mixer.
- Use descriptive filenames, for example:
  `sfx_monitor_open_01.wav`.

Codex will configure import compression, looping, mixer groups, random variation,
and playback behavior.

### Character design handoff

For each character, the creator should eventually decide:

- Name and visual concept.
- Starting location.
- Possible locations and movement route.
- What clues reveal its state or position.
- What causes it to advance or retreat.
- How the player prevents or survives its attack.
- How its difficulty changes between nights.
- Required camera images, office images, sounds, and attack animation.

Codex will translate those decisions into a testable behavior specification and
implementation. Ambiguous or contradictory behavior will be raised for a creator
decision rather than silently invented.

## 7. macOS distribution levels

### Internal development build

Suitable for testing on the development Mac. No signing or polished packaging is
required.

### Informal friend build

The application can be zipped and sent directly. If it is unsigned or not
notarized, macOS Gatekeeper may warn or block the first launch, requiring the
recipient to explicitly approve it in macOS security settings. This is usable for
trusted testers but is not the desired final experience.

### Properly shippable friend/public build

For a normal first-launch experience, the release should be:

- Code-signed with a Developer ID Application certificate.
- Submitted to Apple for notarization.
- Stapled with the notarization result when appropriate.
- Packaged without losing its signature or executable permissions.

This requires an active Apple Developer Program membership and access to the
creator's signing identity. Codex can configure the project and prepare or run the
commands in the creator's environment, but the creator owns the Apple account,
credentials, certificates, and final authorization to sign and submit the game.
Secrets will not be committed to Git.

## 8. Testing requirements

Before calling a version shippable, verify:

- A new game can be started and completed.
- Every intended character can win and can be countered correctly.
- All camera transitions and interaction zones work.
- The game behaves correctly after losing focus or being paused.
- Audio remains controllable and does not unexpectedly stack or clip.
- Settings and progress survive restarting the application.
- Corrupt or missing save data fails safely.
- Common 16:9 resolutions work without displaced controls.
- At least one non-16:9 display is usable or receives deliberate letterboxing.
- Windowed and fullscreen modes behave correctly.
- The game starts on a clean Mac without Unity installed.
- The packaged application passes a Gatekeeper assessment after signing and
  notarization.
- Release logs contain no repeated exceptions or missing-asset errors.

## 9. Definition of the first complete release

The first release is complete when:

- It contains the agreed number of nights and characters.
- All final creative assets approved for that release are integrated.
- There are no known progress-blocking or crash-level defects.
- The game has working menus, settings, saves, win/loss flows, and credits.
- A release build has been tested outside the Unity editor.
- The macOS application has been packaged and, for a clean distribution
  experience, signed and notarized.
- A friend can receive the package, open it, understand how to play, and complete
  the game without developer assistance.

## 10. Immediate next steps

1. Create and verify the placeholder main menu.
2. Make a macOS build and launch it outside the editor.
3. Complete the highest-priority decisions in `DESIGN_BRIEF.md`.
4. Build the one-enemy grey-box vertical slice before requesting final art.

This plan is a living document. Significant technical or scope decisions should
be reflected here so that the implementation and the creator's expectations stay
aligned.
