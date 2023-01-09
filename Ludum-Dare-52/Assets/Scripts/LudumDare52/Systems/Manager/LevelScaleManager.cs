using System;
using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    
    public class LevelScaleManager : Singleton<LevelScaleManager>
    {
        //TODO: Fast and dirty
        [SerializeField]
        private int[] MoneyPerDay = new int[] {50,100,250,300,400,600,900,100 };

        public int GetDayMoney(int day)
        {
            return MoneyPerDay[Math.Max(day, MoneyPerDay.Length-1)];
        }
    }
}