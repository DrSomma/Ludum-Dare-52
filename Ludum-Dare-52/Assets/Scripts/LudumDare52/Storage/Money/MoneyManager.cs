using System;
using Amazeit.Utilities.Singleton;
using LudumDare52.DayNightCycle;
using LudumDare52.Systems.Manager;

namespace LudumDare52.Storage.Money
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        public Action<int, int, int> OnUpdateMoney; //value to add, new sum, LevelMoney
        public int Money { get; private set; }

        public int LevelMoney { get; set; }
        public bool HasRequiertMoney => Money >= LevelMoney;

        public void Reset()
        {
            // int diff = Money - LevelMoney;
            // Money = diff;
            // OnUpdateMoney?.Invoke(arg1: diff, arg2: Money, arg3: LevelMoney);
            Money = 0;
            OnUpdateMoney?.Invoke(arg1: 0, arg2: Money, arg3: LevelMoney);
        }

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnState;
        }

        private void OnState(GameState obj)
        {
            if (obj == GameState.Running)
            {
                LevelMoney = LevelScaleManager.Instance.GetDayMoney(TimeManager.Instance.Day);
                OnUpdateMoney?.Invoke(arg1: 0, arg2: Money, arg3: LevelMoney);
            }
        }

        public void AddMoney(int add)
        {
            Money += add;
            OnUpdateMoney?.Invoke(arg1: add, arg2: Money, arg3: LevelMoney);
        }
    }
}