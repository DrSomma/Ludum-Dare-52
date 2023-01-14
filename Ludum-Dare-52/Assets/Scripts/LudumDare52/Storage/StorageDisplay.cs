using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Entitys;
using LudumDare52.Entitys.Interactable;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    internal class StorageDisplayEntity
    {
        public Item Storageable { get; set; }
        public GameObject GameObject { get; set; }
    }

    public class StorageDisplay : MonoBehaviour
    {
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
            container.Storage.OnClearStorage += OnClearStorage;
        }

        private void OnClearStorage()
        {
            foreach (Vector2 pos in _slots.Keys.ToList())
            {
                StorageDisplayEntity entity = _slots[pos];
                if (entity == null)
                {
                    continue;
                }

                DestroyImmediate(entity.GameObject);
                _slots[pos] = null;
            }
        }


        private void OnRemoveFromStorage(Item item, int index)
        {
            KeyValuePair<Vector2, StorageDisplayEntity> entityKeyValuePair = _slots.ElementAt(index);
            // KeyValuePair<Vector2, StorageDisplayEntity> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value?.Storageable == item);
            StorageDisplayEntity entity = entityKeyValuePair.Value;

            entity.GameObject.transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(entity.GameObject); });
            _slots[entityKeyValuePair.Key] = null;
        }

        private void OnAddToStorage(Item item, int index)
        {
            // Vector2 position = GetFirstEmptySlotPositionInGridCoordinates();
            Vector2 position = _slots.ElementAt(index).Key;

            GameObject newGameObjectEntity = Instantiate(original: ResourceSystem.Instance.StorageEntiyContainerPrefab, position: position, rotation: Quaternion.identity);

            _slots[position] = new StorageDisplayEntity {GameObject = newGameObjectEntity, Storageable = item};
            EntiyContainer con = newGameObjectEntity.GetComponent<EntiyContainer>();
            newGameObjectEntity.GetComponent<StorageEntiyInteraction>().SlotIndex = index;

            con.SetEnity(item: item, position: position);
        }


        private Vector2 GetFirstEmptySlotPositionInGridCoordinates()
        {
            return _slots.First(x => x.Value == null).Key;
        }
    }
}