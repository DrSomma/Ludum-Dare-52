using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public interface IStorageable
    {
        public Sprite DisplaySprite { get; }
    }

    public class ItemStorageEntity : IStorageable
    {
        public ItemStorageEntity(Item item)
        {
            Item = item;
        }

        public Item Item { get; }
        public Sprite DisplaySprite => Item.displaySpriteUi;
    }

    public class Storage
    {
        private readonly List<IStorageable> _storageList = new();
        private static int MaxStorage => StoragePositionManager.Instance.PositonList.Count;

        public bool HasSpace => MaxStorage > _storageList.Count;

        public void AddToStorage(IStorageable newItem)
        {
            if (!HasSpace)
            {
                return;
            }

            _storageList.Add(newItem);
        }

        public bool TryRemoveFromStorage(Item orderItemKey, out ItemStorageEntity entity)
        {
            entity = _storageList.OfType<ItemStorageEntity>().FirstOrDefault(x => x.Item == orderItemKey);
            if (entity == null)
            {
                return false;
            }

            _storageList.Remove(entity);

            return true;
        }
    }

    public class ItemStorageManager : Singleton<ItemStorageManager>
    {
        private readonly Storage _storage = new();
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;
        public bool HasSpace => _storage.HasSpace;

        public void AddToStorage(Item newItem)
        {
            ItemStorageEntity entiy = new ItemStorageEntity(newItem);
            _storage.AddToStorage(entiy);
            OnAddToStorage?.Invoke(entiy);
        }

        public bool TryRemoveFromStorage(Item item)
        {
            bool hasItem = _storage.TryRemoveFromStorage(orderItemKey: item, entity: out ItemStorageEntity entity);
            if (hasItem)
            {
                OnRemoveFromStorage?.Invoke(entity);
            }

            return true;
        }
    }
}