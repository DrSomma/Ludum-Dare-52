using System;
using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField]
        private float DayLengthInSeconds = 90;

        [SerializeField]
        private int StartHour = 6;

        [SerializeField]
        private int EndHour = 22;

        [SerializeField]
        private int StartDawnHour = 18;

        [Tooltip("The dawn hour is the time, the dawn finished. After this, the darkness will not grow anymore.")]
        [SerializeField]
        private int EndDawnHour = 21;

        private float _time;
        private bool _timeIsTicking;

        private int dayLengthInGameHours;
        private int durationOfDawnInGameHours;

        public Action onEnterDayTime;
        private bool onEnterDayTimeEventFired;
        public Action<float> onEnterNightTime;
        private bool onEnterNightTimeEventFired;
        
        public float TimeGrowMultiplier { get; set; } = 1;
        public float TimeDayMultiplier { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.OnStateUpdate += OnStateUpdate;

            ValidateFieldsAndWarn();

            dayLengthInGameHours = EndHour - StartHour;
            durationOfDawnInGameHours = EndDawnHour - StartDawnHour;
        }

        private void Update()
        {
            if (!_timeIsTicking)
            {
                return;
            }

            _time += Time.deltaTime * TimeDayMultiplier;
            if (_time >= DayLengthInSeconds)
            {
                _time = DayLengthInSeconds;
                GameManager.Instance.SetState(GameState.DayEnd);
            }
            else
            {
                if (Math.Abs(_time - DayLengthInSeconds / dayLengthInGameHours * (StartDawnHour - StartHour)) < 0.1f && !onEnterNightTimeEventFired)
                {
                    onEnterNightTime?.Invoke(DayLengthInSeconds / dayLengthInGameHours * durationOfDawnInGameHours);
                    onEnterNightTimeEventFired = true;
                    onEnterDayTimeEventFired = false;
                }
            }
        }

        private void ValidateFieldsAndWarn()
        {
            if (DayLengthInSeconds < 1)
            {
                Debug.LogWarning($"The field {nameof(DayLengthInSeconds)} should be greater than 0.");
            }

            if (EndHour <= StartHour)
            {
                Debug.LogWarning($"The field {nameof(EndHour)} should be greater than the field {nameof(StartHour)} ({StartHour}).");
            }

            if (EndDawnHour <= StartDawnHour)
            {
                Debug.LogWarning($"The field {nameof(EndDawnHour)} should be greater than the field {nameof(StartDawnHour)} ({StartDawnHour}).");
            }

            if (StartDawnHour < StartHour || StartDawnHour > EndHour)
            {
                Debug.LogWarning($"The field {nameof(StartDawnHour)} should be within the range of {StartHour} and {EndHour}.");
            }

            if (EndDawnHour < StartHour || EndDawnHour > EndHour)
            {
                Debug.LogWarning($"The field {nameof(EndDawnHour)} should be within the range of {StartHour} and {EndHour}.");
            }
        }

        private void OnStateUpdate(GameState obj)
        {
            _timeIsTicking = obj == GameState.Running;

            if (obj == GameState.Running && !onEnterDayTimeEventFired)
            {
                onEnterDayTimeEventFired = true;
                onEnterDayTime?.Invoke();
            }
        }

        public void ResetTime()
        {
            _time = 0;
            onEnterDayTime?.Invoke();
            onEnterDayTimeEventFired = true;
            onEnterNightTimeEventFired = false;
        }

        public string GetTimeAsBeautifulString()
        {
            float dayTimeInPercentCache = _time / DayLengthInSeconds;
            int timeRange = dayLengthInGameHours;

            int hours = (int) (dayTimeInPercentCache * timeRange);
            int minutes = (int) ((dayTimeInPercentCache * timeRange - hours) * 60);
            return $"{hours + StartHour:00}:{minutes:00}";
        }
    }
}
