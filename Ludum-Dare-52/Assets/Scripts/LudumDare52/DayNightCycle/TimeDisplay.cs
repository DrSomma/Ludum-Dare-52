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
            GameManager.Instance.OnStartDay += SetDay;
            SetDay(1);
        }

        private void Update()
        {
            txtTime.text = TimeManager.Instance.GetTimeAsBeautifulString();
        }

        private void SetDay(int day)
        {
            txtDay.text = day.ToString();
        }
    }
}