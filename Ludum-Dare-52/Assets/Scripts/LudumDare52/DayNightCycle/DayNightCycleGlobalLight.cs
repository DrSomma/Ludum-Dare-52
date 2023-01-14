using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace LudumDare52.DayNightCycle
{
    public class DayNightCycleGlobalLight : MonoBehaviour
    {
        [SerializeField]
        private Light2D globalLight;

        [SerializeField]
        private Gradient lightColor;

        private void Update()
        {
            globalLight.color = lightColor.Evaluate(TimeManager.Instance.DayTimeInPercent);
        }
    }
}