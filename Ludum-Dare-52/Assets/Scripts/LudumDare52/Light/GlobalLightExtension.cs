using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace LudumDare52.Light
{
    [RequireComponent(typeof(Light2D))]
    public class GlobalLightExtension : MonoBehaviour
    {
        public Camera Camera;
        public Light2D Light2D;
        private Color initialCameraBackgroundColor;

        private void Start()
        {
            initialCameraBackgroundColor = Camera.backgroundColor;
        }

        private void Update()
        {
            float intensity = Light2D.intensity;
            float grayscaleIntensity = Color.grey.r * (intensity * 2);
            Color newBackgroundColor = initialCameraBackgroundColor * grayscaleIntensity;

            Camera.backgroundColor = newBackgroundColor;
        }
    }
}