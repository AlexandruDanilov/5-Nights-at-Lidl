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
        private const string CurrentNightKey = "progress.current_night";
        private const string NightReplayUnlockedKey = "settings.night_replay_unlocked";
        private static int _replayNight;
        private static bool _playingReplay;

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
            return true;
        }

        public static int ConsumeNightToPlay()
        {
            _playingReplay = _replayNight > 0;
            int night = _replayNight > 0 ? _replayNight : CurrentNight;
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
