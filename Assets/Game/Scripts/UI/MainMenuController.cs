using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FiveNightsAtLidl.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIDocument))]
    public sealed class MainMenuController : MonoBehaviour
    {
        private const string DemoSceneName = "DemoPlaceholder";

        [SerializeField]
        private RenderTexture _backgroundTexture;

        private VisualElement _root;
        private VisualElement _menuContent;
        private VisualElement _settingsOverlay;
        private Button _startButton;
        private Button _settingsButton;
        private Label _currentNightLabel;
        private Label _progressResetStatusLabel;
        private VisualElement _replayLockControls;
        private VisualElement _replayControls;
        private Label _replayStatusLabel;
        private TextField _replayPassword;
        private IntegerField _replayNightField;
        private VisualElement _customNightOverlay;
        private VisualElement _customNightLock;
        private VisualElement _customNightForm;
        private Label _customNightStatus;
        private Label _customNightSummary;
        private TextField _customNightPassword;
        private Button _customNightButton;
        private CustomNightSettings _customNightSettings;

        private void OnEnable()
        {
            MenuPreferences.Apply();
            BuildInterface();
        }

        private void OnDisable()
        {
            MenuPreferences.Save();
        }

        private void BuildInterface()
        {
            UIDocument document = GetComponent<UIDocument>();
            _root = MenuUiFactory.PrepareRoot(document, "main-menu-root");
            _root.AddToClassList("screen-root");
            _root.AddToClassList("video-menu");
            _root.RegisterCallback<KeyDownEvent>(OnKeyDown);

            if (_backgroundTexture != null)
            {
                Image videoBackground = new Image
                {
                    name = "menu-video-background",
                    image = _backgroundTexture,
                    scaleMode = ScaleMode.ScaleAndCrop,
                    pickingMode = PickingMode.Ignore
                };
                videoBackground.AddToClassList("video-background");
                _root.Insert(0, videoBackground);
            }
            else
            {
                Debug.LogError("The main menu has no background render texture assigned.", this);
            }

            _menuContent = new VisualElement { name = "menu-content" };
            _menuContent.AddToClassList("menu-content");

            VisualElement titleBlock = new VisualElement();
            titleBlock.AddToClassList("title-block");
            titleBlock.Add(MenuUiFactory.CreateLabel("Five Nights", "game-title"));
            titleBlock.Add(MenuUiFactory.CreateLabel("at Lidl", "game-title-accent"));
            titleBlock.Add(MenuUiFactory.CreateLabel(
                "You've been hired as a night security guard.",
                "game-subtitle"));
            _currentNightLabel = MenuUiFactory.CreateLabel(
                $"CURRENT NIGHT  {MenuPreferences.CurrentNight}",
                "current-night-label");
            titleBlock.Add(_currentNightLabel);
            _menuContent.Add(titleBlock);

            VisualElement actions = new VisualElement();
            actions.AddToClassList("menu-actions");

            _startButton = MenuUiFactory.CreateButton("START NIGHT", StartDemo, "start-demo-button");
            _startButton.AddToClassList("primary-button");
            actions.Add(_startButton);

            _settingsButton = MenuUiFactory.CreateButton("SETTINGS", ShowSettings, "settings-button");
            actions.Add(_settingsButton);
            _customNightButton = MenuUiFactory.CreateButton(
                "CUSTOM NIGHT", ShowCustomNight, "custom-night-button");
            actions.Add(_customNightButton);
            actions.Add(MenuUiFactory.CreateButton("QUIT", QuitGame, "quit-button"));
            _menuContent.Add(actions);
            _root.Add(_menuContent);

            BuildSettingsOverlay();
            BuildCustomNightOverlay();

            Label footer = MenuUiFactory.CreateLabel(
                $"User subjected to Bolojan Taxes  •  VERSION {Application.version}",
                "footer-label");
            _root.Add(footer);

            _startButton.schedule.Execute(_startButton.Focus);
        }

        public void SetBackgroundTexture(RenderTexture backgroundTexture)
        {
            _backgroundTexture = backgroundTexture;
        }

        private void BuildSettingsOverlay()
        {
            _settingsOverlay = new VisualElement { name = "settings-overlay" };
            _settingsOverlay.AddToClassList("settings-overlay");
            _settingsOverlay.style.display = DisplayStyle.None;

            ScrollView panel = new ScrollView(ScrollViewMode.Vertical)
            {
                verticalScrollerVisibility = ScrollerVisibility.Hidden,
                horizontalScrollerVisibility = ScrollerVisibility.Hidden
            };
            panel.AddToClassList("settings-panel");
            panel.Add(MenuUiFactory.CreateLabel("SETTINGS", "panel-title"));
            panel.Add(MenuUiFactory.CreateLabel(
                "These preferences are saved automatically.",
                "panel-description"));

            Slider volume = new Slider("MASTER VOLUME", 0f, 1f)
            {
                name = "master-volume-slider",
                value = MenuPreferences.MasterVolume,
                showInputField = false
            };
            volume.AddToClassList("settings-control");
            volume.RegisterValueChangedCallback(change =>
            {
                MenuPreferences.MasterVolume = change.newValue;
            });
            panel.Add(volume);

            Toggle fullscreen = new Toggle("FULLSCREEN")
            {
                name = "fullscreen-toggle",
                value = MenuPreferences.Fullscreen
            };
            fullscreen.AddToClassList("settings-control");
            fullscreen.RegisterValueChangedCallback(change =>
            {
                MenuPreferences.Fullscreen = change.newValue;
            });
            panel.Add(fullscreen);

            SliderInt aggressionReduction = new SliderInt("AGGRESSION", -5, 5)
            {
                name = "aggression-reduction-slider",
                value = MenuPreferences.Aggression,
                showInputField = true
            };
            aggressionReduction.AddToClassList("settings-control");
            aggressionReduction.RegisterValueChangedCallback(change =>
            {
                MenuPreferences.Aggression = change.newValue;
                aggressionReduction.SetValueWithoutNotify(MenuPreferences.Aggression);
            });
            panel.Add(aggressionReduction);
            Label aggressionDescription = MenuUiFactory.CreateLabel(
                "0: normal. -5: 5 AI points easier. +5: 5 AI points harder.\nApplies to Selly, Cabral and Dorian on every night.",
                "night-progress-description");
            aggressionDescription.style.whiteSpace = WhiteSpace.Normal;
            panel.Add(aggressionDescription);

            panel.Add(MenuUiFactory.CreateLabel(
                "NIGHT PROGRESS",
                "settings-section-label"));
            panel.Add(MenuUiFactory.CreateLabel(
                $"Currently on Night {MenuPreferences.CurrentNight}.",
                "night-progress-description"));

            Button resetProgressButton = MenuUiFactory.CreateButton(
                "RESET TO NIGHT 1",
                ResetNightProgress,
                "reset-night-progress-button");
            resetProgressButton.AddToClassList("reset-progress-button");
            panel.Add(resetProgressButton);

            _progressResetStatusLabel = MenuUiFactory.CreateLabel(
                string.Empty,
                "progress-reset-status");
            _progressResetStatusLabel.style.display = DisplayStyle.None;
            panel.Add(_progressResetStatusLabel);

            BuildNightReplayControls(panel);

            Button backButton = MenuUiFactory.CreateButton("BACK", HideSettings, "settings-back-button");
            backButton.AddToClassList("settings-back-button");
            panel.Add(backButton);

            _settingsOverlay.Add(panel);
            _root.Add(_settingsOverlay);
        }

        private void BuildNightReplayControls(VisualElement panel)
        {
            panel.Add(MenuUiFactory.CreateLabel("REPLAY A NIGHT", "settings-section-label"));
            _replayLockControls = new VisualElement();
            _replayLockControls.Add(MenuUiFactory.CreateLabel(
                "Complete the first 5 nights or enter the password to unlock.",
                "night-progress-description"));
            _replayPassword = new TextField("PASSWORD")
            {
                name = "night-replay-password",
                isPasswordField = true
            };
            _replayPassword.AddToClassList("settings-control");
            _replayPassword.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                {
                    UnlockNightReplay();
                    evt.StopPropagation();
                }
            });
            _replayLockControls.Add(_replayPassword);
            _replayLockControls.Add(MenuUiFactory.CreateButton(
                "UNLOCK REPLAY", UnlockNightReplay, "unlock-night-replay-button"));
            panel.Add(_replayLockControls);

            _replayControls = new VisualElement();
            _replayNightField = new IntegerField("NIGHT")
            {
                name = "night-replay-number",
                value = 1
            };
            _replayNightField.AddToClassList("settings-control");
            _replayControls.Add(_replayNightField);
            _replayControls.Add(MenuUiFactory.CreateButton(
                "PLAY SELECTED NIGHT", StartReplayNight, "start-night-replay-button"));
            panel.Add(_replayControls);
            _replayStatusLabel = MenuUiFactory.CreateLabel(string.Empty, "night-progress-description");
            panel.Add(_replayStatusLabel);
            RefreshNightReplayUi();
        }

        private void BuildCustomNightOverlay()
        {
            _customNightSettings = MenuPreferences.LoadCustomNightSettings();
            _customNightOverlay = new VisualElement { name = "custom-night-overlay" };
            _customNightOverlay.AddToClassList("settings-overlay");
            _customNightOverlay.style.display = DisplayStyle.None;
            ScrollView panel = new ScrollView(ScrollViewMode.Vertical)
            {
                verticalScrollerVisibility = ScrollerVisibility.Hidden,
                horizontalScrollerVisibility = ScrollerVisibility.Hidden
            };
            panel.AddToClassList("settings-panel");
            panel.AddToClassList("custom-night-panel");
            panel.Add(MenuUiFactory.CreateLabel("CUSTOM NIGHT", "panel-title"));

            _customNightLock = new VisualElement();
            _customNightLock.Add(CustomNightDescription(
                "Complete the first 5 nights or enter the password to unlock."));
            _customNightPassword = new TextField("PASSWORD") { isPasswordField = true };
            _customNightPassword.AddToClassList("settings-control");
            _customNightPassword.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                {
                    UnlockCustomNight();
                    evt.StopPropagation();
                }
            });
            _customNightLock.Add(_customNightPassword);
            _customNightLock.Add(MenuUiFactory.CreateButton("UNLOCK", UnlockCustomNight));
            panel.Add(_customNightLock);
            _customNightStatus = CustomNightDescription(string.Empty);
            panel.Add(_customNightStatus);

            _customNightForm = new VisualElement();
            _customNightForm.Add(CustomNightDescription(
                "Set each character's baseline AI and how many AI points it gains each hour. Both zero disables it.\n" +
                "Bolojan and the girls: each AI point means a 4% appearance chance.\n" +
                "Custom AI replaces normal night/hour bonuses and the Settings aggression modifier."));
            for (int i = 0; i < CustomNightSettings.CharacterNames.Length; i++)
            {
                int character = i;
                VisualElement row = new VisualElement();
                row.AddToClassList("custom-night-character-row");
                row.Add(MenuUiFactory.CreateLabel(CustomNightSettings.CharacterNames[i], "custom-night-character-name"));
                AddCustomInteger(row, "BASE AI", _customNightSettings.Aggression[i], 0, 25,
                    value => _customNightSettings.Aggression[character] = value, true);
                AddCustomInteger(row, "+ / HOUR", _customNightSettings.HourlyGrowth[i], 0, 25,
                    value => _customNightSettings.HourlyGrowth[character] = value, true);
                _customNightForm.Add(row);
            }
            AddCustomInteger(_customNightForm, "BATTERY CAPACITY (%)", _customNightSettings.BatteryCapacityPercent,
                10, 300, value => _customNightSettings.BatteryCapacityPercent = value);
            _customNightForm.Add(CustomNightDescription(
                "100% is standard energy; 120% lasts 20% longer. The battery HUD still begins at 100%.\n" +
                "AI = baseline + hourly increase × hours elapsed, capped at 25.\n" +
                "All threats are available. Custom wins do not advance story progress."));
            _customNightSummary = CustomNightDescription(string.Empty);
            _customNightForm.Add(_customNightSummary);
            _customNightForm.Add(MenuUiFactory.CreateButton("START CUSTOM NIGHT", StartCustomNight));
            panel.Add(_customNightForm);
            panel.Add(MenuUiFactory.CreateButton("BACK", HideCustomNight));
            _customNightOverlay.Add(panel);
            _root.Add(_customNightOverlay);
            RefreshCustomNightUi();
        }

        private static Label CustomNightDescription(string text)
        {
            Label label = MenuUiFactory.CreateLabel(text, "night-progress-description");
            label.style.whiteSpace = WhiteSpace.Normal;
            return label;
        }

        private void AddCustomInteger(VisualElement parent, string title, int initial,
            int minimum, int maximum, System.Action<int> changed, bool compact = false)
        {
            IntegerField field = new IntegerField(title) { value = initial, isDelayed = true };
            field.AddToClassList("settings-control");
            if (compact) field.AddToClassList("custom-night-number");
            field.RegisterValueChangedCallback(evt =>
            {
                int value = Mathf.Clamp(evt.newValue, minimum, maximum);
                field.SetValueWithoutNotify(value);
                changed(value);
                MenuPreferences.SaveCustomNightSettings(_customNightSettings);
                RefreshCustomNightUi();
            });
            parent.Add(field);
        }

        private void UnlockCustomNight()
        {
            bool unlocked = MenuPreferences.UnlockNightReplay(_customNightPassword.value);
            _customNightPassword.value = string.Empty;
            _customNightStatus.text = unlocked ? "Custom Night unlocked." : "Incorrect password.";
            RefreshCustomNightUi();
            RefreshNightReplayUi();
        }

        private void RefreshCustomNightUi()
        {
            bool unlocked = MenuPreferences.NightReplayUnlocked;
            _customNightLock.style.display = unlocked ? DisplayStyle.None : DisplayStyle.Flex;
            _customNightForm.style.display = unlocked ? DisplayStyle.Flex : DisplayStyle.None;
            _customNightButton.text = unlocked ? "CUSTOM NIGHT" : "CUSTOM NIGHT (LOCKED)";
            string summary = "STARTING AI — CUSTOM NIGHT\n";
            for (int i = 0; i < CustomNightSettings.CharacterNames.Length; i++)
                summary += $"{CustomNightSettings.CharacterNames[i]}: {_customNightSettings.GetAi(i)}  ";
            _customNightSummary.text = summary;
        }

        private void ShowCustomNight()
        {
            RefreshCustomNightUi();
            _menuContent.style.display = DisplayStyle.None;
            _customNightOverlay.style.display = DisplayStyle.Flex;
        }

        private void HideCustomNight()
        {
            MenuPreferences.SaveCustomNightSettings(_customNightSettings);
            _customNightOverlay.style.display = DisplayStyle.None;
            _menuContent.style.display = DisplayStyle.Flex;
            _customNightButton.Focus();
        }

        private void StartCustomNight()
        {
            if (MenuPreferences.SelectCustomNight(_customNightSettings)) StartDemo();
        }

        private void UnlockNightReplay()
        {
            bool unlocked = MenuPreferences.UnlockNightReplay(_replayPassword.value);
            _replayPassword.value = string.Empty;
            _replayStatusLabel.text = unlocked ? "Night replay unlocked." : "Incorrect password.";
            RefreshNightReplayUi();
            RefreshCustomNightUi();
        }

        private void RefreshNightReplayUi()
        {
            bool unlocked = MenuPreferences.NightReplayUnlocked;
            _replayLockControls.style.display = unlocked ? DisplayStyle.None : DisplayStyle.Flex;
            _replayControls.style.display = unlocked ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void StartReplayNight()
        {
            if (!MenuPreferences.SelectReplayNight(_replayNightField.value))
            {
                _replayStatusLabel.text = "Choose a night number of 1 or higher.";
                return;
            }

            StartDemo();
        }

        private void ShowSettings()
        {
            _menuContent.style.display = DisplayStyle.None;
            _settingsOverlay.style.display = DisplayStyle.Flex;

            Button backButton = _settingsOverlay.Q<Button>("settings-back-button");
            backButton?.schedule.Execute(backButton.Focus);
        }

        private void HideSettings()
        {
            MenuPreferences.Save();
            _settingsOverlay.style.display = DisplayStyle.None;
            _menuContent.style.display = DisplayStyle.Flex;
            _settingsButton.schedule.Execute(_settingsButton.Focus);
        }

        private void StartDemo()
        {
            SceneManager.LoadScene(DemoSceneName);
        }

        private void ResetNightProgress()
        {
            MenuPreferences.ResetNightProgress();
            RefreshNightProgressUi();
            RefreshNightReplayUi();
            _progressResetStatusLabel.text = "Progress reset. Night 1 is ready.";
            RefreshCustomNightUi();
            _progressResetStatusLabel.style.display = DisplayStyle.Flex;
        }

        private void RefreshNightProgressUi()
        {
            int currentNight = MenuPreferences.CurrentNight;
            _currentNightLabel.text = $"CURRENT NIGHT  {currentNight}";
            _startButton.text = $"START NIGHT {currentNight}";

            Label progressDescription = _settingsOverlay?.Q<Label>(
                className: "night-progress-description");
            if (progressDescription != null)
            {
                progressDescription.text = $"Currently on Night {currentNight}.";
            }
        }

        private static void QuitGame()
        {
            MenuPreferences.Save();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnKeyDown(KeyDownEvent keyEvent)
        {
            if (keyEvent.keyCode != KeyCode.Escape)
            {
                return;
            }

            if (_customNightOverlay.style.display.value != DisplayStyle.None)
            {
                HideCustomNight();
                keyEvent.StopPropagation();
                return;
            }

            if (_settingsOverlay.resolvedStyle.display != DisplayStyle.None)
            {
                HideSettings();
                keyEvent.StopPropagation();
            }
        }
    }
}
