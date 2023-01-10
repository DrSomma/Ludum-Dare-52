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
        public Action<GameState> OnStateUpdate;
        public Action OnNewDay;
        
        public GameState State { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            OnStateUpdate = null;
            State = GameState.Init;
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForEndOfFrame();
            StartNextDay();
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

        public void StartNextDay()
        {
            //Todo: nutze das Event für alles
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.dayStart);
            TimeManager.Instance.ResetTime();
            MoneyManager.Instance.Reset();
            SetState(GameState.Running);
            OnNewDay?.Invoke();
        }
    }
}