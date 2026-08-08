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
            _menuContent.Add(titleBlock);

            VisualElement actions = new VisualElement();
            actions.AddToClassList("menu-actions");

            _startButton = MenuUiFactory.CreateButton("START DEMO", StartDemo, "start-demo-button");
            _startButton.AddToClassList("primary-button");
            actions.Add(_startButton);

            _settingsButton = MenuUiFactory.CreateButton("SETTINGS", ShowSettings, "settings-button");
            actions.Add(_settingsButton);
            actions.Add(MenuUiFactory.CreateButton("QUIT", QuitGame, "quit-button"));
            _menuContent.Add(actions);
            _root.Add(_menuContent);

            BuildSettingsOverlay();

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

            VisualElement panel = new VisualElement();
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

            Button backButton = MenuUiFactory.CreateButton("BACK", HideSettings, "settings-back-button");
            backButton.AddToClassList("settings-back-button");
            panel.Add(backButton);

            _settingsOverlay.Add(panel);
            _root.Add(_settingsOverlay);
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

            if (_settingsOverlay.resolvedStyle.display != DisplayStyle.None)
            {
                HideSettings();
                keyEvent.StopPropagation();
            }
        }
    }
}
