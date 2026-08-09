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
            CurrentNight = Mathf.Max(CurrentNight, completedNight + 1);
            Save();
        }

        public static void ResetNightProgress()
        {
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
