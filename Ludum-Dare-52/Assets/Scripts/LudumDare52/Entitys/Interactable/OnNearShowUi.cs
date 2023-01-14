using DG.Tweening;
using UnityEngine;

namespace LudumDare52.Entitys.Interactable
{
    public class OnNearShowUi : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private CanvasGroup uiCanvas;

        private GameObject _ui;

        private void Start()
        {
            interactable.OnPlayerEnter += OnPlayerEnter;
            interactable.OnPlayerExit += OnPlayerExit;
            uiCanvas.DOFade(endValue: 0, duration: 0f);
            _ui = uiCanvas.gameObject;
            _ui.SetActive(false);
        }

        private void OnPlayerExit()
        {
            uiCanvas.DOFade(endValue: 0, duration: 0.3f).OnComplete(() => _ui.SetActive(false));
        }

        private void OnPlayerEnter()
        {
            _ui.SetActive(true);
            uiCanvas.DOFade(endValue: 1, duration: 0.3f);
        }
    }
}