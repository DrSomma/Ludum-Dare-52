using System;
using UnityEngine;

namespace LudumDare52.Systems
{
    public enum GameState
    {
        Init,
        Running,
        Pause,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State { get; private set; }

        public Action<GameState> OnStateUpdate;
        public Action<Vector2> OnRespawn;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
            OnStateUpdate = null;
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