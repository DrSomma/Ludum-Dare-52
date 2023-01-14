using System;
using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    public class LevelScaleManager : Singleton<LevelScaleManager>
    {
        //TODO: Fast and dirty
        [SerializeField]
        private int[] MoneyPerDay = {100, 200, 350, 400, 500, 600, 900, 999, 999};

        [SerializeField]
        private int[] CropScaling = {2, 3, 4, 4, 5, 6, 7, 7};

        public int GetDayMoney(int day)
        {
            return MoneyPerDay[Math.Min(val1: day - 1, val2: MoneyPerDay.Length - 1)];
        }

        public (int min, int max) GetMinMaxCrops(int day)
        {
            int x = CropScaling[Math.Min(val1: day - 1, val2: CropScaling.Length - 1)];
            return (1, x);
        }
    }
}