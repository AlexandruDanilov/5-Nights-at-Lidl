# Missing Audio Assets

This file tracks sounds that are not currently implemented. Suggested paths are
only naming targets; adding a file at one of these paths will not wire it into
the game automatically.

For seamless loops, prefer WAV or OGG over MP3. Short one-shots can use any
Unity-supported format.

## High priority

| Suggested asset | Use | Playback | Useful search terms |
| --- | --- | --- | --- |
| `Assets/Game/Audio/Ambience/Camera_HumLoop.ogg` | CCTV dashboard is open | Seamless loop | CCTV electrical hum, CRT static ambience |
| `Assets/Game/Audio/SFX/Power_LowWarning.wav` | Battery reaches a low threshold | Repeating one-shot or loop | low battery warning beep, electrical warning pulse |
| `Assets/Game/Audio/SFX/Power_Out.wav` | Battery reaches 0% and the office shuts down | One-shot | electrical power failure, transformer shutdown |

## Threat feedback

| Suggested asset | Use | Playback | Useful search terms |
| --- | --- | --- | --- |
| `Assets/Game/Audio/SFX/Selly_Move.wav` | Selly changes rooms | Short one-shot | distant footsteps, supermarket aisle shuffle |
| `Assets/Game/Audio/SFX/Selly_Retreat.wav` | Closed right door makes Selly retreat | One-shot | metal door bang, footsteps walking away |
| `Assets/Game/Audio/SFX/Cabral_Move.wav` | Cabral changes rooms | Short one-shot | heavy distant footsteps, store shelf movement |
| `Assets/Game/Audio/SFX/Cabral_Retreat.wav` | Closed left door makes Cabral retreat | One-shot | heavy door bang, footsteps retreating |
| `Assets/Game/Audio/SFX/Threat_CameraGlitch.wav` | A threat appears or disappears on a camera | Short one-shot | analog camera glitch, CCTV interference hit |
| `Assets/Game/Audio/SFX/Threat_DoorArrival.wav` | Selly or Cabral first reaches an office doorway | One-shot | distant impact, ominous footstep stop |
| `Assets/Game/Audio/SFX/Dorian_Charge.wav` | Blender charge control is held | Loop or repeating pulse | appliance motor wind-up, blender button mechanism |

## Office and interface polish

| Suggested asset | Use | Playback | Useful search terms |
| --- | --- | --- | --- |
| `Assets/Game/Audio/Ambience/Office_RoomToneLoop.ogg` | Office environmental layer beneath the music | Seamless loop | fluorescent light hum, ventilation room tone, empty supermarket ambience |
| `Assets/Game/Audio/SFX/Control_Disabled.wav` | Door or light button is pressed with no power | One-shot | dead switch click, button error click |
| `Assets/Game/Audio/SFX/Hour_Chime.wav` | In-game hour changes | One-shot | clock hour chime, digital clock beep |
| `Assets/Game/Audio/SFX/Phone_Ring.wav` | Before the Night 1 call connects, if the recording does not already contain it | One-shot | desk phone ring, old telephone ring |
| `Assets/Game/Audio/SFX/Phone_Connect.wav` | Phone message starts or is muted | One-shot | telephone pickup click, call disconnect click |
| `Assets/Game/Audio/UI/Menu_Hover.wav` | Menu selection changes | One-shot | horror menu hover, soft UI tick |
| `Assets/Game/Audio/UI/Menu_Confirm.wav` | Menu option is activated | One-shot | horror UI confirm, muted button confirm |
| `Assets/Game/Audio/UI/Menu_Back.wav` | Menu or options screen is closed | One-shot | UI back click, menu cancel sound |

## Intentionally silent

- The girls' disappearance has no separate audio cue. **FAS FAS** now plays
  `FasFas_Spray.mp3` on every valid press from Night 4 onward and immediately
  stops `Girls_Taro.mp3` when the pair is present.
- Automatic door/light shutdown during the end-of-night fade does not reuse the
  manual door and light button sounds.

## Already supplied and implemented

- Night 1 and Night 4 phone calls
- Global manele background track
- Door and light controls
- Camera monitor open/close and camera feed switching
- FAS FAS spray bottle
- Selly and Cabral doorway warning sounds
- Dorian blender and final-stage alarm
- Girls' Taro office-presence sound
- Universal jumpscare sound
- 6 AM clock-animation victory chime
