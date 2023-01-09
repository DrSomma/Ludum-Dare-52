using LudumDare52.Systems.Manager;
using TMPro;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txtTime;

        [SerializeField]
        private TextMeshProUGUI txtDay;

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnDayDone;
            SetDay();
        }

        private void Update()
        {
            txtTime.text = TimeManager.Instance.GetTimeAsBeautifulString();
        }

        private void OnDayDone(GameState state)
        {
            if (state != GameState.DayEnd)
            {
                return;
            }

            SetDay();
        }

        private void SetDay()
        {
            txtDay.text = TimeManager.Instance.Day.ToString();
        }
    }
}