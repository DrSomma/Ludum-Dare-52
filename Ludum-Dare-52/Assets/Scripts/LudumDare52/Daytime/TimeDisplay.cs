using TMPro;
using UnityEngine;

namespace LudumDare52.Daytime
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txtTime;

        private void Update()
        {
            txtTime.text = "" +  TimeManager.Instance.DaytimeInPercent;
        }
    }
}