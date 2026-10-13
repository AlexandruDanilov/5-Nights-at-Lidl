using System;
using UnityEngine;

namespace FiveNightsAtLidl.UI
{
    [Serializable]
    internal sealed class CustomNightSettings
    {
        public const int Selly = 0;
        public const int Cabral = 1;
        public const int Dorian = 2;
        public const int Bolojan = 3;
        public const int Girls = 4;
        public static readonly string[] CharacterNames =
            { "SELLY", "CABRAL", "DORIAN", "BOLOJAN", "BOSTANICA / BEREGOI" };

        public int[] Aggression = { 5, 5, 3, 3, 3 };
        public int[] HourlyGrowth = { 0, 0, 0, 0, 0 };
        public int BatteryCapacityPercent = 100;

        public void Validate()
        {
            BatteryCapacityPercent = Mathf.Clamp(BatteryCapacityPercent, 10, 300);
            if (Aggression == null || Aggression.Length != CharacterNames.Length)
                Aggression = new[] { 5, 5, 3, 3, 3 };
            if (HourlyGrowth == null || HourlyGrowth.Length != CharacterNames.Length)
                HourlyGrowth = new int[CharacterNames.Length];
            for (int i = 0; i < CharacterNames.Length; i++)
            {
                Aggression[i] = Mathf.Clamp(Aggression[i], 0, 25);
                HourlyGrowth[i] = Mathf.Clamp(HourlyGrowth[i], 0, 25);
            }
        }

        public int GetAi(int character, int hour = 0)
        {
            // Both zero means disabled; a zero baseline with hourly growth can
            // activate the threat later in the shift.
            return Mathf.Clamp(Aggression[character] + Mathf.Max(0, hour) * HourlyGrowth[character], 0, 25);
        }

        public CustomNightSettings Copy()
        {
            CustomNightSettings copy = (CustomNightSettings)MemberwiseClone();
            copy.Aggression = (int[])Aggression.Clone();
            copy.HourlyGrowth = (int[])HourlyGrowth.Clone();
            copy.Validate();
            return copy;
        }
    }
}
