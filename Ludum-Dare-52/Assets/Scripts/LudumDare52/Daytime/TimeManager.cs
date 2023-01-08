using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Daytime
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField]
        private float dayLengthInSeconds;
        
        private float _time;
        private bool _timeIsTicking;

        public float DaytimeInPercent => _time/dayLengthInSeconds;
        
        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
        }

        private void Update()
        {
            if(!_timeIsTicking)
                return;

            _time += UnityEngine.Time.deltaTime;
            Debug.Log(_time);
            if (_time >= dayLengthInSeconds)
            {
                _time = dayLengthInSeconds;
                GameManager.Instance.SetState(GameState.DayEnd);
            }
        }

        private void OnStateUpdate(GameState obj)
        {
            _timeIsTicking = obj == GameState.Running;
        }
    }
}