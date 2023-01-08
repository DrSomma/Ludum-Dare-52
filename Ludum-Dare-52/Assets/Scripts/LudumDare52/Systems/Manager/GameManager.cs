using System;
using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    public enum GameState
    {
        Init,
        Running,
        Pause,
        GameOver
    }


    public class GameManager : Singleton<GameManager>
    {
        public Action<Vector2> OnRespawn;

        public Action<GameState> OnStateUpdate;
        public GameState State { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            OnStateUpdate = null;
            OnRespawn = null;
            State = GameState.Init;
        }

        public void SetState(GameState state)
        {
            State = state;
            OnStateUpdate?.Invoke(state);
        }

        public void SetRespawn(Vector2 respawnPos)
        {
            OnRespawn?.Invoke(respawnPos);
        }
    }
}