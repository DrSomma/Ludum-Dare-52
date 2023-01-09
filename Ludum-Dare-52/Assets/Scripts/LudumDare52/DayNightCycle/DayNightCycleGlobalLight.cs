using System.Collections;
using LudumDare52.Daytime;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace LudumDare52.DayNightCycle
{
    public class DayNightCycleGlobalLight : MonoBehaviour
    {
        public Light2D GlobalLight;
        public float TargetIntensity;

        private float initialLightIntensity;

        public void Start()
        {
            initialLightIntensity = GlobalLight.intensity;

            TimeManager.Instance.onEnterDayTime += OnEnterDayTime;
            TimeManager.Instance.onEnterNightTime += OnEnterNightTime;
        }

        private void OnEnterNightTime(float timeUntilFullDarkness)
        {
            StartCoroutine(FadeLight(targetIntensity: TargetIntensity, duration: timeUntilFullDarkness));
        }

        private void OnEnterDayTime()
        {
            GlobalLight.intensity = initialLightIntensity;
        }

        private IEnumerator FadeLight(float targetIntensity, float duration)
        {
            float currentIntensity = GlobalLight.intensity;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                GlobalLight.intensity = Mathf.Lerp(a: currentIntensity, b: targetIntensity, t: elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            GlobalLight.intensity = targetIntensity;
        }
    }
}