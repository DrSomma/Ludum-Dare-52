using System;
using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField]
        private float DayLengthInSeconds;

        [SerializeField]
        private int StartHour;

        [SerializeField]
        private int EndHour;

        private float _time;
        private bool _timeIsTicking;
        public Action onEnterDayTime;

        private bool onEnterDayTimeEventFired;
        public Action<float> onEnterNightTime;
        private bool onEnterNightTimeEventFired;

        public float DaytimeInPercent => _time / DayLengthInSeconds;
        public int Day { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            Day = 1;
        }

        private void Update()
        {
            if (!_timeIsTicking)
            {
                return;
            }

            _time += Time.deltaTime;
            if (_time >= DayLengthInSeconds)
            {
                _time = DayLengthInSeconds;
                Day++;
                GameManager.Instance.SetState(GameState.DayEnd);
            }
            else if (Math.Abs(_time - DayLengthInSeconds * 0.75) < 0.5f && !onEnterNightTimeEventFired)
            {
                onEnterNightTime?.Invoke(DayLengthInSeconds * 0.125f);
                onEnterNightTimeEventFired = true;
                onEnterDayTimeEventFired = false;
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
            float dayTimeInPercentCache = DaytimeInPercent;
            int timeRange = EndHour - StartHour;

            int hours = (int) (dayTimeInPercentCache * timeRange);
            int minutes = (int) ((dayTimeInPercentCache * timeRange - hours) * 60);
            return $"{hours + StartHour:00}:{minutes:00}";
        }
    }
}