using DG.Tweening;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.Entitys.Interactable
{
    public class StorageEntiyInteraction : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private EntiyContainer entiyContainer;

        private bool _deledWasTriggerted;
        private float _size = -1;

        public int SlotIndex { get; set; }

        private void Start()
        {
            interactable.OnPlayerEnter += OnPlayerIsClose;
            interactable.OnPlayerExit += OnPlayerExit;
            interactable.OnRightClick += OnRightClick;
        }

        private void OnRightClick()
        {
            if (!interactable.CanInteract)
            {
                return;
            }

            interactable.SetCanInteract(false);
            MainStorage.Instance.Container.RemoveFromStorageByIndex(SlotIndex);
        }

        private void OnPlayerExit()
        {
            if (!interactable.CanInteract)
            {
                return;
            }
        
            if (_size == -1)
            {
                _size = transform.localScale.x;
            }
        
            transform.DOKill();
            transform.DOScale(endValue: _size, duration: 0.3f);
        }

        private void OnPlayerIsClose()
        {
            if (!interactable.CanInteract)
            {
                return;
            }

            if (_size == -1)
            {
                _size = transform.localScale.x;
            }

            transform.DOKill();
            transform.DOScale(endValue: _size * 1.7f, duration: 0.3f);
        }
    }
}