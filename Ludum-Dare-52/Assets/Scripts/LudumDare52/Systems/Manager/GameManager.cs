using System;
using System.Collections;
using Amazeit.Utilities.Singleton;
using LudumDare52.DayNightCycle;
using LudumDare52.Storage.Money;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    public enum GameState
    {
        Init,
        Running,
        DayEnd,
        Pause,
        GameOver
    }


    public class GameManager : Singleton<GameManager>
    {
        public Action<int> OnStartDay; //day
        public Action<GameState> OnStateUpdate;

        public GameState State { get; private set; }

        public int Day { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();
            OnStateUpdate = null;
            State = GameState.Init;
            Day = 1;
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForEndOfFrame();
            StartDay();
        }

        public void SetState(GameState state)
        {
            if (state == GameState.DayEnd)
            {
                //check if day end or game over 
                if (IsGameOver())
                {
                    state = GameState.GameOver;
                }
            }

            State = state;
            OnStateUpdate?.Invoke(state);
        }

        private bool IsGameOver()
        {
            return !MoneyManager.Instance.HasRequiertMoney;
        }

        private void StartDay()
        {
            //Todo: nutze das Event für alles
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.dayStart);
            TimeManager.Instance.ResetTime();
            MoneyManager.Instance.Reset();
            OnStartDay?.Invoke(Day);
            SetState(GameState.Running);
        }

        public void StartDayNext()
        {
            Day++;
            StartDay();
        }

        public void RestartDay()
        {
            StartDay();
        }
    }
}