using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Entitys.Interactable
{
    public class WorldEntiyInteraction : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private EntiyContainer entiyContainer;

        private bool _isCollected;
        private bool _playerIsClose;

        private Transform _transform;

        private Item Item => entiyContainer.Item;

        private void Start()
        {
            interactable.OnPlayerEnter += OnPlayerIsClose;
            interactable.OnPlayerExit += OnPlayerExit;
            _transform = transform;
        }

        private void Update()
        {
            if (!_playerIsClose || _isCollected || !MainStorage.Instance.Container.HasSpace || !entiyContainer.IsSpawend)
            {
                return;
            }

            _isCollected = true;

            _transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => Destroy(gameObject)).Play();
            MainStorage.Instance.Container.AddToStorage(Item);
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.collectItem);
        }

        private void OnPlayerExit()
        {
            _playerIsClose = false;
        }

        private void OnPlayerIsClose()
        {
            _playerIsClose = true;
        }
    }
}