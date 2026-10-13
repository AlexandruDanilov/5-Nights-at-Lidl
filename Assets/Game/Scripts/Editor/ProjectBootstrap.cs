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
        private const string FitnessCameraFeedPath =
            "Assets/Game/Art/CameraFeeds/Cam03_Fitness.png";
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
        private const string SellyCameraFourPath =
            "Assets/Game/Art/Characters/Selly/Selly_Cam04_Integrated.png";
        private const string SellyCameraFivePath =
            "Assets/Game/Art/Characters/Selly/Selly_Cam05_Integrated.png";
        private const string SellyCameraSevenPath =
            "Assets/Game/Art/Characters/Selly/Selly_Cam07_Integrated.png";
        private const string SellyCameraEightPath =
            "Assets/Game/Art/Characters/Selly/Selly_Cam08_Integrated.png";
        private const string SellyRightDoorPath =
            "Assets/Game/Art/Characters/Selly/Selly_RightDoor_Integrated.png";
        private const string SellyJumpscarePath =
            "Assets/Game/Art/Characters/Selly/Selly_Jumpscare.png";
        private const string CabralCameraOnePath =
            "Assets/Game/Art/Characters/Cabral/Cabral_Cam01_Integrated.png";
        private const string CabralCameraTwoPath =
            "Assets/Game/Art/Characters/Cabral/Cabral_Cam02_Integrated.png";
        private const string CabralCameraSixPath =
            "Assets/Game/Art/Characters/Cabral/Cabral_Cam06_Integrated.png";
        private const string CabralLeftDoorPath =
            "Assets/Game/Art/Characters/Cabral/Cabral_LeftDoor_Integrated.png";
        private const string CabralJumpscarePath =
            "Assets/Game/Art/Characters/Cabral/Cabral_Jumpscare.png";
        private const string DorianHappyPath =
            "Assets/Game/Art/Characters/Dorian/Dorian_Cam03_HappyBlender_Integrated.png";
        private const string DorianIrritatedPath =
            "Assets/Game/Art/Characters/Dorian/Dorian_Cam03_Irritated_Integrated.png";
        private const string DorianAngryPath =
            "Assets/Game/Art/Characters/Dorian/Dorian_Cam03_AngryCamera_Integrated.png";
        private const string DorianJumpscarePath =
            "Assets/Game/Art/Characters/Dorian/Dorian_Jumpscare.png";
        private const string DorianBlenderLoopPath =
            "Assets/Game/Audio/SFX/Dorian_Blender.mp3";
        private const string DorianAlarmLoopPath =
            "Assets/Game/Audio/SFX/Dorian_AlarmLoop.wav";
        private const string BostanicaBeregoiOfficeFightPath =
            "Assets/Game/Art/Characters/BostanicaBeregoi/BostanicaBeregoi_OfficeFightForeground.png";
        private const string BostanicaBeregoiShockedPath =
            "Assets/Game/Art/Characters/BostanicaBeregoi/BostanicaBeregoi_OfficeFightShockedForeground.png";
        private const string BostanicaBeregoiJumpscarePath =
            "Assets/Game/Art/Characters/BostanicaBeregoi/BostanicaBeregoi_Jumpscare.png";
        private const string OfficeSprayBottleForegroundPath =
            "Assets/Game/Art/Characters/BostanicaBeregoi/SprayBottle_OfficeForeground.png";
        private const string OfficeFightPresenceAudioPath =
            "Assets/Game/Audio/SFX/Girls_Taro.mp3";
        private const string OfficeNightFourBackgroundPath =
            "Assets/Game/Art/Backgrounds/OfficeBaseNight4_SprayIntegrated.png";
        private const string NightOnePhoneCallPath =
            "Assets/Game/Audio/Voice/Night01_PhoneCall.mp3";
        private const string NightFourPhoneCallPath =
            "Assets/Game/Audio/Voice/Night04_PhoneCall.mp3";
        private const string NightAmbiencePath =
            "Assets/Game/Audio/Ambience/Manele_Lofi_01.mp3";
        private const string DoorControlAudioPath =
            "Assets/Game/Audio/SFX/Office_Doors.mp3";
        private const string LightControlAudioPath =
            "Assets/Game/Audio/SFX/Office_Lights.mp3";
        private const string UniversalJumpscareAudioPath =
            "Assets/Game/Audio/SFX/Universal_Jumpscare.mp3";
        private const string SellyDoorwayWarningAudioPath =
            "Assets/Game/Audio/SFX/Selly_DoorwayWarning.mp3";
        private const string CabralDoorwayWarningAudioPath =
            "Assets/Game/Audio/SFX/Cabral_DoorwayWarning.mp3";
        private const string CameraMonitorToggleAudioPath =
            "Assets/Game/Audio/SFX/Camera_MonitorToggle.mp3";
        private const string CameraFeedSwitchAudioPath =
            "Assets/Game/Audio/SFX/Camera_FeedSwitch.mp3";
        private const string OfficeFightSprayAudioPath =
            "Assets/Game/Audio/SFX/FasFas_Spray.mp3";
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
            Texture2D fitnessCameraFeed = LoadRequiredTexture(FitnessCameraFeedPath);
            Texture2D detergentsCameraFeed = LoadRequiredTexture(DetergentsCameraFeedPath);
            Texture2D iceCreamCameraFeed = LoadRequiredTexture(IceCreamCameraFeedPath);
            Texture2D checkoutsCameraFeed = LoadRequiredTexture(CheckoutsCameraFeedPath);
            Texture2D stockroomCameraFeed = LoadRequiredTexture(StockroomCameraFeedPath);
            Texture2D employeeRoomCameraFeed = LoadRequiredTexture(EmployeeRoomCameraFeedPath);
            Texture2D bolojanCameraOne = LoadRequiredTexture(BolojanCameraOnePath);
            Texture2D bolojanCameraSeven = LoadRequiredTexture(BolojanCameraSevenPath);
            Texture2D bolojanJumpscare = LoadRequiredTexture(BolojanJumpscarePath);
            Texture2D sellyCameraFour = LoadRequiredTexture(SellyCameraFourPath);
            Texture2D sellyCameraFive = LoadRequiredTexture(SellyCameraFivePath);
            Texture2D sellyCameraSeven = LoadRequiredTexture(SellyCameraSevenPath);
            Texture2D sellyCameraEight = LoadRequiredTexture(SellyCameraEightPath);
            Texture2D sellyRightDoor = LoadRequiredTexture(SellyRightDoorPath);
            Texture2D sellyJumpscare = LoadRequiredTexture(SellyJumpscarePath);
            Texture2D cabralCameraOne = LoadRequiredTexture(CabralCameraOnePath);
            Texture2D cabralCameraTwo = LoadRequiredTexture(CabralCameraTwoPath);
            Texture2D cabralCameraSix = LoadRequiredTexture(CabralCameraSixPath);
            Texture2D cabralLeftDoor = LoadRequiredTexture(CabralLeftDoorPath);
            Texture2D cabralJumpscare = LoadRequiredTexture(CabralJumpscarePath);
            Texture2D dorianHappy = LoadRequiredTexture(DorianHappyPath);
            Texture2D dorianIrritated = LoadRequiredTexture(DorianIrritatedPath);
            Texture2D dorianAngry = LoadRequiredTexture(DorianAngryPath);
            Texture2D dorianJumpscare = LoadRequiredTexture(DorianJumpscarePath);
            AudioClip dorianBlenderLoop = LoadRequiredAudioClip(DorianBlenderLoopPath);
            AudioClip dorianAlarmLoop = LoadRequiredAudioClip(DorianAlarmLoopPath);
            Texture2D bostanicaBeregoiOfficeFight =
                LoadRequiredTexture(BostanicaBeregoiOfficeFightPath);
            Texture2D bostanicaBeregoiShocked =
                LoadRequiredTexture(BostanicaBeregoiShockedPath);
            Texture2D bostanicaBeregoiJumpscare =
                LoadRequiredTexture(BostanicaBeregoiJumpscarePath);
            Texture2D officeSprayBottleForeground =
                LoadRequiredTexture(OfficeSprayBottleForegroundPath);
            AudioClip officeFightPresenceAudio =
                LoadRequiredAudioClip(OfficeFightPresenceAudioPath);
            Texture2D officeNightFourBackground =
                LoadRequiredTexture(OfficeNightFourBackgroundPath);
            AudioClip nightOnePhoneCall =
                LoadRequiredAudioClip(NightOnePhoneCallPath);
            AudioClip nightFourPhoneCall =
                LoadRequiredAudioClip(NightFourPhoneCallPath);
            AudioClip nightAmbience =
                LoadRequiredAudioClip(NightAmbiencePath);
            AudioClip doorControlAudio =
                LoadRequiredAudioClip(DoorControlAudioPath);
            AudioClip lightControlAudio =
                LoadRequiredAudioClip(LightControlAudioPath);
            AudioClip universalJumpscareAudio =
                LoadRequiredAudioClip(UniversalJumpscareAudioPath);
            AudioClip sellyDoorwayWarningAudio =
                LoadRequiredAudioClip(SellyDoorwayWarningAudioPath);
            AudioClip cabralDoorwayWarningAudio =
                LoadRequiredAudioClip(CabralDoorwayWarningAudioPath);
            AudioClip cameraMonitorToggleAudio = LoadRequiredAudioClip(CameraMonitorToggleAudioPath);
            AudioClip cameraFeedSwitchAudio = LoadRequiredAudioClip(CameraFeedSwitchAudioPath);
            AudioClip officeFightSprayAudio = LoadRequiredAudioClip(OfficeFightSprayAudioPath);

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
                fitnessCameraFeed,
                detergentsCameraFeed,
                iceCreamCameraFeed,
                checkoutsCameraFeed,
                stockroomCameraFeed,
                employeeRoomCameraFeed,
                bolojanCameraOne,
                bolojanCameraSeven,
                bolojanJumpscare,
                sellyCameraFour,
                sellyCameraFive,
                sellyCameraSeven,
                sellyCameraEight,
                sellyRightDoor,
                sellyJumpscare,
                cabralCameraOne,
                cabralCameraTwo,
                cabralCameraSix,
                cabralLeftDoor,
                cabralJumpscare,
                dorianHappy,
                dorianIrritated,
                dorianAngry,
                dorianJumpscare,
                dorianBlenderLoop,
                dorianAlarmLoop,
                bostanicaBeregoiOfficeFight,
                bostanicaBeregoiShocked,
                bostanicaBeregoiJumpscare,
                officeSprayBottleForeground,
                officeFightPresenceAudio,
                officeNightFourBackground,
                nightOnePhoneCall,
                nightFourPhoneCall,
                nightAmbience,
                doorControlAudio,
                lightControlAudio,
                universalJumpscareAudio,
                sellyDoorwayWarningAudio,
                cabralDoorwayWarningAudio,
                cameraMonitorToggleAudio,
                cameraFeedSwitchAudio,
                officeFightSprayAudio);
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
            Texture2D fitnessCameraFeed,
            Texture2D detergentsCameraFeed,
            Texture2D iceCreamCameraFeed,
            Texture2D checkoutsCameraFeed,
            Texture2D stockroomCameraFeed,
            Texture2D employeeRoomCameraFeed,
            Texture2D bolojanCameraOne,
            Texture2D bolojanCameraSeven,
            Texture2D bolojanJumpscare,
            Texture2D sellyCameraFour,
            Texture2D sellyCameraFive,
            Texture2D sellyCameraSeven,
            Texture2D sellyCameraEight,
            Texture2D sellyRightDoor,
            Texture2D sellyJumpscare,
            Texture2D cabralCameraOne,
            Texture2D cabralCameraTwo,
            Texture2D cabralCameraSix,
            Texture2D cabralLeftDoor,
            Texture2D cabralJumpscare,
            Texture2D dorianHappy,
            Texture2D dorianIrritated,
            Texture2D dorianAngry,
            Texture2D dorianJumpscare,
            AudioClip dorianBlenderLoop,
            AudioClip dorianAlarmLoop,
            Texture2D bostanicaBeregoiOfficeFight,
            Texture2D bostanicaBeregoiShocked,
            Texture2D bostanicaBeregoiJumpscare,
            Texture2D officeSprayBottleForeground,
            AudioClip officeFightPresenceAudio,
            Texture2D officeNightFourBackground,
            AudioClip nightOnePhoneCall,
            AudioClip nightFourPhoneCall,
            AudioClip nightAmbience,
            AudioClip doorControlAudio,
            AudioClip lightControlAudio,
            AudioClip universalJumpscareAudio,
            AudioClip sellyDoorwayWarningAudio,
            AudioClip cabralDoorwayWarningAudio,
            AudioClip cameraMonitorToggleAudio,
            AudioClip cameraFeedSwitchAudio,
            AudioClip officeFightSprayAudio)
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
                fitnessCameraFeed,
                detergentsCameraFeed,
                iceCreamCameraFeed,
                checkoutsCameraFeed,
                stockroomCameraFeed,
                employeeRoomCameraFeed,
                bolojanCameraOne,
                bolojanCameraSeven,
                bolojanJumpscare,
                sellyCameraFour,
                sellyCameraFive,
                sellyCameraSeven,
                sellyCameraEight,
                sellyRightDoor,
                sellyJumpscare,
                cabralCameraOne,
                cabralCameraTwo,
                cabralCameraSix,
                cabralLeftDoor,
                cabralJumpscare,
                dorianHappy,
                dorianIrritated,
                dorianAngry,
                dorianJumpscare,
                dorianBlenderLoop,
                dorianAlarmLoop,
                bostanicaBeregoiOfficeFight,
                bostanicaBeregoiShocked,
                bostanicaBeregoiJumpscare,
                officeSprayBottleForeground,
                officeFightPresenceAudio,
                officeNightFourBackground,
                nightOnePhoneCall,
                nightFourPhoneCall,
                nightAmbience,
                doorControlAudio,
                lightControlAudio,
                universalJumpscareAudio,
                sellyDoorwayWarningAudio,
                cabralDoorwayWarningAudio,
                cameraMonitorToggleAudio,
                cameraFeedSwitchAudio,
                officeFightSprayAudio);

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

        private static AudioClip LoadRequiredAudioClip(string path)
        {
            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            if (clip == null)
            {
                throw new InvalidOperationException($"Required audio clip was not imported at {path}.");
            }

            return clip;
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
            PlayerSettings.bundleVersion = "0.1.1";
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
