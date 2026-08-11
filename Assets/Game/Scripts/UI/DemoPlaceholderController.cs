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
        private const float CameraFeedSwitchDelaySeconds = 0.25f;
        private const float PowerFailureRollIntervalSeconds = 1f;
        private const float PowerFailureChanceStep = 0.1f;
        private const string BolojanTaxMessage = "BOLOJAN TAX";
        private const string ElectricityWastedMessage = "ELECTRICITY WASTED";
        private const string SellyCameraFourId = "CAM 04";
        private const string SellyCameraFiveId = "CAM 05";
        private const string SellyCameraSevenId = "CAM 07";
        private const string SellyCameraEightId = "CAM 08";
        private const int SellyDoorRouteIndex = 4;
        private const float SellyMovementStaticDurationSeconds = 0.35f;
        private const string SellyLossTitle = "SHIFT FAILED";
        private const string SellyLossMessage = "Selly reached the office.";
        private const string CabralCameraOneId = "CAM 01";
        private const string CabralCameraTwoId = "CAM 02";
        private const string CabralCameraSixId = "CAM 06";
        private const int CabralDoorRouteIndex = 3;
        private const float CabralMovementStaticDurationSeconds = 0.35f;
        private const string CabralLossTitle = "SHIFT FAILED";
        private const string CabralLossMessage = "Cabral reached the office.";

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

        [Header("Selly Threat")]
        [SerializeField]
        private Texture2D _sellyCameraFourTexture;

        [SerializeField]
        private Texture2D _sellyCameraFiveTexture;

        [SerializeField]
        private Texture2D _sellyCameraSevenTexture;

        [SerializeField]
        private Texture2D _sellyCameraEightTexture;

        [SerializeField]
        private Texture2D _sellyRightDoorTexture;

        [SerializeField]
        private Texture2D _sellyJumpscareTexture;

        [SerializeField, Min(0.1f)]
        [Tooltip("Seconds between Bonnie-style movement opportunities.")]
        private float _sellyMovementOpportunitySeconds = 4.97f;

        [SerializeField, Min(0.1f)]
        [Tooltip("Continuous seconds at the open right door before Selly attacks.")]
        private float _sellyOpenDoorAttackSeconds = 5f;

        [SerializeField, Min(0f)]
        private float _sellyMinimumRetreatDelaySeconds = 2f;

        [SerializeField, Min(0f)]
        private float _sellyMaximumRetreatDelaySeconds = 5f;

        [SerializeField, Range(-1, 20)]
        [Tooltip("Editor balance override. -1 uses the current night and hour.")]
        private int _sellyAiOverride = -1;

        [Header("Cabral Threat")]
        [SerializeField]
        private Texture2D _cabralCameraOneTexture;

        [SerializeField]
        private Texture2D _cabralCameraTwoTexture;

        [SerializeField]
        private Texture2D _cabralCameraSixTexture;

        [SerializeField]
        private Texture2D _cabralLeftDoorTexture;

        [SerializeField]
        private Texture2D _cabralJumpscareTexture;

        [SerializeField, Min(0.1f)]
        [Tooltip("Seconds between Chica-style movement opportunities.")]
        private float _cabralMovementOpportunitySeconds = 4.98f;

        [SerializeField, Min(0.1f)]
        [Tooltip("Continuous seconds at the open left door before Cabral attacks.")]
        private float _cabralOpenDoorAttackSeconds = 5f;

        [SerializeField, Min(0f)]
        private float _cabralMinimumRetreatDelaySeconds = 2f;

        [SerializeField, Min(0f)]
        private float _cabralMaximumRetreatDelaySeconds = 5f;

        [SerializeField, Range(-1, 20)]
        [Tooltip("Editor balance override. -1 uses Chica's current night and hour progression.")]
        private int _cabralAiOverride = -1;

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
        private VisualElement _cameraFeedLoadingOverlay;
        private VisualElement _bolojanJumpscareOverlay;
        private VisualElement _powerFailureDimOverlay;
        private VisualElement _resultOverlay;
        private Image _leftOfficeStateImage;
        private Image _rightOfficeStateImage;
        private Image _cameraFeedImage;
        private Image _bolojanCameraImage;
        private Image _bolojanJumpscareImage;
        private Label _bolojanJumpscareMessageLabel;
        private Label _resultTitleLabel;
        private Label _resultMessageLabel;
        private Button _leftLightButton;
        private Button _rightLightButton;
        private Button _leftDoorButton;
        private Button _rightDoorButton;
        private Button _selectedCameraButton;
        private Button _cameraToggleButton;
        private Button _resultPrimaryButton;
        private float _elapsedNightSeconds;
        private float _batteryRemaining;
        private float _nextCameraFeedFlickerAt;
        private float _cameraFeedFlickerUntil;
        private float _cameraFeedRevealAt;
        private float _preNightTitleRemaining;
        private float _bolojanExposureElapsed;
        private float _bolojanJumpscareElapsed;
        private float _bolojanCameraBaseLeft;
        private float _bolojanCameraBaseTop;
        private float _powerFailureElapsed;
        private float _nextPowerFailureRollAt;
        private int _powerFailureRollCount;
        private int _displayedHour = -1;
        private int _displayedBatteryPercent = -1;
        private bool _leftLightOn;
        private bool _rightLightOn;
        private bool _leftDoorClosed;
        private bool _rightDoorClosed;
        private bool _cameraDashboardOpen;
        private bool _cameraFeedActive;
        private bool _cameraFeedLoading;
        private bool _nightStarted;
        private bool _nightEnded;
        private bool _bolojanVisible;
        private bool _bolojanJumpscareActive;
        private bool _bolojanJumpscareEndsNight;
        private bool _jumpscareShowsMessage;
        private bool _powerFailureActive;
        private bool _resultWasVictory;
        private bool _sellyThreatActive;
        private bool _sellyRetreatPending;
        private int _sellyRouteIndex;
        private float _sellyMovementOpportunityElapsed;
        private float _sellyDoorOpenElapsed;
        private float _sellyRetreatElapsed;
        private float _sellyRetreatDelay;
        private float _sellyCameraStaticUntil;
        private bool _cabralThreatActive;
        private bool _cabralRetreatPending;
        private int _cabralRouteIndex;
        private float _cabralMovementOpportunityElapsed;
        private float _cabralDoorOpenElapsed;
        private float _cabralRetreatElapsed;
        private float _cabralRetreatDelay;
        private float _cabralCameraStaticUntil;
        private string _lossTitle = ElectricityWastedMessage;
        private string _lossMessage = "The shift ended in darkness.";
        private string _activeCameraId = BolojanCameraOneId;
        private readonly HashSet<string> _roomsOccupiedByOtherThreats = new HashSet<string>();
        private readonly HashSet<string> _sellyStaticCameraIds = new HashSet<string>();
        private readonly HashSet<string> _cabralStaticCameraIds = new HashSet<string>();

        private void OnEnable()
        {
            if (!EnsurePanelSettings())
            {
                return;
            }

            _nightNumber = MenuPreferences.CurrentNight;
            _elapsedNightSeconds = 0f;
            _batteryRemaining = StartingBatteryPercent;
            _displayedBatteryPercent = -1;
            _leftLightOn = false;
            _rightLightOn = false;
            _leftDoorClosed = false;
            _rightDoorClosed = false;
            _cameraDashboardOpen = false;
            _cameraFeedActive = false;
            _cameraFeedLoading = false;
            _nightStarted = false;
            _nightEnded = false;
            _bolojanVisible = false;
            _bolojanJumpscareActive = false;
            _bolojanJumpscareEndsNight = false;
            _jumpscareShowsMessage = false;
            _powerFailureActive = false;
            _resultWasVictory = false;
            _sellyThreatActive = true;
            _sellyRetreatPending = false;
            _sellyRouteIndex = 0;
            _sellyMovementOpportunityElapsed = 0f;
            _sellyDoorOpenElapsed = 0f;
            _sellyRetreatElapsed = 0f;
            _sellyRetreatDelay = 0f;
            _sellyCameraStaticUntil = 0f;
            _cabralThreatActive = true;
            _cabralRetreatPending = false;
            _cabralRouteIndex = 0;
            _cabralMovementOpportunityElapsed = 0f;
            _cabralDoorOpenElapsed = 0f;
            _cabralRetreatElapsed = 0f;
            _cabralRetreatDelay = 0f;
            _cabralCameraStaticUntil = 0f;
            _lossTitle = ElectricityWastedMessage;
            _lossMessage = $"Night {_nightNumber} ended in darkness.";
            _bolojanExposureElapsed = 0f;
            _bolojanJumpscareElapsed = 0f;
            _powerFailureElapsed = 0f;
            _nextPowerFailureRollAt = PowerFailureRollIntervalSeconds;
            _powerFailureRollCount = 0;
            _activeCameraId = BolojanCameraOneId;
            _roomsOccupiedByOtherThreats.Clear();
            _roomsOccupiedByOtherThreats.Add(SellyCameraFourId);
            _roomsOccupiedByOtherThreats.Add(CabralCameraOneId);
            _sellyStaticCameraIds.Clear();
            _cabralStaticCameraIds.Clear();
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

            float elapsedSeconds = Time.deltaTime;
            AnimateCameraFeed();
            _elapsedNightSeconds += elapsedSeconds;
            UpdateClock(force: false);
            if (_displayedHour >= FinalHour)
            {
                CompleteNight();
                return;
            }

            if (_powerFailureActive)
            {
                UpdatePowerFailure(elapsedSeconds);
                return;
            }

            UpdateBolojanThreat(elapsedSeconds);
            if (_bolojanJumpscareActive || _nightEnded)
            {
                return;
            }

            UpdateSellyThreat(elapsedSeconds);
            if (_bolojanJumpscareActive || _nightEnded)
            {
                return;
            }

            UpdateCabralThreat(elapsedSeconds);
            if (_bolojanJumpscareActive || _nightEnded)
            {
                return;
            }

            DrainBattery(elapsedSeconds);
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
            BuildPowerFailureDimOverlay(root);
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
            BuildResultOverlay(root);
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

        private void BuildPowerFailureDimOverlay(VisualElement root)
        {
            _powerFailureDimOverlay = new VisualElement { name = "power-failure-dim" };
            _powerFailureDimOverlay.pickingMode = PickingMode.Ignore;
            _powerFailureDimOverlay.AddToClassList("power-failure-dim");
            _powerFailureDimOverlay.style.display = DisplayStyle.None;
            root.Insert(2, _powerFailureDimOverlay);
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

            _bolojanJumpscareMessageLabel = MenuUiFactory.CreateLabel(
                BolojanTaxMessage,
                "bolojan-tax-flash");
            _bolojanJumpscareMessageLabel.style.display = DisplayStyle.None;
            _bolojanJumpscareOverlay.Add(_bolojanJumpscareMessageLabel);

            root.Add(_bolojanJumpscareOverlay);
        }

        private void BuildResultOverlay(VisualElement root)
        {
            _resultOverlay = new VisualElement { name = "night-result-overlay" };
            _resultOverlay.AddToClassList("night-result-overlay");
            _resultOverlay.style.display = DisplayStyle.None;

            VisualElement panel = new VisualElement { name = "night-result-panel" };
            panel.AddToClassList("night-result-panel");
            _resultTitleLabel = MenuUiFactory.CreateLabel(string.Empty, "night-result-title");
            _resultMessageLabel = MenuUiFactory.CreateLabel(string.Empty, "night-result-message");
            panel.Add(_resultTitleLabel);
            panel.Add(_resultMessageLabel);

            VisualElement actions = new VisualElement { name = "night-result-actions" };
            actions.AddToClassList("night-result-actions");
            _resultPrimaryButton = MenuUiFactory.CreateButton(
                string.Empty,
                HandleResultPrimaryAction,
                "night-result-primary-button");
            _resultPrimaryButton.AddToClassList("primary-button");
            actions.Add(_resultPrimaryButton);
            actions.Add(MenuUiFactory.CreateButton(
                "MAIN MENU",
                ReturnToMenu,
                "night-result-menu-button"));
            panel.Add(actions);

            _resultOverlay.Add(panel);
            root.Add(_resultOverlay);
        }

        private void BuildOfficeControls(VisualElement root)
        {
            _officeControlsLayer = new VisualElement { name = "office-controls-layer" };
            _officeControlsLayer.AddToClassList("office-controls-layer");

            _officeControlsLayer.Add(CreateSideControls("left"));
            _officeControlsLayer.Add(CreateSideControls("right"));

            _cameraToggleButton = new Button(ShowCameraDashboard)
            {
                name = "raise-camera-button",
                text = "OPEN CAMERAS",
                tooltip = "Raise the camera monitor"
            };
            _cameraToggleButton.AddToClassList("camera-toggle-button");
            _officeControlsLayer.Add(_cameraToggleButton);

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
            if (_powerFailureActive || _nightEnded)
            {
                return;
            }

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
            if (_powerFailureActive || _nightEnded)
            {
                return;
            }

            if (isLeft)
            {
                _leftDoorClosed = !_leftDoorClosed;
            }
            else
            {
                _rightDoorClosed = !_rightDoorClosed;
            }

            RefreshDoorButtonStates();
            UpdateOfficeVisualState();
        }

        private void RefreshDoorButtonStates()
        {
            _leftDoorButton?.EnableInClassList("office-control-active", _leftDoorClosed);
            _rightDoorButton?.EnableInClassList("office-control-active", _rightDoorClosed);
        }

        private void UpdateOfficeVisualState()
        {
            if (_leftOfficeStateImage == null || _rightOfficeStateImage == null)
            {
                return;
            }

            Texture2D leftState = null;
            if (_leftDoorClosed)
            {
                leftState = _leftDoorBackground;
            }
            else if (_leftLightOn)
            {
                leftState = IsCabralAtOfficeDoor() && _cabralLeftDoorTexture != null
                    ? _cabralLeftDoorTexture
                    : _leftLightBackground;
            }
            Texture2D rightState = null;
            if (_rightDoorClosed)
            {
                rightState = _rightDoorBackground;
            }
            else if (_rightLightOn)
            {
                rightState = IsSellyAtOfficeDoor() && _sellyRightDoorTexture != null
                    ? _sellyRightDoorTexture
                    : _rightLightBackground;
            }

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

            _cameraFeedLoadingOverlay = new VisualElement
            {
                name = "camera-feed-loading-overlay",
                pickingMode = PickingMode.Ignore
            };
            _cameraFeedLoadingOverlay.AddToClassList("camera-feed-loading-overlay");
            _cameraFeedLoadingOverlay.Add(MenuUiFactory.CreateLabel(
                "ACQUIRING SIGNAL",
                "camera-feed-loading-label"));
            _cameraFeedLoadingOverlay.style.display = DisplayStyle.None;
            _cameraFeedViewport.Add(_cameraFeedLoadingOverlay);

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

            if (_selectedCameraButton == cameraButton && _activeCameraId == cameraId)
            {
                return;
            }

            DismissBolojan();
            _selectedCameraButton?.RemoveFromClassList("camera-node-selected");
            _selectedCameraButton = cameraButton;
            _selectedCameraButton.AddToClassList("camera-node-selected");
            _activeCameraLabel.text = $"{cameraId} / {locationName}";
            BeginCameraFeedSwitch(cameraId, locationName);
        }

        private void BeginCameraFeedSwitch(string cameraId, string locationName)
        {
            UpdateCameraFeed(cameraId, locationName);
            _cameraFeedLoading = true;
            _cameraFeedRevealAt = Time.unscaledTime + CameraFeedSwitchDelaySeconds;
            _cameraFeedImage.style.display = DisplayStyle.None;
            _feedPlaceholderLabel.style.display = DisplayStyle.None;
            _cameraFeedLoadingOverlay.style.display = DisplayStyle.Flex;
            _cameraFeedLoadingOverlay.BringToFront();
        }

        private void UpdateCameraFeedLoading()
        {
            if (!_cameraFeedLoading || Time.unscaledTime < _cameraFeedRevealAt)
            {
                return;
            }

            _cameraFeedLoading = false;
            _cameraFeedLoadingOverlay.style.display = DisplayStyle.None;
            _cameraFeedImage.style.display = _cameraFeedActive
                ? DisplayStyle.Flex
                : DisplayStyle.None;
            _feedPlaceholderLabel.style.display = _cameraFeedActive
                ? DisplayStyle.None
                : DisplayStyle.Flex;

            if (_cameraDashboardOpen)
            {
                TryShowBolojan(_activeCameraId);
            }
        }

        private void UpdateCameraFeed(string cameraId, string locationName)
        {
            _activeCameraId = cameraId;
            Texture2D feedTexture = ResolveCameraFeedTexture(cameraId);
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

        private Texture2D ResolveCameraFeedTexture(string cameraId)
        {
            Texture2D cabralTexture = ResolveCabralCameraTexture(cameraId);
            if (cabralTexture != null)
            {
                return cabralTexture;
            }

            Texture2D sellyTexture = ResolveSellyCameraTexture(cameraId);
            if (sellyTexture != null)
            {
                return sellyTexture;
            }

            if (cameraId == "CAM 01")
            {
                return _dairyCameraFeed;
            }

            if (cameraId == "CAM 02")
            {
                return _bakeryCameraFeed;
            }

            if (cameraId == "CAM 03")
            {
                return _parksideCameraFeed;
            }

            if (cameraId == SellyCameraFourId)
            {
                return _detergentsCameraFeed;
            }

            if (cameraId == SellyCameraFiveId)
            {
                return _iceCreamCameraFeed;
            }

            if (cameraId == "CAM 06")
            {
                return _checkoutsCameraFeed;
            }

            if (cameraId == SellyCameraSevenId)
            {
                return _stockroomCameraFeed;
            }

            if (cameraId == SellyCameraEightId)
            {
                return _employeeRoomCameraFeed;
            }

            return null;
        }

        private void AnimateCameraFeed()
        {
            UpdateCameraFeedLoading();
            if (!_cameraDashboardOpen || !_cameraFeedActive || _cameraFeedImage == null)
            {
                return;
            }

            float now = Time.unscaledTime;
            bool hasSellyMovementStatic = now < _sellyCameraStaticUntil &&
                _sellyStaticCameraIds.Contains(_activeCameraId);
            bool hasCabralMovementStatic = now < _cabralCameraStaticUntil &&
                _cabralStaticCameraIds.Contains(_activeCameraId);
            if (now >= _sellyCameraStaticUntil && _sellyStaticCameraIds.Count > 0)
            {
                _sellyStaticCameraIds.Clear();
            }

            if (now >= _cabralCameraStaticUntil && _cabralStaticCameraIds.Count > 0)
            {
                _cabralStaticCameraIds.Clear();
            }

            bool hasThreatMovementStatic = hasSellyMovementStatic || hasCabralMovementStatic;

            float staticJitterX = hasThreatMovementStatic ? Random.Range(-0.22f, 0.22f) : 0f;
            float staticJitterY = hasThreatMovementStatic ? Random.Range(-0.14f, 0.14f) : 0f;
            float panX = -2.5f + Mathf.Sin(now * 0.18f) * 1.15f + staticJitterX;
            float panY = -2.5f + Mathf.Sin(now * 0.13f + 1.1f) * 0.75f + staticJitterY;
            _cameraFeedImage.style.left = new Length(panX, LengthUnit.Percent);
            _cameraFeedImage.style.top = new Length(panY, LengthUnit.Percent);

            if (now >= _nextCameraFeedFlickerAt)
            {
                _cameraFeedFlickerUntil = now + Random.Range(0.025f, 0.075f);
                _nextCameraFeedFlickerAt = now + Random.Range(0.9f, 2.5f);
            }

            bool isFlickering = now < _cameraFeedFlickerUntil;
            float quietNoise = Mathf.PerlinNoise(now * 7.3f, 0.37f);
            _cameraFeedImage.style.opacity = hasThreatMovementStatic
                ? Random.Range(0.87f, 0.96f)
                : isFlickering
                ? Random.Range(0.82f, 0.91f)
                : 0.96f + (quietNoise - 0.5f) * 0.025f;
            _cameraFlickerOverlay.style.opacity = hasThreatMovementStatic
                ? Random.Range(0.055f, 0.115f)
                : isFlickering
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
            if (_nightEnded || _powerFailureActive)
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
            if (!_cameraFeedLoading)
            {
                TryShowBolojan(_activeCameraId);
            }
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

        private void UpdateSellyThreat(float elapsedSeconds)
        {
            if (!_sellyThreatActive)
            {
                return;
            }

            if (IsSellyAtOfficeDoor())
            {
                UpdateSellyDoorThreat(elapsedSeconds);
                return;
            }

            _sellyMovementOpportunityElapsed += elapsedSeconds;
            float opportunitySeconds = Mathf.Max(0.1f, _sellyMovementOpportunitySeconds);
            while (_sellyMovementOpportunityElapsed >= opportunitySeconds)
            {
                _sellyMovementOpportunityElapsed -= opportunitySeconds;
                TryAdvanceSelly();
                if (IsSellyAtOfficeDoor())
                {
                    return;
                }
            }
        }

        private void TryAdvanceSelly()
        {
            if (IsSellyObserved())
            {
                LogSellyState("Movement opportunity blocked because his current feed is observed.");
                return;
            }

            int aiLevel = GetSellyAiLevel();
            int roll = Random.Range(1, 21);
            if (roll > aiLevel)
            {
                LogSellyState($"Movement roll {roll} failed against AI {aiLevel}.");
                return;
            }

            int nextRouteIndex = Mathf.Min(_sellyRouteIndex + 1, SellyDoorRouteIndex);
            SetSellyRouteIndex(nextRouteIndex);
            LogSellyState($"Movement roll {roll} succeeded against AI {aiLevel}.");
        }

        private void UpdateSellyDoorThreat(float elapsedSeconds)
        {
            if (_sellyRetreatPending)
            {
                _sellyRetreatElapsed += elapsedSeconds;
                if (_sellyRetreatElapsed >= _sellyRetreatDelay)
                {
                    RetreatSellyFromDoor();
                }

                return;
            }

            if (_rightDoorClosed)
            {
                BeginSellyRetreat();
                return;
            }

            _sellyDoorOpenElapsed += elapsedSeconds;
            if (_sellyDoorOpenElapsed >= _sellyOpenDoorAttackSeconds)
            {
                TriggerSellyJumpscare();
            }
        }

        private void BeginSellyRetreat()
        {
            float minimumDelay = Mathf.Min(
                _sellyMinimumRetreatDelaySeconds,
                _sellyMaximumRetreatDelaySeconds);
            float maximumDelay = Mathf.Max(
                _sellyMinimumRetreatDelaySeconds,
                _sellyMaximumRetreatDelaySeconds);

            _sellyRetreatPending = true;
            _sellyRetreatElapsed = 0f;
            _sellyRetreatDelay = Random.Range(minimumDelay, maximumDelay);
            LogSellyState($"Right door blocked him; retreat scheduled in {_sellyRetreatDelay:0.00}s.");
        }

        private void RetreatSellyFromDoor()
        {
            int roomsToRetreat = Random.Range(2, 4);
            int destinationIndex = Mathf.Clamp(
                SellyDoorRouteIndex - roomsToRetreat,
                0,
                SellyDoorRouteIndex - 1);
            _sellyRetreatPending = false;
            _sellyRetreatElapsed = 0f;
            _sellyRetreatDelay = 0f;
            SetSellyRouteIndex(destinationIndex);
            LogSellyState($"Retreated {roomsToRetreat} rooms after the right door closed.");
        }

        private void SetSellyRouteIndex(int routeIndex)
        {
            string previousCameraId = GetSellyCameraId(_sellyRouteIndex);
            if (!string.IsNullOrEmpty(previousCameraId))
            {
                _roomsOccupiedByOtherThreats.Remove(previousCameraId);
            }

            _sellyRouteIndex = Mathf.Clamp(routeIndex, 0, SellyDoorRouteIndex);
            _sellyDoorOpenElapsed = 0f;
            _sellyRetreatPending = false;
            _sellyRetreatElapsed = 0f;
            _sellyRetreatDelay = 0f;

            string currentCameraId = GetSellyCameraId(_sellyRouteIndex);
            TriggerSellyMovementStatic(previousCameraId, currentCameraId);
            if (!string.IsNullOrEmpty(currentCameraId))
            {
                _roomsOccupiedByOtherThreats.Add(currentCameraId);
                if (_bolojanVisible && currentCameraId == _activeCameraId)
                {
                    DismissBolojan();
                }
            }

            RefreshActiveCameraThreatTexture();
            UpdateOfficeVisualState();
            LogSellyState($"Now at {GetSellyLocationLabel(_sellyRouteIndex)}.");
        }

        private void DeactivateSellyThreat()
        {
            if (!_sellyThreatActive)
            {
                return;
            }

            string cameraId = GetSellyCameraId(_sellyRouteIndex);
            if (!string.IsNullOrEmpty(cameraId))
            {
                _roomsOccupiedByOtherThreats.Remove(cameraId);
            }

            _sellyThreatActive = false;
            _sellyRetreatPending = false;
            _sellyStaticCameraIds.Clear();
            _sellyCameraStaticUntil = 0f;
            RefreshActiveCameraThreatTexture();
            UpdateOfficeVisualState();
        }

        private void TriggerSellyMovementStatic(
            string previousCameraId,
            string currentCameraId)
        {
            _sellyStaticCameraIds.Clear();
            if (!string.IsNullOrEmpty(previousCameraId))
            {
                _sellyStaticCameraIds.Add(previousCameraId);
            }

            if (!string.IsNullOrEmpty(currentCameraId))
            {
                _sellyStaticCameraIds.Add(currentCameraId);
            }

            _sellyCameraStaticUntil = _sellyStaticCameraIds.Count > 0
                ? Time.unscaledTime + SellyMovementStaticDurationSeconds
                : 0f;
        }

        private bool IsSellyObserved()
        {
            string currentCameraId = GetSellyCameraId(_sellyRouteIndex);
            return !string.IsNullOrEmpty(currentCameraId) &&
                _cameraDashboardOpen &&
                !_cameraFeedLoading &&
                _activeCameraId == currentCameraId;
        }

        private bool IsSellyAtOfficeDoor()
        {
            return _sellyThreatActive && _sellyRouteIndex == SellyDoorRouteIndex;
        }

        private Texture2D ResolveSellyCameraTexture(string cameraId)
        {
            if (!_sellyThreatActive || GetSellyCameraId(_sellyRouteIndex) != cameraId)
            {
                return null;
            }

            if (_sellyRouteIndex == 0)
            {
                return _sellyCameraFourTexture;
            }

            if (_sellyRouteIndex == 1)
            {
                return _sellyCameraFiveTexture;
            }

            if (_sellyRouteIndex == 2)
            {
                return _sellyCameraSevenTexture;
            }

            if (_sellyRouteIndex == 3)
            {
                return _sellyCameraEightTexture;
            }

            return null;
        }

        private void RefreshActiveCameraThreatTexture()
        {
            if (_cameraFeedImage == null)
            {
                return;
            }

            Texture2D feedTexture = ResolveCameraFeedTexture(_activeCameraId);
            _cameraFeedActive = feedTexture != null;
            _cameraFeedImage.image = feedTexture;
            if (!_cameraFeedLoading)
            {
                _cameraFeedImage.style.display = _cameraFeedActive
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
                _feedPlaceholderLabel.style.display = _cameraFeedActive
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
            }
        }

        private int GetSellyAiLevel()
        {
            if (_sellyAiOverride >= 0)
            {
                return _sellyAiOverride;
            }

            int baseAi;
            switch (_nightNumber)
            {
                case 1:
                case 3:
                    baseAi = 0;
                    break;
                case 2:
                    baseAi = 3;
                    break;
                case 4:
                    baseAi = 2;
                    break;
                case 5:
                    baseAi = 5;
                    break;
                case 6:
                    baseAi = 10;
                    break;
                default:
                    baseAi = Mathf.Clamp(10 + (_nightNumber - 6) * 2, 0, 20);
                    break;
            }

            int hourBonus = 0;
            if (_displayedHour >= 2)
            {
                hourBonus++;
            }

            if (_displayedHour >= 3)
            {
                hourBonus++;
            }

            if (_displayedHour >= 4)
            {
                hourBonus++;
            }

            return Mathf.Clamp(baseAi + hourBonus, 0, 20);
        }

        private static string GetSellyCameraId(int routeIndex)
        {
            switch (routeIndex)
            {
                case 0:
                    return SellyCameraFourId;
                case 1:
                    return SellyCameraFiveId;
                case 2:
                    return SellyCameraSevenId;
                case 3:
                    return SellyCameraEightId;
                default:
                    return null;
            }
        }

        private static string GetSellyLocationLabel(int routeIndex)
        {
            string cameraId = GetSellyCameraId(routeIndex);
            return string.IsNullOrEmpty(cameraId) ? "RIGHT OFFICE DOOR" : cameraId;
        }

        private void LogSellyState(string message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"[Selly] {message}", this);
#endif
        }

        private void UpdateCabralThreat(float elapsedSeconds)
        {
            if (!_cabralThreatActive)
            {
                return;
            }

            if (IsCabralAtOfficeDoor())
            {
                UpdateCabralDoorThreat(elapsedSeconds);
                return;
            }

            _cabralMovementOpportunityElapsed += elapsedSeconds;
            float opportunitySeconds = Mathf.Max(0.1f, _cabralMovementOpportunitySeconds);
            while (_cabralMovementOpportunityElapsed >= opportunitySeconds)
            {
                _cabralMovementOpportunityElapsed -= opportunitySeconds;
                TryAdvanceCabral();
                if (IsCabralAtOfficeDoor())
                {
                    return;
                }
            }
        }

        private void TryAdvanceCabral()
        {
            int aiLevel = GetCabralAiLevel();
            int roll = Random.Range(1, 21);
            if (roll > aiLevel)
            {
                LogCabralState($"Movement roll {roll} failed against AI {aiLevel}.");
                return;
            }

            int nextRouteIndex = Mathf.Min(_cabralRouteIndex + 1, CabralDoorRouteIndex);
            SetCabralRouteIndex(nextRouteIndex);
            LogCabralState($"Movement roll {roll} succeeded against AI {aiLevel}; camera observation does not stall him.");
        }

        private void UpdateCabralDoorThreat(float elapsedSeconds)
        {
            if (_cabralRetreatPending)
            {
                _cabralRetreatElapsed += elapsedSeconds;
                if (_cabralRetreatElapsed >= _cabralRetreatDelay)
                {
                    RetreatCabralFromDoor();
                }

                return;
            }

            if (_leftDoorClosed)
            {
                BeginCabralRetreat();
                return;
            }

            _cabralDoorOpenElapsed += elapsedSeconds;
            if (_cabralDoorOpenElapsed >= _cabralOpenDoorAttackSeconds)
            {
                TriggerCabralJumpscare();
            }
        }

        private void BeginCabralRetreat()
        {
            float minimumDelay = Mathf.Min(
                _cabralMinimumRetreatDelaySeconds,
                _cabralMaximumRetreatDelaySeconds);
            float maximumDelay = Mathf.Max(
                _cabralMinimumRetreatDelaySeconds,
                _cabralMaximumRetreatDelaySeconds);

            _cabralRetreatPending = true;
            _cabralRetreatElapsed = 0f;
            _cabralRetreatDelay = Random.Range(minimumDelay, maximumDelay);
            LogCabralState($"Left door blocked him; retreat scheduled in {_cabralRetreatDelay:0.00}s.");
        }

        private void RetreatCabralFromDoor()
        {
            int roomsToRetreat = Random.Range(2, 4);
            int destinationIndex = Mathf.Clamp(
                CabralDoorRouteIndex - roomsToRetreat,
                0,
                CabralDoorRouteIndex - 1);
            _cabralRetreatPending = false;
            _cabralRetreatElapsed = 0f;
            _cabralRetreatDelay = 0f;
            SetCabralRouteIndex(destinationIndex);
            LogCabralState($"Retreated {roomsToRetreat} rooms after the left door closed.");
        }

        private void SetCabralRouteIndex(int routeIndex)
        {
            string previousCameraId = GetCabralCameraId(_cabralRouteIndex);
            if (!string.IsNullOrEmpty(previousCameraId))
            {
                _roomsOccupiedByOtherThreats.Remove(previousCameraId);
            }

            _cabralRouteIndex = Mathf.Clamp(routeIndex, 0, CabralDoorRouteIndex);
            _cabralDoorOpenElapsed = 0f;
            _cabralRetreatPending = false;
            _cabralRetreatElapsed = 0f;
            _cabralRetreatDelay = 0f;

            string currentCameraId = GetCabralCameraId(_cabralRouteIndex);
            TriggerCabralMovementStatic(previousCameraId, currentCameraId);
            if (!string.IsNullOrEmpty(currentCameraId))
            {
                _roomsOccupiedByOtherThreats.Add(currentCameraId);
                if (_bolojanVisible && currentCameraId == _activeCameraId)
                {
                    DismissBolojan();
                }
            }

            RefreshActiveCameraThreatTexture();
            UpdateOfficeVisualState();
            LogCabralState($"Now at {GetCabralLocationLabel(_cabralRouteIndex)}.");
        }

        private void DeactivateCabralThreat()
        {
            if (!_cabralThreatActive)
            {
                return;
            }

            string cameraId = GetCabralCameraId(_cabralRouteIndex);
            if (!string.IsNullOrEmpty(cameraId))
            {
                _roomsOccupiedByOtherThreats.Remove(cameraId);
            }

            _cabralThreatActive = false;
            _cabralRetreatPending = false;
            _cabralStaticCameraIds.Clear();
            _cabralCameraStaticUntil = 0f;
            RefreshActiveCameraThreatTexture();
            UpdateOfficeVisualState();
        }

        private void TriggerCabralMovementStatic(
            string previousCameraId,
            string currentCameraId)
        {
            _cabralStaticCameraIds.Clear();
            if (!string.IsNullOrEmpty(previousCameraId))
            {
                _cabralStaticCameraIds.Add(previousCameraId);
            }

            if (!string.IsNullOrEmpty(currentCameraId))
            {
                _cabralStaticCameraIds.Add(currentCameraId);
            }

            _cabralCameraStaticUntil = _cabralStaticCameraIds.Count > 0
                ? Time.unscaledTime + CabralMovementStaticDurationSeconds
                : 0f;
        }

        private bool IsCabralAtOfficeDoor()
        {
            return _cabralThreatActive && _cabralRouteIndex == CabralDoorRouteIndex;
        }

        private Texture2D ResolveCabralCameraTexture(string cameraId)
        {
            if (!_cabralThreatActive || GetCabralCameraId(_cabralRouteIndex) != cameraId)
            {
                return null;
            }

            if (_cabralRouteIndex == 0)
            {
                return _cabralCameraOneTexture;
            }

            if (_cabralRouteIndex == 1)
            {
                return _cabralCameraTwoTexture;
            }

            if (_cabralRouteIndex == 2)
            {
                return _cabralCameraSixTexture;
            }

            return null;
        }

        private int GetCabralAiLevel()
        {
            if (_cabralAiOverride >= 0)
            {
                return _cabralAiOverride;
            }

            int baseAi;
            switch (_nightNumber)
            {
                case 1:
                    baseAi = 0;
                    break;
                case 2:
                    baseAi = 1;
                    break;
                case 3:
                    baseAi = 5;
                    break;
                case 4:
                    baseAi = 4;
                    break;
                case 5:
                    baseAi = 7;
                    break;
                case 6:
                    baseAi = 12;
                    break;
                default:
                    baseAi = Mathf.Clamp(12 + (_nightNumber - 6) * 2, 0, 20);
                    break;
            }

            int hourBonus = 0;
            if (_displayedHour >= 3)
            {
                hourBonus++;
            }

            if (_displayedHour >= 4)
            {
                hourBonus++;
            }

            return Mathf.Clamp(baseAi + hourBonus, 0, 20);
        }

        private static string GetCabralCameraId(int routeIndex)
        {
            switch (routeIndex)
            {
                case 0:
                    return CabralCameraOneId;
                case 1:
                    return CabralCameraTwoId;
                case 2:
                    return CabralCameraSixId;
                default:
                    return null;
            }
        }

        private static string GetCabralLocationLabel(int routeIndex)
        {
            string cameraId = GetCabralCameraId(routeIndex);
            return string.IsNullOrEmpty(cameraId) ? "LEFT OFFICE DOOR" : cameraId;
        }

        private void LogCabralState(string message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"[Cabral] {message}", this);
#endif
        }

        private void TryShowBolojan(string cameraId)
        {
            if (!_cameraDashboardOpen || _cameraFeedLoading || _nightEnded || _bolojanVisible)
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
                TriggerBolojanTaxJumpscare();
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

        private void TriggerBolojanTaxJumpscare()
        {
            TriggerJumpscare(
                _bolojanJumpscareTexture,
                BolojanTaxMessage,
                showsMessage: true,
                endsNight: false,
                appliesBatteryTax: true,
                lossTitle: null,
                lossMessage: null);
        }

        private void TriggerPowerFailureJumpscare()
        {
            TriggerJumpscare(
                _bolojanJumpscareTexture,
                ElectricityWastedMessage,
                showsMessage: true,
                endsNight: true,
                appliesBatteryTax: false,
                lossTitle: ElectricityWastedMessage,
                lossMessage: $"Night {_nightNumber} ended in darkness.");
        }

        private void TriggerSellyJumpscare()
        {
            DeactivateSellyThreat();
            DeactivateCabralThreat();
            TriggerJumpscare(
                _sellyJumpscareTexture,
                string.Empty,
                showsMessage: false,
                endsNight: true,
                appliesBatteryTax: false,
                lossTitle: SellyLossTitle,
                lossMessage: SellyLossMessage);
        }

        private void TriggerCabralJumpscare()
        {
            DeactivateCabralThreat();
            DeactivateSellyThreat();
            TriggerJumpscare(
                _cabralJumpscareTexture,
                string.Empty,
                showsMessage: false,
                endsNight: true,
                appliesBatteryTax: false,
                lossTitle: CabralLossTitle,
                lossMessage: CabralLossMessage);
        }

        private void TriggerJumpscare(
            Texture2D jumpscareTexture,
            string message,
            bool showsMessage,
            bool endsNight,
            bool appliesBatteryTax,
            string lossTitle,
            string lossMessage)
        {
            DismissBolojan();
            _bolojanJumpscareActive = true;
            _bolojanJumpscareEndsNight = endsNight;
            _jumpscareShowsMessage = showsMessage;
            _bolojanJumpscareElapsed = 0f;
            if (endsNight)
            {
                _lossTitle = string.IsNullOrEmpty(lossTitle)
                    ? "SHIFT FAILED"
                    : lossTitle;
                _lossMessage = string.IsNullOrEmpty(lossMessage)
                    ? $"Night {_nightNumber} ended."
                    : lossMessage;
            }

            if (appliesBatteryTax)
            {
                _batteryRemaining = Mathf.Max(
                    0f,
                    _batteryRemaining - _bolojanBatteryTaxPercent);
                UpdateBatteryReadout();
            }

            _cameraDashboardOpen = false;
            _cameraDashboard.style.display = DisplayStyle.None;
            _officeControlsLayer.style.display = DisplayStyle.None;

            _bolojanJumpscareImage.image = jumpscareTexture;
            _bolojanJumpscareImage.style.display = DisplayStyle.Flex;
            _bolojanJumpscareMessageLabel.text = message;
            _bolojanJumpscareMessageLabel.style.display = DisplayStyle.None;
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
            bool showMessage = _jumpscareShowsMessage &&
                _bolojanJumpscareElapsed >= BolojanTaxTextStartSeconds &&
                _bolojanJumpscareElapsed <= BolojanTaxTextEndSeconds;
            _bolojanJumpscareMessageLabel.style.display = showMessage
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
            _jumpscareShowsMessage = false;
            _bolojanJumpscareImage.style.display = DisplayStyle.None;
            _bolojanJumpscareMessageLabel.style.display = DisplayStyle.None;
            _bolojanJumpscareOverlay.style.display = DisplayStyle.None;

            if (_bolojanJumpscareEndsNight)
            {
                _bolojanJumpscareEndsNight = false;
                _powerFailureActive = false;
                _nightEnded = true;
                ShowResultOverlay(victory: false);
                return;
            }

            if (_batteryRemaining <= 0f)
            {
                EnterPowerFailure();
                return;
            }

            _officeControlsLayer.style.display = DisplayStyle.Flex;
            _officeControlsLayer.BringToFront();
            _clockLabel.parent.BringToFront();
            _batteryLabel.parent.BringToFront();
        }

        private void EnterPowerFailure()
        {
            if (_powerFailureActive || _nightEnded)
            {
                return;
            }

            _powerFailureActive = true;
            _powerFailureElapsed = 0f;
            _nextPowerFailureRollAt = PowerFailureRollIntervalSeconds;
            _powerFailureRollCount = 0;
            DismissBolojan();
            DeactivateSellyThreat();
            DeactivateCabralThreat();

            _cameraDashboardOpen = false;
            _cameraDashboard.style.display = DisplayStyle.None;
            _leftLightOn = false;
            _rightLightOn = false;
            _leftDoorClosed = false;
            _rightDoorClosed = false;
            RefreshLightButtonStates();
            RefreshDoorButtonStates();
            UpdateOfficeVisualState();

            SetOfficeControlsEnabled(false);
            _officeControlsLayer.style.display = DisplayStyle.Flex;
            _powerFailureDimOverlay.style.opacity = 0.82f;
            _powerFailureDimOverlay.style.display = DisplayStyle.Flex;
        }

        private void UpdatePowerFailure(float elapsedSeconds)
        {
            _powerFailureElapsed += elapsedSeconds;
            float flicker = Mathf.PerlinNoise(Time.unscaledTime * 3.2f, 0.71f);
            _powerFailureDimOverlay.style.opacity = 0.78f + flicker * 0.1f;

            while (_powerFailureElapsed >= _nextPowerFailureRollAt)
            {
                _powerFailureRollCount++;
                float attackChance = Mathf.Clamp01(
                    _powerFailureRollCount * PowerFailureChanceStep);
                _nextPowerFailureRollAt += PowerFailureRollIntervalSeconds;
                if (Random.value < attackChance)
                {
                    TriggerPowerFailureJumpscare();
                    return;
                }
            }
        }

        private void SetOfficeControlsEnabled(bool enabled)
        {
            _leftDoorButton?.SetEnabled(enabled);
            _rightDoorButton?.SetEnabled(enabled);
            _leftLightButton?.SetEnabled(enabled);
            _rightLightButton?.SetEnabled(enabled);
            _cameraToggleButton?.SetEnabled(enabled);
        }

        private void CompleteNight()
        {
            if (_nightEnded)
            {
                return;
            }

            _nightEnded = true;
            _powerFailureActive = false;
            DismissBolojan();
            DeactivateSellyThreat();
            DeactivateCabralThreat();
            _cameraDashboardOpen = false;
            _cameraDashboard.style.display = DisplayStyle.None;
            SetOfficeControlsEnabled(false);
            MenuPreferences.CompleteNight(_nightNumber);
            ShowResultOverlay(victory: true);
        }

        private void ShowResultOverlay(bool victory)
        {
            _resultWasVictory = victory;
            _resultTitleLabel.EnableInClassList("night-result-loss-title", !victory);
            if (victory)
            {
                int nextNight = MenuPreferences.CurrentNight;
                _resultTitleLabel.text = "SHIFT ENDED";
                _resultMessageLabel.text =
                    $"Night {_nightNumber} complete.\nSee you around for Night {nextNight}.";
                _resultPrimaryButton.text = $"START NIGHT {nextNight}";
            }
            else
            {
                _resultTitleLabel.text = _lossTitle;
                _resultMessageLabel.text = _lossMessage;
                _resultPrimaryButton.text = $"RETRY NIGHT {_nightNumber}";
            }

            _resultOverlay.style.display = DisplayStyle.Flex;
            _resultOverlay.BringToFront();
            _resultPrimaryButton.schedule.Execute(_resultPrimaryButton.Focus);
        }

        private void HandleResultPrimaryAction()
        {
            if (_resultWasVictory)
            {
                MenuPreferences.Save();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            Texture2D bolojanJumpscareTexture,
            Texture2D sellyCameraFourTexture,
            Texture2D sellyCameraFiveTexture,
            Texture2D sellyCameraSevenTexture,
            Texture2D sellyCameraEightTexture,
            Texture2D sellyRightDoorTexture,
            Texture2D sellyJumpscareTexture,
            Texture2D cabralCameraOneTexture,
            Texture2D cabralCameraTwoTexture,
            Texture2D cabralCameraSixTexture,
            Texture2D cabralLeftDoorTexture,
            Texture2D cabralJumpscareTexture)
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
            _sellyCameraFourTexture = sellyCameraFourTexture;
            _sellyCameraFiveTexture = sellyCameraFiveTexture;
            _sellyCameraSevenTexture = sellyCameraSevenTexture;
            _sellyCameraEightTexture = sellyCameraEightTexture;
            _sellyRightDoorTexture = sellyRightDoorTexture;
            _sellyJumpscareTexture = sellyJumpscareTexture;
            _cabralCameraOneTexture = cabralCameraOneTexture;
            _cabralCameraTwoTexture = cabralCameraTwoTexture;
            _cabralCameraSixTexture = cabralCameraSixTexture;
            _cabralLeftDoorTexture = cabralLeftDoorTexture;
            _cabralJumpscareTexture = cabralJumpscareTexture;
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
                EnterPowerFailure();
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
            if (_batteryRemaining <= 0f)
            {
                EnterPowerFailure();
            }
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
