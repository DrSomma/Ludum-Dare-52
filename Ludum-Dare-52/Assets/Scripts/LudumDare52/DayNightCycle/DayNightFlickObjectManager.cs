using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class DayNightFlickObjectManager : MonoBehaviour
    {
        [SerializeField]
        private int hourTurnOn;

        private DayNightFlickObject[] _allObjects;

        private float _dayPercentTurnOn;
        private GameObject _flickObjects;

        private bool _offWasFlicked;
        private bool _onWasFlicked;

        private TimeManager _timeManager;

        private void Awake()
        {
            _allObjects = FindObjectsOfType<DayNightFlickObject>();
        }

        private void Start()
        {
            _timeManager = TimeManager.Instance;
            _dayPercentTurnOn = hourTurnOn / 24f;
            Debug.LogWarning(_dayPercentTurnOn);
        }

        private void Update()
        {
            if (_onWasFlicked && _offWasFlicked)
            {
                return;
            }

            if (_timeManager.DayTimeInPercent >= _dayPercentTurnOn)
            {
                TurnOn();
            }
            else if (_timeManager.DayTimeInPercent <= _dayPercentTurnOn)
            {
                TurnOff();
            }
        }

        private void TurnOn()
        {
            _onWasFlicked = true;
            _offWasFlicked = false;
            foreach (DayNightFlickObject flickObject in _allObjects)
            {
                flickObject.TurnOn();
            }
        }

        private void TurnOff()
        {
            _onWasFlicked = false;
            _offWasFlicked = true;
            foreach (DayNightFlickObject flickObject in _allObjects)
            {
                flickObject.TurnOff();
            }
        }
    }
}