using System;
using Amazeit.Utilities.Singleton;
using LudumDare52.DayNightCycle;

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