using System;
using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    
    public class LevelScaleManager : Singleton<LevelScaleManager>
    {
        //TODO: Fast and dirty
        [SerializeField]
        private int[] MoneyPerDay = new int[] {100,200,350,400,500,600,900,999,999 };

        [SerializeField]
        private int[] CropScaling = new int[] {2,3,4,4,5,6,7,7 };
        
        public int GetDayMoney(int day)
        {
            return MoneyPerDay[Math.Min(day-1, MoneyPerDay.Length-1)];
        }

        public (int min, int max) GetMinMaxCrops(int day)
        {
            var x = CropScaling[Math.Min(day-1, CropScaling.Length - 1)];
            return (1,x);
        }
    }
}