using System.Collections;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.ItemTransformer
{
    public class ItemTransformer : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private CanvasGroup uiCanvas;

        [SerializeField]
        protected Item input;

        [SerializeField]
        protected Item output;

        [SerializeField]
        protected float timeInSeconds;

        [SerializeField]
        protected ItemStorageContainer containerInput;

        [SerializeField]
        protected ItemStorageContainer containerOutput;

        protected virtual void Start()
        {
            interactable.OnPlayerEnter += OnPlayerIsClose;
            interactable.OnPlayerLeft += OnPlayerLeft;
            interactable.OnPlayerExit += OnLeftClick;
            uiCanvas.DOFade(endValue: 0, duration: 0.3f);
        }

        private void OnPlayerLeft()
        {
            uiCanvas.DOFade(endValue: 0, duration: 0.3f);
        }

        private void OnLeftClick()
        {
            if (!containerInput.HasSpace || !MainStorage.Instance.Container.HasItem(input))
            {
                return;
            }

            MainStorage.Instance.Container.TryRemoveFromStorage(input);
            containerInput.Storage.AddToStorage(input);

            StartCoroutine(Produce());
        }

        protected virtual IEnumerator Produce()
        {
            yield return new WaitForSeconds(timeInSeconds);

            MainStorage.Instance.Container.AddToStorage(output);
        }

        private void OnPlayerIsClose()
        {
            uiCanvas.DOFade(endValue: 1, duration: 0.3f);
        }
    }
}