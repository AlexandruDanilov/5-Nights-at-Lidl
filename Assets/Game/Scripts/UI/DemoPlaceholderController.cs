using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace FiveNightsAtLidl.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIDocument))]
    public sealed class DemoPlaceholderController : MonoBehaviour
    {
        private const string MainMenuSceneName = "MainMenu";
        private const float SecondsPerGameHour = 60f;
        private const float OriginalSecondsPerGameHour = 90f;
        private const int FinalHour = 6;
        private const float StartingBatteryPercent = 100f;
        private const float OriginalUsageDrainPerSecond = 0.1f;
        private const float NightLengthDrainScale =
            OriginalSecondsPerGameHour / SecondsPerGameHour;
        private const float StandardUsageDrainPerSecond =
            OriginalUsageDrainPerSecond * NightLengthDrainScale;
        private const float CameraUsageDrainPerSecond =
            StandardUsageDrainPerSecond * 0.5f;
        private const int CriticalBatteryMaximum = 20;
        private const int WarningBatteryMaximum = 70;
        private const float PreNightTitleDurationSeconds = 3f;
        private const string BolojanCameraOneId = "CAM 01";
        private const string BolojanCameraSevenId = "CAM 07";
        private const float DefaultBolojanBatteryTaxPercent = 20f;
        private const float BolojanTaxTextStartSeconds = 0.12f;
        private const float BolojanTaxTextEndSeconds = 1.2f;

        [SerializeField, Min(1)]
        private int _nightNumber = 1;

        [SerializeField]
        private PanelSettings _fallbackPanelSettings;

        [SerializeField]
        private Texture2D _officeBackground;

        [SerializeField]
        private Texture2D _leftLightBackground;

        [SerializeField]
        private Texture2D _rightLightBackground;

        [SerializeField]
        private Texture2D _leftDoorBackground;

        [SerializeField]
        private Texture2D _rightDoorBackground;

        [SerializeField]
        private Texture2D _dairyCameraFeed;

        [SerializeField]
        private Texture2D _bakeryCameraFeed;

        [SerializeField]
        private Texture2D _parksideCameraFeed;

        [SerializeField]
        private Texture2D _detergentsCameraFeed;

        [SerializeField]
        private Texture2D _iceCreamCameraFeed;

        [SerializeField]
        private Texture2D _checkoutsCameraFeed;

        [SerializeField]
        private Texture2D _stockroomCameraFeed;

        [SerializeField]
        private Texture2D _employeeRoomCameraFeed;

        [Header("Bolojan Threat")]
        [SerializeField]
        private Texture2D _bolojanCameraOneTexture;

        [SerializeField]
        private Texture2D _bolojanCameraSevenTexture;

        [SerializeField]
        private Texture2D _bolojanJumpscareTexture;

        [SerializeField, Range(0f, 1f)]
        [Tooltip("Chance that Bolojan appears whenever CAM 01 or CAM 07 is entered.")]
        private float _bolojanAppearanceChance = 0.1f;

        [SerializeField, Min(0.1f)]
        [Tooltip("Uninterrupted seconds spent looking at Bolojan before he attacks.")]
        private float _bolojanExposureSeconds = 2f;

        [SerializeField, Min(0.5f)]
        private float _bolojanJumpscareDurationSeconds = 1.35f;

        [SerializeField, Range(0f, 100f)]
        [Tooltip("Battery percentage points removed when the Bolojan tax lands.")]
        private float _bolojanBatteryTaxPercent = DefaultBolojanBatteryTaxPercent;

        private Label _clockLabel;
        private Label _batteryLabel;
        private Label _activeCameraLabel;
        private Label _feedPlaceholderLabel;
        private VisualElement _nightIntroOverlay;
        private VisualElement _officeControlsLayer;
        private VisualElement _cameraDashboard;
        private VisualElement _cameraFeedViewport;
        private VisualElement _cameraFlickerOverlay;
        private VisualElement _cameraScanBar;
        private VisualElement _bolojanJumpscareOverlay;
        private Image _leftOfficeStateImage;
        private Image _rightOfficeStateImage;
        private Image _cameraFeedImage;
        private Image _bolojanCameraImage;
        private Image _bolojanJumpscareImage;
        private Label _bolojanTaxLabel;
        private Button _leftLightButton;
        private Button _rightLightButton;
        private Button _leftDoorButton;
        private Button _rightDoorButton;
        private Button _selectedCameraButton;
        private float _elapsedNightSeconds;
        private float _batteryRemaining;
        private float _nextCameraFeedFlickerAt;
        private float _cameraFeedFlickerUntil;
        private float _preNightTitleRemaining;
        private float _bolojanExposureElapsed;
        private float _bolojanJumpscareElapsed;
        private float _bolojanCameraBaseLeft;
        private float _bolojanCameraBaseTop;
        private int _displayedHour = -1;
        private int _displayedBatteryPercent = -1;
        private bool _leftLightOn;
        private bool _rightLightOn;
        private bool _leftDoorClosed;
        private bool _rightDoorClosed;
        private bool _cameraDashboardOpen;
        private bool _cameraFeedActive;
        private bool _nightStarted;
        private bool _nightEnded;
        private bool _bolojanVisible;
        private bool _bolojanJumpscareActive;
        private string _activeCameraId = BolojanCameraOneId;
        private readonly HashSet<string> _roomsOccupiedByOtherThreats = new HashSet<string>();

        private void OnEnable()
        {
            if (!EnsurePanelSettings())
            {
                return;
            }

            _elapsedNightSeconds = 0f;
            _batteryRemaining = StartingBatteryPercent;
            _displayedBatteryPercent = -1;
            _leftLightOn = false;
            _rightLightOn = false;
            _leftDoorClosed = false;
            _rightDoorClosed = false;
            _cameraDashboardOpen = false;
            _cameraFeedActive = false;
            _nightStarted = false;
            _nightEnded = false;
            _bolojanVisible = false;
            _bolojanJumpscareActive = false;
            _bolojanExposureElapsed = 0f;
            _bolojanJumpscareElapsed = 0f;
            _activeCameraId = BolojanCameraOneId;
            _roomsOccupiedByOtherThreats.Clear();
            _preNightTitleRemaining = PreNightTitleDurationSeconds;
            BuildInterface();
            UpdateClock(force: true);
            UpdateBatteryReadout();
        }

        private bool EnsurePanelSettings()
        {
            UIDocument document = GetComponent<UIDocument>();
            if (document.panelSettings != null)
            {
                return true;
            }

            if (_fallbackPanelSettings != null)
            {
                document.panelSettings = _fallbackPanelSettings;
                return true;
            }

            Debug.LogError("The gameplay UI has no Panel Settings assigned.", this);
            return false;
        }

        private void Update()
        {
            if (!_nightStarted)
            {
                _preNightTitleRemaining -= Time.unscaledDeltaTime;
                if (_preNightTitleRemaining <= 0f)
                {
                    BeginNight();
                }

                return;
            }

            if (_bolojanJumpscareActive)
            {
                AnimateBolojanJumpscare(Time.unscaledDeltaTime);
                return;
            }

            if (_nightEnded)
            {
                return;
            }

            AnimateCameraFeed();

            if (_displayedHour >= FinalHour)
            {
                DismissBolojan();
                return;
            }

            float elapsedSeconds = Time.deltaTime;
            UpdateBolojanThreat(elapsedSeconds);
            if (_nightEnded)
            {
                return;
            }

            _elapsedNightSeconds += elapsedSeconds;
            UpdateClock(force: false);
            if (_displayedHour < FinalHour)
            {
                DrainBattery(elapsedSeconds);
            }
        }

        private void BuildInterface()
        {
            UIDocument document = GetComponent<UIDocument>();
            VisualElement root = MenuUiFactory.PrepareRoot(document, "demo-placeholder-root");
            root.AddToClassList("screen-root");
            root.AddToClassList("gameplay-placeholder");
            root.RegisterCallback<KeyDownEvent>(keyEvent =>
            {
                if (keyEvent.keyCode == KeyCode.Escape)
                {
                    if (_bolojanJumpscareActive)
                    {
                        keyEvent.StopPropagation();
                        return;
                    }

                    if (IsCameraDashboardOpen())
                    {
                        HideCameraDashboard();
                    }
                    else
                    {
                        ReturnToMenu();
                    }

                    keyEvent.StopPropagation();
                }
            });

            if (_officeBackground != null)
            {
                Image officeBackground = new Image
                {
                    name = "office-background",
                    image = _officeBackground,
                    scaleMode = ScaleMode.ScaleAndCrop,
                    pickingMode = PickingMode.Ignore
                };
                officeBackground.AddToClassList("gameplay-background");
                root.Insert(0, officeBackground);
            }
            else
            {
                Debug.LogError("The gameplay scene has no office background assigned.", this);
            }

            BuildOfficeStateLayer(root);
            BuildOfficeControls(root);

            VisualElement hud = new VisualElement();
            hud.AddToClassList("demo-hud");
            _clockLabel = MenuUiFactory.CreateLabel("12 AM", "clock-label");
            hud.Add(_clockLabel);
            root.Add(hud);

            VisualElement batteryHud = new VisualElement();
            batteryHud.AddToClassList("battery-hud");
            batteryHud.Add(MenuUiFactory.CreateLabel("POWER LEFT", "battery-caption"));
            _batteryLabel = MenuUiFactory.CreateLabel("100%", "battery-value");
            batteryHud.Add(_batteryLabel);
            root.Add(batteryHud);

            BuildCameraDashboard(root);
            hud.BringToFront();
            batteryHud.BringToFront();
            BuildNightIntro(root);
            BuildBolojanJumpscare(root);
            root.schedule.Execute(root.Focus);
        }

        private void BuildNightIntro(VisualElement root)
        {
            _nightIntroOverlay = new VisualElement { name = "night-intro-overlay" };
            _nightIntroOverlay.AddToClassList("night-intro-overlay");
            _nightIntroOverlay.Add(MenuUiFactory.CreateLabel(
                $"NIGHT {_nightNumber}",
                "night-intro-title"));
            root.Add(_nightIntroOverlay);
            _nightIntroOverlay.BringToFront();
        }

        private void BeginNight()
        {
            _nightStarted = true;
            if (_nightIntroOverlay != null)
            {
                _nightIntroOverlay.style.display = DisplayStyle.None;
            }
        }

        private void BuildBolojanJumpscare(VisualElement root)
        {
            _bolojanJumpscareOverlay = new VisualElement { name = "bolojan-jumpscare-overlay" };
            _bolojanJumpscareOverlay.AddToClassList("bolojan-jumpscare-overlay");
            _bolojanJumpscareOverlay.style.display = DisplayStyle.None;

            _bolojanJumpscareImage = new Image
            {
                name = "bolojan-jumpscare-image",
                image = _bolojanJumpscareTexture,
                scaleMode = ScaleMode.ScaleToFit,
                pickingMode = PickingMode.Ignore
            };
            _bolojanJumpscareImage.AddToClassList("bolojan-jumpscare-image");
            _bolojanJumpscareOverlay.Add(_bolojanJumpscareImage);

            _bolojanTaxLabel = MenuUiFactory.CreateLabel(
                "BOLOJAN TAX",
                "bolojan-tax-flash");
            _bolojanTaxLabel.style.display = DisplayStyle.None;
            _bolojanJumpscareOverlay.Add(_bolojanTaxLabel);

            root.Add(_bolojanJumpscareOverlay);
        }

        private void BuildOfficeControls(VisualElement root)
        {
            _officeControlsLayer = new VisualElement { name = "office-controls-layer" };
            _officeControlsLayer.AddToClassList("office-controls-layer");

            _officeControlsLayer.Add(CreateSideControls("left"));
            _officeControlsLayer.Add(CreateSideControls("right"));

            Button cameraButton = new Button(ShowCameraDashboard)
            {
                name = "raise-camera-button",
                text = "OPEN CAMERAS",
                tooltip = "Raise the camera monitor"
            };
            cameraButton.AddToClassList("camera-toggle-button");
            _officeControlsLayer.Add(cameraButton);

            root.Add(_officeControlsLayer);
        }

        private void BuildOfficeStateLayer(VisualElement root)
        {
            VisualElement stateLayer = new VisualElement { name = "office-state-layer" };
            stateLayer.pickingMode = PickingMode.Ignore;
            stateLayer.AddToClassList("office-state-layer");

            VisualElement leftClip = CreateOfficeStateClip("left", out _leftOfficeStateImage);
            VisualElement rightClip = CreateOfficeStateClip("right", out _rightOfficeStateImage);
            stateLayer.Add(leftClip);
            stateLayer.Add(rightClip);
            root.Insert(1, stateLayer);

            UpdateOfficeVisualState();
        }

        private static VisualElement CreateOfficeStateClip(string side, out Image stateImage)
        {
            VisualElement clip = new VisualElement { name = $"{side}-office-state" };
            clip.pickingMode = PickingMode.Ignore;
            clip.AddToClassList("office-state-clip");
            clip.AddToClassList($"office-state-{side}");

            stateImage = new Image
            {
                name = $"{side}-office-state-image",
                scaleMode = ScaleMode.ScaleAndCrop,
                pickingMode = PickingMode.Ignore
            };
            stateImage.AddToClassList("office-state-image");
            clip.Add(stateImage);
            return clip;
        }

        private VisualElement CreateSideControls(string side)
        {
            bool isLeft = side == "left";
            VisualElement controls = new VisualElement
            {
                name = $"{side}-office-controls"
            };
            controls.AddToClassList("office-controls");
            controls.AddToClassList($"office-controls-{side}");

            Button doorButton = CreateOfficeControlButton(
                side,
                "DOOR",
                "door-control-button",
                () => ToggleDoor(isLeft));
            Button lightButton = CreateOfficeControlButton(
                side,
                "LIGHT",
                "light-control-button",
                () => ToggleLight(isLeft));

            if (isLeft)
            {
                _leftDoorButton = doorButton;
                _leftLightButton = lightButton;
            }
            else
            {
                _rightDoorButton = doorButton;
                _rightLightButton = lightButton;
            }

            controls.Add(doorButton);
            controls.Add(lightButton);
            return controls;
        }

        private static Button CreateOfficeControlButton(
            string side,
            string label,
            string className,
            System.Action clicked)
        {
            Button button = new Button(clicked)
            {
                name = $"{side}-{label.ToLowerInvariant()}-button",
                text = label,
                tooltip = $"Toggle the {side} {label.ToLowerInvariant()}"
            };
            button.AddToClassList("office-control-button");
            button.AddToClassList(className);
            return button;
        }

        private void ToggleLight(bool isLeft)
        {
            if (isLeft)
            {
                _leftLightOn = !_leftLightOn;
                if (_leftLightOn)
                {
                    _rightLightOn = false;
                }
            }
            else
            {
                _rightLightOn = !_rightLightOn;
                if (_rightLightOn)
                {
                    _leftLightOn = false;
                }
            }

            RefreshLightButtonStates();
            UpdateOfficeVisualState();
        }

        private void TurnOffLights()
        {
            _leftLightOn = false;
            _rightLightOn = false;
            RefreshLightButtonStates();
            UpdateOfficeVisualState();
        }

        private void RefreshLightButtonStates()
        {
            _leftLightButton?.EnableInClassList("office-control-active", _leftLightOn);
            _rightLightButton?.EnableInClassList("office-control-active", _rightLightOn);
        }

        private void ToggleDoor(bool isLeft)
        {
            if (isLeft)
            {
                _leftDoorClosed = !_leftDoorClosed;
                _leftDoorButton.EnableInClassList("office-control-active", _leftDoorClosed);
            }
            else
            {
                _rightDoorClosed = !_rightDoorClosed;
                _rightDoorButton.EnableInClassList("office-control-active", _rightDoorClosed);
            }

            UpdateOfficeVisualState();
        }

        private void UpdateOfficeVisualState()
        {
            if (_leftOfficeStateImage == null || _rightOfficeStateImage == null)
            {
                return;
            }

            Texture2D leftState = _leftDoorClosed
                ? _leftDoorBackground
                : _leftLightOn ? _leftLightBackground : null;
            Texture2D rightState = _rightDoorClosed
                ? _rightDoorBackground
                : _rightLightOn ? _rightLightBackground : null;

            ApplyOfficeState(_leftOfficeStateImage, leftState);
            ApplyOfficeState(_rightOfficeStateImage, rightState);
        }

        private static void ApplyOfficeState(Image stateImage, Texture2D stateTexture)
        {
            stateImage.image = stateTexture;
            stateImage.style.display = stateTexture == null
                ? DisplayStyle.None
                : DisplayStyle.Flex;
        }

        private void BuildCameraDashboard(VisualElement root)
        {
            _cameraDashboard = new VisualElement { name = "camera-dashboard" };
            _cameraDashboard.AddToClassList("camera-dashboard");
            _cameraDashboard.style.display = DisplayStyle.None;

            VisualElement topBar = new VisualElement();
            topBar.AddToClassList("camera-top-bar");
            topBar.Add(MenuUiFactory.CreateLabel("SURVEILLANCE SYSTEM", "camera-dashboard-title"));
            _activeCameraLabel = MenuUiFactory.CreateLabel(
                "CAM 01 / DAIRY",
                "active-camera-label");
            topBar.Add(_activeCameraLabel);
            _cameraDashboard.Add(topBar);

            VisualElement workspace = new VisualElement();
            workspace.AddToClassList("camera-workspace");

            VisualElement feedPanel = new VisualElement();
            feedPanel.AddToClassList("camera-feed-panel");
            feedPanel.Add(MenuUiFactory.CreateLabel("LIVE FEED", "camera-section-label"));

            _cameraFeedViewport = new VisualElement { name = "camera-feed-viewport" };
            _cameraFeedViewport.AddToClassList("camera-feed-viewport");

            _cameraFeedImage = new Image
            {
                name = "camera-feed-image",
                scaleMode = ScaleMode.ScaleAndCrop,
                pickingMode = PickingMode.Ignore
            };
            _cameraFeedImage.AddToClassList("camera-feed-image");
            _cameraFeedViewport.Add(_cameraFeedImage);

            _bolojanCameraImage = new Image
            {
                name = "bolojan-camera-image",
                scaleMode = ScaleMode.ScaleAndCrop,
                pickingMode = PickingMode.Ignore
            };
            _bolojanCameraImage.AddToClassList("bolojan-camera-image");
            _bolojanCameraImage.style.display = DisplayStyle.None;
            _cameraFeedViewport.Add(_bolojanCameraImage);

            _feedPlaceholderLabel = MenuUiFactory.CreateLabel(
                "CAM 01\nDAIRY\nFEED NOT CONNECTED",
                "camera-feed-placeholder");
            _cameraFeedViewport.Add(_feedPlaceholderLabel);

            _cameraFlickerOverlay = new VisualElement { name = "camera-feed-flicker" };
            _cameraFlickerOverlay.pickingMode = PickingMode.Ignore;
            _cameraFlickerOverlay.AddToClassList("camera-feed-flicker");
            _cameraFeedViewport.Add(_cameraFlickerOverlay);

            _cameraScanBar = new VisualElement { name = "camera-feed-scan-bar" };
            _cameraScanBar.pickingMode = PickingMode.Ignore;
            _cameraScanBar.AddToClassList("camera-feed-scan-bar");
            _cameraFeedViewport.Add(_cameraScanBar);

            feedPanel.Add(_cameraFeedViewport);
            UpdateCameraFeed("CAM 01", "DAIRY");
            workspace.Add(feedPanel);

            VisualElement mapPanel = new VisualElement();
            mapPanel.AddToClassList("camera-map-panel");
            mapPanel.Add(MenuUiFactory.CreateLabel(
                "STORE MAP / PROVISIONAL PATHS",
                "camera-section-label"));

            VisualElement map = new VisualElement { name = "camera-map" };
            map.AddToClassList("camera-map");

            AddMapRoom(map, "DAIRY", 4f, 4f, 68f, 12f);
            AddMapRoom(map, "BAKERY", 4f, 20f, 13f, 34f);
            AddMapRoom(map, "PARKSIDE", 21f, 20f, 13f, 34f);
            AddMapRoom(map, "DETERGENTS", 38f, 20f, 13f, 34f);
            AddMapRoom(map, "ICE CREAM", 55f, 20f, 17f, 34f);
            AddMapRoom(map, "STOCKROOM", 76f, 18f, 20f, 69f);
            AddMapRoom(map, "CHECKOUTS", 4f, 58f, 27f, 30f);
            AddMapRoom(map, "OFFICE", 34f, 77f, 14f, 18f);
            AddMapRoom(map, "EMPLOYEE ROOM", 51f, 64f, 22f, 24f);

            AddCameraPathSegment(map, 10.6f, 15f, 0.8f, 5f);
            AddCameraPathSegment(map, 27.6f, 15f, 0.8f, 5f);
            AddCameraPathSegment(map, 44.6f, 15f, 0.8f, 5f);
            AddCameraPathSegment(map, 63.6f, 15f, 0.8f, 5f);
            AddCameraPathSegment(map, 16.5f, 45.5f, 5f, 0.8f);
            AddCameraPathSegment(map, 33.5f, 45.5f, 5f, 0.8f);
            AddCameraPathSegment(map, 50.5f, 45.5f, 6.5f, 0.8f);
            AddCameraPathSegment(map, 10.6f, 54f, 0.8f, 4f);
            AddCameraPathSegment(map, 72f, 38.6f, 4f, 0.8f);
            AddCameraPathSegment(map, 31f, 82f, 3f, 0.8f);
            AddCameraPathSegment(map, 48f, 82f, 3f, 0.8f);
            AddCameraPathSegment(map, 73f, 78f, 3f, 0.8f);

            CreateCameraNode(map, "CAM 01", "DAIRY", 56f, 7f, selectedByDefault: true);
            CreateCameraNode(map, "CAM 02", "BAKERY", 4.5f, 43f);
            CreateCameraNode(map, "CAM 03", "PARKSIDE", 21.5f, 43f);
            CreateCameraNode(map, "CAM 04", "DETERGENTS", 38.5f, 43f);
            CreateCameraNode(map, "CAM 05", "ICE CREAM", 57f, 43f);
            CreateCameraNode(map, "CAM 06", "CHECKOUTS", 7f, 78f);
            CreateCameraNode(map, "CAM 07", "STOCKROOM", 79f, 72f);
            CreateCameraNode(map, "CAM 08", "EMPLOYEE ROOM", 55f, 78f);
            AddPlayerMarker(map, 36f, 86f);
            mapPanel.Add(map);
            workspace.Add(mapPanel);
            _cameraDashboard.Add(workspace);

            Button lowerMonitorButton = new Button(HideCameraDashboard)
            {
                name = "lower-camera-button",
                text = "LOWER MONITOR"
            };
            lowerMonitorButton.AddToClassList("camera-toggle-button");
            lowerMonitorButton.AddToClassList("lower-camera-button");
            _cameraDashboard.Add(lowerMonitorButton);

            root.Add(_cameraDashboard);
        }

        private static void AddMapRoom(
            VisualElement map,
            string label,
            float left,
            float top,
            float width,
            float height)
        {
            VisualElement room = new VisualElement();
            room.AddToClassList("camera-map-room");
            room.style.left = new Length(left, LengthUnit.Percent);
            room.style.top = new Length(top, LengthUnit.Percent);
            room.style.width = new Length(width, LengthUnit.Percent);
            room.style.height = new Length(height, LengthUnit.Percent);
            room.Add(MenuUiFactory.CreateLabel(label, "camera-map-room-label"));
            map.Add(room);
        }

        private static void AddPlayerMarker(VisualElement map, float left, float top)
        {
            Label marker = MenuUiFactory.CreateLabel("YOU", "camera-player-marker");
            marker.style.left = new Length(left, LengthUnit.Percent);
            marker.style.top = new Length(top, LengthUnit.Percent);
            map.Add(marker);
        }

        private static void AddCameraPathSegment(
            VisualElement map,
            float left,
            float top,
            float width,
            float height)
        {
            VisualElement path = new VisualElement();
            path.pickingMode = PickingMode.Ignore;
            path.AddToClassList("camera-route-segment");
            path.style.left = new Length(left, LengthUnit.Percent);
            path.style.top = new Length(top, LengthUnit.Percent);
            path.style.width = new Length(width, LengthUnit.Percent);
            path.style.height = new Length(height, LengthUnit.Percent);
            map.Add(path);
        }

        private void CreateCameraNode(
            VisualElement map,
            string cameraId,
            string locationName,
            float left,
            float top,
            bool selectedByDefault = false)
        {
            Button cameraButton = null;
            cameraButton = new Button(() => SelectCamera(cameraButton, cameraId, locationName))
            {
                name = $"{cameraId.ToLowerInvariant().Replace(' ', '-')}-button",
                text = cameraId,
                tooltip = $"View {locationName}"
            };
            cameraButton.AddToClassList("camera-node-button");
            cameraButton.style.left = new Length(left, LengthUnit.Percent);
            cameraButton.style.top = new Length(top, LengthUnit.Percent);
            map.Add(cameraButton);

            if (selectedByDefault)
            {
                _selectedCameraButton = cameraButton;
                cameraButton.AddToClassList("camera-node-selected");
            }
        }

        private void SelectCamera(Button cameraButton, string cameraId, string locationName)
        {
            if (_nightEnded)
            {
                return;
            }

            DismissBolojan();
            _selectedCameraButton?.RemoveFromClassList("camera-node-selected");
            _selectedCameraButton = cameraButton;
            _selectedCameraButton.AddToClassList("camera-node-selected");
            _activeCameraLabel.text = $"{cameraId} / {locationName}";
            UpdateCameraFeed(cameraId, locationName);
            TryShowBolojan(cameraId);
        }

        private void UpdateCameraFeed(string cameraId, string locationName)
        {
            _activeCameraId = cameraId;
            Texture2D feedTexture = null;
            if (cameraId == "CAM 01")
            {
                feedTexture = _dairyCameraFeed;
            }
            else if (cameraId == "CAM 02")
            {
                feedTexture = _bakeryCameraFeed;
            }
            else if (cameraId == "CAM 03")
            {
                feedTexture = _parksideCameraFeed;
            }
            else if (cameraId == "CAM 04")
            {
                feedTexture = _detergentsCameraFeed;
            }
            else if (cameraId == "CAM 05")
            {
                feedTexture = _iceCreamCameraFeed;
            }
            else if (cameraId == "CAM 06")
            {
                feedTexture = _checkoutsCameraFeed;
            }
            else if (cameraId == "CAM 07")
            {
                feedTexture = _stockroomCameraFeed;
            }
            else if (cameraId == "CAM 08")
            {
                feedTexture = _employeeRoomCameraFeed;
            }

            _cameraFeedActive = feedTexture != null;
            _cameraFeedImage.image = feedTexture;
            _cameraFeedImage.style.display = _cameraFeedActive
                ? DisplayStyle.Flex
                : DisplayStyle.None;
            _feedPlaceholderLabel.style.display = _cameraFeedActive
                ? DisplayStyle.None
                : DisplayStyle.Flex;
            _cameraFlickerOverlay.style.display = _cameraFeedActive
                ? DisplayStyle.Flex
                : DisplayStyle.None;
            _cameraScanBar.style.display = _cameraFeedActive
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            if (!_cameraFeedActive)
            {
                _feedPlaceholderLabel.text = $"{cameraId}\n{locationName}\nFEED NOT CONNECTED";
                return;
            }

            float now = Time.unscaledTime;
            _nextCameraFeedFlickerAt = now + Random.Range(0.7f, 1.8f);
            _cameraFeedFlickerUntil = 0f;
            _cameraFeedImage.style.left = new Length(-2.5f, LengthUnit.Percent);
            _cameraFeedImage.style.top = new Length(-2.5f, LengthUnit.Percent);
        }

        private void AnimateCameraFeed()
        {
            if (!_cameraDashboardOpen || !_cameraFeedActive || _cameraFeedImage == null)
            {
                return;
            }

            float now = Time.unscaledTime;
            float panX = -2.5f + Mathf.Sin(now * 0.18f) * 1.15f;
            float panY = -2.5f + Mathf.Sin(now * 0.13f + 1.1f) * 0.75f;
            _cameraFeedImage.style.left = new Length(panX, LengthUnit.Percent);
            _cameraFeedImage.style.top = new Length(panY, LengthUnit.Percent);

            if (now >= _nextCameraFeedFlickerAt)
            {
                _cameraFeedFlickerUntil = now + Random.Range(0.025f, 0.075f);
                _nextCameraFeedFlickerAt = now + Random.Range(0.9f, 2.5f);
            }

            bool isFlickering = now < _cameraFeedFlickerUntil;
            float quietNoise = Mathf.PerlinNoise(now * 7.3f, 0.37f);
            _cameraFeedImage.style.opacity = isFlickering
                ? Random.Range(0.82f, 0.91f)
                : 0.96f + (quietNoise - 0.5f) * 0.025f;
            _cameraFlickerOverlay.style.opacity = isFlickering
                ? 0.09f
                : 0.008f + quietNoise * 0.015f;

            float scanPosition = (now * 3.5f) % 105f - 2.5f;
            _cameraScanBar.style.top = new Length(scanPosition, LengthUnit.Percent);

            if (_bolojanVisible && _bolojanCameraImage != null)
            {
                float threatDriftX = Mathf.Sin(now * 2.15f) * 0.16f;
                float threatDriftY = Mathf.Sin(now * 1.47f + 0.8f) * 0.11f;
                _bolojanCameraImage.style.left = new Length(
                    _bolojanCameraBaseLeft + threatDriftX,
                    LengthUnit.Percent);
                _bolojanCameraImage.style.top = new Length(
                    _bolojanCameraBaseTop + threatDriftY,
                    LengthUnit.Percent);
                _bolojanCameraImage.style.opacity = isFlickering
                    ? Random.Range(0.84f, 0.94f)
                    : 0.985f + quietNoise * 0.015f;
            }
        }

        private void ShowCameraDashboard()
        {
            if (_nightEnded)
            {
                return;
            }

            TurnOffLights();
            _cameraDashboardOpen = true;
            _officeControlsLayer.style.display = DisplayStyle.None;
            _cameraDashboard.style.display = DisplayStyle.Flex;
            _cameraDashboard.BringToFront();
            _clockLabel.parent.BringToFront();
            _batteryLabel.parent.BringToFront();
            TryShowBolojan(_activeCameraId);
        }

        private void HideCameraDashboard()
        {
            DismissBolojan();
            _cameraDashboardOpen = false;
            _cameraDashboard.style.display = DisplayStyle.None;
            _officeControlsLayer.style.display = DisplayStyle.Flex;
            _officeControlsLayer.BringToFront();
            _clockLabel.parent.BringToFront();
            _batteryLabel.parent.BringToFront();
        }

        private bool IsCameraDashboardOpen()
        {
            return _cameraDashboardOpen;
        }

        private void TryShowBolojan(string cameraId)
        {
            if (!_cameraDashboardOpen || _nightEnded || _bolojanVisible)
            {
                return;
            }

            bool isEligibleRoom = cameraId == BolojanCameraOneId ||
                cameraId == BolojanCameraSevenId;
            if (!isEligibleRoom || _roomsOccupiedByOtherThreats.Contains(cameraId))
            {
                return;
            }

            Texture2D appearanceTexture = cameraId == BolojanCameraOneId
                ? _bolojanCameraOneTexture
                : _bolojanCameraSevenTexture;
            if (appearanceTexture == null || Random.value >= _bolojanAppearanceChance)
            {
                return;
            }

            _bolojanVisible = true;
            _bolojanExposureElapsed = 0f;
            _bolojanCameraImage.image = appearanceTexture;
            _bolojanCameraImage.style.display = DisplayStyle.Flex;

            _bolojanCameraBaseLeft = -2.5f;
            _bolojanCameraBaseTop = -2.5f;
            _bolojanCameraImage.style.width = new Length(105f, LengthUnit.Percent);
            _bolojanCameraImage.style.height = new Length(105f, LengthUnit.Percent);

            _bolojanCameraImage.style.left = new Length(
                _bolojanCameraBaseLeft,
                LengthUnit.Percent);
            _bolojanCameraImage.style.top = new Length(
                _bolojanCameraBaseTop,
                LengthUnit.Percent);
        }

        private void UpdateBolojanThreat(float elapsedSeconds)
        {
            if (!_bolojanVisible)
            {
                return;
            }

            if (!_cameraDashboardOpen ||
                _roomsOccupiedByOtherThreats.Contains(_activeCameraId) ||
                (_activeCameraId != BolojanCameraOneId &&
                    _activeCameraId != BolojanCameraSevenId))
            {
                DismissBolojan();
                return;
            }

            _bolojanExposureElapsed += elapsedSeconds;
            if (_bolojanExposureElapsed >= _bolojanExposureSeconds)
            {
                TriggerBolojanJumpscare();
            }
        }

        private void DismissBolojan()
        {
            _bolojanVisible = false;
            _bolojanExposureElapsed = 0f;
            if (_bolojanCameraImage != null)
            {
                _bolojanCameraImage.image = null;
                _bolojanCameraImage.style.display = DisplayStyle.None;
            }
        }

        private void TriggerBolojanJumpscare()
        {
            DismissBolojan();
            _bolojanJumpscareActive = true;
            _bolojanJumpscareElapsed = 0f;
            _batteryRemaining = Mathf.Max(
                0f,
                _batteryRemaining - _bolojanBatteryTaxPercent);
            UpdateBatteryReadout();
            _cameraDashboardOpen = false;
            _cameraDashboard.style.display = DisplayStyle.None;
            _officeControlsLayer.style.display = DisplayStyle.None;

            _bolojanJumpscareImage.image = _bolojanJumpscareTexture;
            _bolojanJumpscareImage.style.display = DisplayStyle.Flex;
            _bolojanTaxLabel.style.display = DisplayStyle.None;
            _bolojanJumpscareOverlay.style.display = DisplayStyle.Flex;
            _bolojanJumpscareOverlay.BringToFront();
        }

        private void AnimateBolojanJumpscare(float elapsedSeconds)
        {
            if (!_bolojanJumpscareActive)
            {
                return;
            }

            _bolojanJumpscareElapsed += elapsedSeconds;
            bool showTaxText = _bolojanJumpscareElapsed >= BolojanTaxTextStartSeconds &&
                _bolojanJumpscareElapsed <= BolojanTaxTextEndSeconds;
            _bolojanTaxLabel.style.display = showTaxText
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            float violence = Mathf.Clamp01(
                1f - _bolojanJumpscareElapsed / _bolojanJumpscareDurationSeconds);
            float shakeX = Random.Range(-2.8f, 2.8f) * violence;
            float shakeY = Random.Range(-2.1f, 2.1f) * violence;
            _bolojanJumpscareImage.style.left = new Length(-3f + shakeX, LengthUnit.Percent);
            _bolojanJumpscareImage.style.top = new Length(-3f + shakeY, LengthUnit.Percent);
            _bolojanJumpscareImage.style.width = new Length(
                106f + Random.Range(0f, 2.5f) * violence,
                LengthUnit.Percent);
            _bolojanJumpscareImage.style.height = new Length(
                106f + Random.Range(0f, 2.5f) * violence,
                LengthUnit.Percent);
            _bolojanJumpscareImage.style.opacity = Random.Range(0.86f, 1f);

            if (_bolojanJumpscareElapsed < _bolojanJumpscareDurationSeconds)
            {
                return;
            }

            _bolojanJumpscareActive = false;
            _bolojanJumpscareImage.style.display = DisplayStyle.None;
            _bolojanTaxLabel.style.display = DisplayStyle.None;
            _bolojanJumpscareOverlay.style.display = DisplayStyle.None;
            _officeControlsLayer.style.display = DisplayStyle.Flex;
            _officeControlsLayer.BringToFront();
            _clockLabel.parent.BringToFront();
            _batteryLabel.parent.BringToFront();
        }

        public void SetOtherThreatRoomOccupied(string cameraId, bool occupied)
        {
            if (string.IsNullOrEmpty(cameraId))
            {
                return;
            }

            if (occupied)
            {
                _roomsOccupiedByOtherThreats.Add(cameraId);
                if (_bolojanVisible && cameraId == _activeCameraId)
                {
                    DismissBolojan();
                }
            }
            else
            {
                _roomsOccupiedByOtherThreats.Remove(cameraId);
            }
        }

        public void SetOfficeVisualAssets(
            PanelSettings panelSettings,
            Texture2D officeBackground,
            Texture2D leftLightBackground,
            Texture2D rightLightBackground,
            Texture2D leftDoorBackground,
            Texture2D rightDoorBackground,
            Texture2D dairyCameraFeed,
            Texture2D bakeryCameraFeed,
            Texture2D parksideCameraFeed,
            Texture2D detergentsCameraFeed,
            Texture2D iceCreamCameraFeed,
            Texture2D checkoutsCameraFeed,
            Texture2D stockroomCameraFeed,
            Texture2D employeeRoomCameraFeed,
            Texture2D bolojanCameraOneTexture,
            Texture2D bolojanCameraSevenTexture,
            Texture2D bolojanJumpscareTexture)
        {
            _fallbackPanelSettings = panelSettings;
            _officeBackground = officeBackground;
            _leftLightBackground = leftLightBackground;
            _rightLightBackground = rightLightBackground;
            _leftDoorBackground = leftDoorBackground;
            _rightDoorBackground = rightDoorBackground;
            _dairyCameraFeed = dairyCameraFeed;
            _bakeryCameraFeed = bakeryCameraFeed;
            _parksideCameraFeed = parksideCameraFeed;
            _detergentsCameraFeed = detergentsCameraFeed;
            _iceCreamCameraFeed = iceCreamCameraFeed;
            _checkoutsCameraFeed = checkoutsCameraFeed;
            _stockroomCameraFeed = stockroomCameraFeed;
            _employeeRoomCameraFeed = employeeRoomCameraFeed;
            _bolojanCameraOneTexture = bolojanCameraOneTexture;
            _bolojanCameraSevenTexture = bolojanCameraSevenTexture;
            _bolojanJumpscareTexture = bolojanJumpscareTexture;
        }

        private void UpdateClock(bool force)
        {
            int completedHours = Mathf.Min(
                Mathf.FloorToInt(_elapsedNightSeconds / SecondsPerGameHour),
                FinalHour);

            if (!force && completedHours == _displayedHour)
            {
                return;
            }

            _displayedHour = completedHours;
            int clockHour = completedHours == 0 ? 12 : completedHours;
            _clockLabel.text = $"{clockHour} AM";
        }

        private void DrainBattery(float elapsedSeconds)
        {
            if (_batteryRemaining <= 0f)
            {
                return;
            }

            float drainPerSecond = StandardUsageDrainPerSecond;
            if (_cameraDashboardOpen)
            {
                drainPerSecond += CameraUsageDrainPerSecond;
            }
            else if (_leftLightOn || _rightLightOn)
            {
                drainPerSecond += StandardUsageDrainPerSecond;
            }

            if (_leftDoorClosed)
            {
                drainPerSecond += StandardUsageDrainPerSecond;
            }

            if (_rightDoorClosed)
            {
                drainPerSecond += StandardUsageDrainPerSecond;
            }

            _batteryRemaining = Mathf.Max(
                0f,
                _batteryRemaining - drainPerSecond * elapsedSeconds);
            UpdateBatteryReadout();
        }

        private void UpdateBatteryReadout()
        {
            int batteryPercent = Mathf.Clamp(
                Mathf.CeilToInt(_batteryRemaining),
                0,
                Mathf.CeilToInt(StartingBatteryPercent));
            if (batteryPercent == _displayedBatteryPercent)
            {
                return;
            }

            _displayedBatteryPercent = batteryPercent;
            _batteryLabel.text = $"{batteryPercent}%";
            _batteryLabel.RemoveFromClassList("battery-safe");
            _batteryLabel.RemoveFromClassList("battery-warning");
            _batteryLabel.RemoveFromClassList("battery-critical");

            if (batteryPercent <= CriticalBatteryMaximum)
            {
                _batteryLabel.AddToClassList("battery-critical");
            }
            else if (batteryPercent <= WarningBatteryMaximum)
            {
                _batteryLabel.AddToClassList("battery-warning");
            }
            else
            {
                _batteryLabel.AddToClassList("battery-safe");
            }
        }

        private static void ReturnToMenu()
        {
            SceneManager.LoadScene(MainMenuSceneName);
        }

    }
}
