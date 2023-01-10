﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    internal class StorageDisplayEntity
    {
        public IStorageable Storageable { get; set; }
        public GameObject GameObject { get; set; }
    }

    public class BaseStorageDisplay : MonoBehaviour
    {

    }

    public class StorageDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject entityContainerPrefab;

        private Dictionary<Vector2, StorageDisplayEntity> _slots = new();

        [SerializeField]
        private StoragePositionManager positionManager;

        private void Start()
        {
            _slots = positionManager.PositonList.ToDictionary<Vector2, Vector2, StorageDisplayEntity>(keySelector: x => x, elementSelector: _ => null);
            ItemStorageContainer.Instance.Storage.OnAddToStorage += OnAddToStorage;
            ItemStorageContainer.Instance.Storage.OnRemoveFromStorage += OnRemoveFromStorage;
        }

        private void OnRemoveFromStorage(IStorageable obj)
        {
            KeyValuePair<Vector2, StorageDisplayEntity> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value?.Storageable == obj);
            StorageDisplayEntity entity = entityKeyValuePair.Value;
            if (entity == null)
            {
                Debug.LogError("somthing is not dispayed!");
                return;
            }

            entity.GameObject.transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(entity.GameObject); });
            _slots[entityKeyValuePair.Key] = null;
        }

        private void OnAddToStorage(IStorageable obj)
        {
            Debug.Log("Display");
            GameObject newGameObjectEntity = Instantiate(entityContainerPrefab);

            Vector2 position = GetStoragePosInGridSpace();

            _slots[position] = new StorageDisplayEntity() {GameObject = newGameObjectEntity, Storageable = obj};

            newGameObjectEntity.transform.position = position;
            newGameObjectEntity.GetComponent<SpriteRenderer>().sprite = obj.DisplaySprite;

            Sequence sq = DOTween.Sequence();
            sq.Append(newGameObjectEntity.transform.DOScale(endValue: 0, duration: 0));
            sq.Append(newGameObjectEntity.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.OutBounce));
        }


        private Vector2 GetStoragePosInGridSpace()
        {
            return positionManager.PositonList.First(x => _slots[x] == null);
        }
    }
}