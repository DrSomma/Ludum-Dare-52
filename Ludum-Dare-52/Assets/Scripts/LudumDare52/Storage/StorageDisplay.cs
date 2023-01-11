using System;
using System.Collections.Generic;
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

    public class StorageDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject entityContainerPrefab;

        [SerializeField]
        private BasePositionManager positionManager;

        [SerializeField]
        private ItemStorageContainer container;

        private Dictionary<Vector2, StorageDisplayEntity> _slots = new();

        private void Start()
        {
            _slots = positionManager.GetPositonList().ToDictionary<Vector2, Vector2, StorageDisplayEntity>(keySelector: x => x, elementSelector: _ => null);
            container.Storage.OnAddToStorage += OnAddToStorage;
            container.Storage.OnRemoveFromStorage += OnRemoveFromStorage;
        }

        public Vector2 GetEnitiyPosInWorld(IStorageable obj)
        {
            return GetDisplayEntity(obj).GameObject.transform.position;
        }

        private StorageDisplayEntity GetDisplayEntity(IStorageable obj)
        {
            KeyValuePair<Vector2, StorageDisplayEntity> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value?.Storageable == obj);
            StorageDisplayEntity entity = entityKeyValuePair.Value;
            if (entity == null)
            {
                throw new IndexOutOfRangeException("somthing is not dispayed!");
            }

            return entity;
        }

        private void OnRemoveFromStorage(IStorageable obj)
        {
            KeyValuePair<Vector2, StorageDisplayEntity> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value?.Storageable == obj);
            StorageDisplayEntity entity = GetDisplayEntity(obj);

            entity.GameObject.transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(entity.GameObject); });
            _slots[entityKeyValuePair.Key] = null;
        }

        private void OnAddToStorage(IStorageable obj)
        {
            GameObject newGameObjectEntity = Instantiate(entityContainerPrefab);

            Vector2 position = GetFirstEmptySlotPositionInGridCoordinates();

            _slots[position] = new StorageDisplayEntity {GameObject = newGameObjectEntity, Storageable = obj};

            newGameObjectEntity.transform.position = position;
            newGameObjectEntity.GetComponent<SpriteRenderer>().sprite = obj.DisplaySprite;

            Sequence sq = DOTween.Sequence();
            sq.Append(newGameObjectEntity.transform.DOScale(endValue: 0, duration: 0));
            sq.Append(newGameObjectEntity.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.OutBounce));
        }


        private Vector2 GetFirstEmptySlotPositionInGridCoordinates()
        {
            return _slots.First(x => x.Value == null).Key;
        }
    }
}