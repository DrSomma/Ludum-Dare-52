using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField]
        private float playtimeDayLengthInSeconds = 90;

        [SerializeField]
        private int startHour = 6;

        [SerializeField]
        private int endHour = 22;

        private float _dayLengthInSeconds;

        private float _endTimeInSeconds;

        private float _time;
        private bool _timeIsTicking;

        public float TimeGrowMultiplier { get; set; } = 1;
        public float TimeDayMultiplier { get; set; } = 1;
        public float DayTimeInPercent { get; private set; }
        
        // public float GameDayPercent => _time / 

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            ResetTime();
        }

        private void Update()
        {
            if (!_timeIsTicking)
            {
                return;
            }

            _time += Time.deltaTime * TimeDayMultiplier;
            DayTimeInPercent = _time / _dayLengthInSeconds;
            if (!(_time >= _endTimeInSeconds))
            {
                return;
            }

            _time = _endTimeInSeconds;
            GameManager.Instance.SetState(GameState.DayEnd);
        }

        private void OnStateUpdate(GameState obj)
        {
            _timeIsTicking = obj == GameState.Running;
        }

        public void ResetTime()
        {
            int dayLengthInGameHours = endHour - startHour;
            float lenghtOneHour = playtimeDayLengthInSeconds / dayLengthInGameHours;
            _dayLengthInSeconds = lenghtOneHour * 24f;

            _endTimeInSeconds = lenghtOneHour * endHour;


            DayTimeInPercent = startHour / 24f;
            _time = lenghtOneHour * startHour;
        }

        public string GetTimeAsBeautifulString()
        {
            int hours = (int) (DayTimeInPercent * 24);
            int minutes = (int) ((DayTimeInPercent * 24 - hours) * 60);
            return $"{hours:00}:{minutes:00}";
        }
    }
}