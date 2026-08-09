using System;
using System.IO;
using FiveNightsAtLidl.UI;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace FiveNightsAtLidl.Editor
{
    public static class ProjectBootstrap
    {
        private const string MainMenuScenePath = "Assets/Game/Scenes/MainMenu/MainMenu.unity";
        private const string DemoScenePath = "Assets/Game/Scenes/Gameplay/DemoPlaceholder.unity";
        private const string PanelSettingsPath = "Assets/Game/Settings/RuntimePanelSettings.asset";
        private const string RuntimeThemePath = "Assets/Game/UI/RuntimeTheme.tss";
        private const string MenuVideoPath = "Assets/Game/Art/UI/menu_screen_loop_runtime.m4v";
        private const string OfficeBackgroundPath =
            "Assets/Game/Art/Backgrounds/OfficeBaseGenerated_v3.png";
        private const string OfficeLeftLightPath =
            "Assets/Game/Art/Backgrounds/OfficeLightLeftGenerated_v3.png";
        private const string OfficeRightLightPath =
            "Assets/Game/Art/Backgrounds/OfficeLightRightGenerated_v3.png";
        private const string OfficeLeftDoorPath =
            "Assets/Game/Art/Backgrounds/OfficeDoorLeftGenerated_v3.png";
        private const string OfficeRightDoorPath =
            "Assets/Game/Art/Backgrounds/OfficeDoorRightGenerated_v3.png";
        private const string DairyCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam01_Dairy.png";
        private const string BakeryCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam02_Bakery.png";
        private const string ParksideCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam03_Parkside.png";
        private const string DetergentsCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam04_Detergents.png";
        private const string IceCreamCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam05_IceCream.png";
        private const string CheckoutsCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam06_Checkouts.png";
        private const string StockroomCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam07_Stockroom.png";
        private const string EmployeeRoomCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam08_EmployeeRoom.png";
        private const string BolojanCameraOnePath =
            "Assets/Game/Art/Characters/Bolojan/Bolojan_Cam01_Integrated.png";
        private const string BolojanCameraSevenPath =
            "Assets/Game/Art/Characters/Bolojan/Bolojan_Cam07_Integrated.png";
        private const string BolojanJumpscarePath =
            "Assets/Game/Art/Characters/Bolojan/Bolojan_Jumpscare.png";
        private const string MenuRenderTexturePath = "Assets/Game/Settings/MenuBackground.renderTexture";
        private const string MacBuildPath = "Builds/macOS/FiveNightsAtLidl.app";

        [MenuItem("Five Nights at Lidl/Configure Project")]
        public static void ConfigureProject()
        {
            EnsureAssetFolders();
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

            PanelSettings panelSettings = CreateOrUpdatePanelSettings();
            VideoClip menuVideo = AssetDatabase.LoadAssetAtPath<VideoClip>(MenuVideoPath);
            if (menuVideo == null)
            {
                throw new InvalidOperationException($"Menu video was not imported at {MenuVideoPath}.");
            }

            Texture2D officeBackground = LoadRequiredTexture(OfficeBackgroundPath);
            Texture2D leftLightBackground = LoadRequiredTexture(OfficeLeftLightPath);
            Texture2D rightLightBackground = LoadRequiredTexture(OfficeRightLightPath);
            Texture2D leftDoorBackground = LoadRequiredTexture(OfficeLeftDoorPath);
            Texture2D rightDoorBackground = LoadRequiredTexture(OfficeRightDoorPath);
            Texture2D dairyCameraFeed = LoadRequiredTexture(DairyCameraFeedPath);
            Texture2D bakeryCameraFeed = LoadRequiredTexture(BakeryCameraFeedPath);
            Texture2D parksideCameraFeed = LoadRequiredTexture(ParksideCameraFeedPath);
            Texture2D detergentsCameraFeed = LoadRequiredTexture(DetergentsCameraFeedPath);
            Texture2D iceCreamCameraFeed = LoadRequiredTexture(IceCreamCameraFeedPath);
            Texture2D checkoutsCameraFeed = LoadRequiredTexture(CheckoutsCameraFeedPath);
            Texture2D stockroomCameraFeed = LoadRequiredTexture(StockroomCameraFeedPath);
            Texture2D employeeRoomCameraFeed = LoadRequiredTexture(EmployeeRoomCameraFeedPath);
            Texture2D bolojanCameraOne = LoadRequiredTexture(BolojanCameraOnePath);
            Texture2D bolojanCameraSeven = LoadRequiredTexture(BolojanCameraSevenPath);
            Texture2D bolojanJumpscare = LoadRequiredTexture(BolojanJumpscarePath);

            RenderTexture menuRenderTexture = CreateOrUpdateMenuRenderTexture();
            CreateMainMenuScene(panelSettings, menuVideo, menuRenderTexture);
            CreateGameplayScene(
                panelSettings,
                officeBackground,
                leftLightBackground,
                rightLightBackground,
                leftDoorBackground,
                rightDoorBackground,
                dairyCameraFeed,
                bakeryCameraFeed,
                parksideCameraFeed,
                detergentsCameraFeed,
                iceCreamCameraFeed,
                checkoutsCameraFeed,
                stockroomCameraFeed,
                employeeRoomCameraFeed,
                bolojanCameraOne,
                bolojanCameraSeven,
                bolojanJumpscare);
            ConfigureBuildScenes();
            ConfigurePlayerSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(MainMenuScenePath, OpenSceneMode.Single);
            Debug.Log("Five Nights at Lidl project configuration completed successfully.");
        }

        [MenuItem("Five Nights at Lidl/Build macOS Release")]
        public static void BuildMacRelease()
        {
            ConfigureProject();
            Directory.CreateDirectory(Path.GetDirectoryName(MacBuildPath) ?? "Builds/macOS");

            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = new[] { MainMenuScenePath, DemoScenePath },
                locationPathName = MacBuildPath,
                target = BuildTarget.StandaloneOSX,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"macOS build failed with result {report.summary.result} and " +
                    $"{report.summary.totalErrors} error(s).");
            }

            Debug.Log(
                $"macOS release built at {Path.GetFullPath(MacBuildPath)} " +
                $"({report.summary.totalSize} bytes)." );
        }

        private static PanelSettings CreateOrUpdatePanelSettings()
        {
            PanelSettings panelSettings = AssetDatabase.LoadAssetAtPath<PanelSettings>(PanelSettingsPath);
            if (panelSettings == null)
            {
                panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
                panelSettings.name = "RuntimePanelSettings";
                AssetDatabase.CreateAsset(panelSettings, PanelSettingsPath);
            }

            ThemeStyleSheet theme = AssetDatabase.LoadAssetAtPath<ThemeStyleSheet>(RuntimeThemePath);
            if (theme == null)
            {
                throw new InvalidOperationException($"Runtime theme was not imported at {RuntimeThemePath}.");
            }

            panelSettings.themeStyleSheet = theme;
            panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
            panelSettings.referenceResolution = new Vector2Int(1920, 1080);
            panelSettings.screenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;
            panelSettings.match = 0.5f;
            panelSettings.sortingOrder = 0;
            EditorUtility.SetDirty(panelSettings);
            return panelSettings;
        }

        private static RenderTexture CreateOrUpdateMenuRenderTexture()
        {
            RenderTexture renderTexture =
                AssetDatabase.LoadAssetAtPath<RenderTexture>(MenuRenderTexturePath);

            if (renderTexture == null)
            {
                renderTexture = new RenderTexture(1280, 720, 0, RenderTextureFormat.ARGB32)
                {
                    name = "MenuBackground",
                    antiAliasing = 1,
                    useMipMap = false,
                    autoGenerateMips = false,
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Bilinear,
                    anisoLevel = 0
                };
                AssetDatabase.CreateAsset(renderTexture, MenuRenderTexturePath);
            }

            EditorUtility.SetDirty(renderTexture);
            return renderTexture;
        }

        private static void CreateGameplayScene(
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
            Texture2D bolojanCameraOne,
            Texture2D bolojanCameraSeven,
            Texture2D bolojanJumpscare)
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            CreatePresentationCamera();
            GameObject root = new GameObject("Main Gameplay");

            UIDocument document = root.AddComponent<UIDocument>();
            document.panelSettings = panelSettings;
            DemoPlaceholderController controller = root.AddComponent<DemoPlaceholderController>();
            controller.SetOfficeVisualAssets(
                panelSettings,
                officeBackground,
                leftLightBackground,
                rightLightBackground,
                leftDoorBackground,
                rightDoorBackground,
                dairyCameraFeed,
                bakeryCameraFeed,
                parksideCameraFeed,
                detergentsCameraFeed,
                iceCreamCameraFeed,
                checkoutsCameraFeed,
                stockroomCameraFeed,
                employeeRoomCameraFeed,
                bolojanCameraOne,
                bolojanCameraSeven,
                bolojanJumpscare);

            EditorSceneManager.SaveScene(scene, DemoScenePath);
        }

        private static Texture2D LoadRequiredTexture(string path)
        {
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (texture == null)
            {
                throw new InvalidOperationException($"Required texture was not imported at {path}.");
            }

            return texture;
        }

        private static void CreateMainMenuScene(
            PanelSettings panelSettings,
            VideoClip menuVideo,
            RenderTexture menuRenderTexture)
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            CreatePresentationCamera();

            GameObject videoObject = new GameObject("Menu Background Video");
            AudioSource audioSource = videoObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 0f;

            VideoPlayer videoPlayer = videoObject.AddComponent<VideoPlayer>();
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = menuVideo;
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = true;
            videoPlayer.waitForFirstFrame = true;
            videoPlayer.skipOnDrop = true;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = menuRenderTexture;
            videoPlayer.aspectRatio = VideoAspectRatio.FitOutside;

            if (menuVideo.audioTrackCount > 0)
            {
                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
                videoPlayer.controlledAudioTrackCount = 1;
                videoPlayer.EnableAudioTrack(0, true);
                videoPlayer.SetTargetAudioSource(0, audioSource);
            }
            else
            {
                videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
            }

            videoObject.AddComponent<LoopingMenuVideoController>();

            GameObject uiRoot = new GameObject("Main Menu");
            UIDocument document = uiRoot.AddComponent<UIDocument>();
            document.panelSettings = panelSettings;
            MainMenuController controller = uiRoot.AddComponent<MainMenuController>();
            controller.SetBackgroundTexture(menuRenderTexture);

            EditorSceneManager.SaveScene(scene, MainMenuScenePath);
        }

        private static void CreatePresentationCamera()
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";

            Camera camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;
            camera.cullingMask = 0;
            camera.orthographic = true;
            camera.allowHDR = false;
            camera.allowMSAA = false;

            cameraObject.AddComponent<AudioListener>();
        }

        private static void ConfigureBuildScenes()
        {
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(MainMenuScenePath, true),
                new EditorBuildSettingsScene(DemoScenePath, true)
            };
        }

        private static void ConfigurePlayerSettings()
        {
            PlayerSettings.companyName = "Independent Creator";
            PlayerSettings.productName = "Five Nights at Lidl";
            PlayerSettings.bundleVersion = "0.1.0";
            PlayerSettings.SetApplicationIdentifier(
                NamedBuildTarget.Standalone,
                "com.independentcreator.fivenightsatlidl");

            PlayerSettings.defaultScreenWidth = 1920;
            PlayerSettings.defaultScreenHeight = 1080;
            PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.runInBackground = false;
            PlayerSettings.colorSpace = ColorSpace.Linear;
        }

        private static void EnsureAssetFolders()
        {
            string[] folders =
            {
                "Assets/Game/Art/Backgrounds",
                "Assets/Game/Art/Characters",
                "Assets/Game/Art/CameraFeeds",
                "Assets/Game/Art/UI",
                "Assets/Game/Art/Effects",
                "Assets/Game/Audio/Ambience",
                "Assets/Game/Audio/Music",
                "Assets/Game/Audio/SFX",
                "Assets/Game/Audio/Voice",
                "Assets/Game/Data/Characters",
                "Assets/Game/Data/Nights",
                "Assets/Game/Data/Balance",
                "Assets/Game/Prefabs",
                "Assets/Game/Scenes/MainMenu",
                "Assets/Game/Scenes/Gameplay",
                "Assets/Game/Scenes/Test",
                "Assets/Game/Settings",
                "Assets/Game/Tests"
            };

            foreach (string folder in folders)
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
