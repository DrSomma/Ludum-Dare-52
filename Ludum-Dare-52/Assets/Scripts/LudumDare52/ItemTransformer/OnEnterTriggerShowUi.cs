using DG.Tweening;
using UnityEngine;

namespace LudumDare52.ItemTransformer
{
    public class OnEnterTriggerShowUi : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup uiCanvas;

        private void Start()
        {
            uiCanvas.DOFade(endValue: 0, duration: 0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            uiCanvas.DOFade(endValue: 1, duration: 0.3f);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            uiCanvas.DOFade(endValue: 0, duration: 0.3f);
        }
    }
}