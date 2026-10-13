using UnityEngine;

namespace FiveNightsAtLidl.UI
{
    /// <summary>
    /// Owns the small set of player preferences needed by the menu prototype.
    /// Keeping persistence out of the view makes the UI replaceable later.
    /// </summary>
    internal static class MenuPreferences
    {
        private const string MasterVolumeKey = "settings.master_volume";
        private const string FullscreenKey = "settings.fullscreen";
        private const string AggressionReductionKey = "settings.aggression_reduction";
        private const string CurrentNightKey = "progress.current_night";
        private const string NightReplayUnlockedKey = "settings.night_replay_unlocked";
        private const string CustomNightSettingsKey = "settings.custom_night";
        private static int _replayNight;
        private static bool _playingReplay;
        private static CustomNightSettings _pendingCustomNight;
        public static CustomNightSettings ActiveCustomNight { get; private set; }

        public static CustomNightSettings LoadCustomNightSettings()
        {
            CustomNightSettings settings;
            try
            {
                settings = JsonUtility.FromJson<CustomNightSettings>(
                    PlayerPrefs.GetString(CustomNightSettingsKey, string.Empty));
            }
            catch (System.ArgumentException)
            {
                settings = null;
            }
            settings = settings ?? new CustomNightSettings();
            settings.Validate();
            return settings;
        }

        public static void SaveCustomNightSettings(CustomNightSettings settings)
        {
            settings.Validate();
            PlayerPrefs.SetString(CustomNightSettingsKey, JsonUtility.ToJson(settings));
            Save();
        }

        public static bool SelectCustomNight(CustomNightSettings settings)
        {
            if (!NightReplayUnlocked) return false;
            settings.Validate();
            _pendingCustomNight = settings.Copy();
            _replayNight = 0;
            return true;
        }

        public static bool NightReplayUnlocked =>
            CurrentNight >= 6 || PlayerPrefs.GetInt(NightReplayUnlockedKey, 0) == 1;

        public static bool UnlockNightReplay(string password)
        {
            if (password != "Faiar20")
            {
                return false;
            }

            PlayerPrefs.SetInt(NightReplayUnlockedKey, 1);
            Save();
            return true;
        }

        public static bool SelectReplayNight(int night)
        {
            if (!NightReplayUnlocked || night < 1)
            {
                return false;
            }

            _replayNight = night;
            _pendingCustomNight = null;
            return true;
        }

        public static int ConsumeNightToPlay()
        {
            ActiveCustomNight = _pendingCustomNight;
            _pendingCustomNight = null;
            _playingReplay = _replayNight > 0 || ActiveCustomNight != null;
            int night = ActiveCustomNight != null ? 1
                : (_replayNight > 0 ? _replayNight : CurrentNight);
            _replayNight = 0;
            return night;
        }

        public static float MasterVolume
        {
            get => Mathf.Clamp01(PlayerPrefs.GetFloat(MasterVolumeKey, 0.8f));
            set
            {
                float normalizedValue = Mathf.Clamp01(value);
                AudioListener.volume = normalizedValue;
                PlayerPrefs.SetFloat(MasterVolumeKey, normalizedValue);
            }
        }

        public static bool Fullscreen
        {
            get => PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
            set
            {
                Screen.fullScreen = value;
                PlayerPrefs.SetInt(FullscreenKey, value ? 1 : 0);
            }
        }

        public static int CurrentNight
        {
            get => Mathf.Max(1, PlayerPrefs.GetInt(CurrentNightKey, 1));
            private set => PlayerPrefs.SetInt(CurrentNightKey, Mathf.Max(1, value));
        }

        public static int Aggression
        {
            // Keep the legacy storage sign so existing saves retain their difficulty.
            get => -Mathf.Clamp(PlayerPrefs.GetInt(AggressionReductionKey, 0), -5, 5);
            set
            {
                PlayerPrefs.SetInt(AggressionReductionKey, -Mathf.Clamp(value, -5, 5));
                Save();
            }
        }

        public static void CompleteNight(int completedNight)
        {
            if (_playingReplay)
            {
                return;
            }

            CurrentNight = Mathf.Max(CurrentNight, completedNight + 1);
            Save();
        }

        public static void ResetNightProgress()
        {
            _replayNight = 0;
            CurrentNight = 1;
            _pendingCustomNight = null;
            ActiveCustomNight = null;
            Save();
        }

        public static void Apply()
        {
            AudioListener.volume = MasterVolume;

            bool fullscreen = Fullscreen;
            if (Screen.fullScreen != fullscreen)
            {
                Screen.fullScreen = fullscreen;
            }
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
