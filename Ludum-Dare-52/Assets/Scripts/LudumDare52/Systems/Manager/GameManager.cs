using System;
using System.Collections;
using Amazeit.Utilities.Singleton;
using LudumDare52.DayNightCycle;
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
            SetState(GameState.Running);
        }

        public void SetState(GameState state)
        {
            State = state;
            OnStateUpdate?.Invoke(state);
        }

        public void StartNextDay()
        {
            TimeManager.Instance.ResetTime();
            SetState(GameState.Running);
        }
    }
}