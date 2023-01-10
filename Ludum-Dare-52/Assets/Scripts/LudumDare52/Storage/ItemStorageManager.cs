using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems.Manager;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public interface IStorageable
    {
        public Sprite DisplaySprite { get; }

        public bool EqualKey(object obj);
    }

    public class ItemStorageEntity : IStorageable
    {
        public ItemStorageEntity(Item item)
        {
            Item = item;
        }

        public Item Item { get; }
        public Sprite DisplaySprite => Item.displaySpriteUi;

        public bool EqualKey(object obj)
        {
            if (obj is Item other)
            {
                return other == Item;
            }

            return false;
        }
    }

    public class Storage<T> where T : IStorageable
    {
        private readonly List<T> _storageList = new();
        private int _maxStorage;

        public bool HasSpace => _maxStorage > _storageList.Count;

        public Storage(int space)
        {
            _maxStorage = space;
        }
        
        public void AddToStorage(T newItem)
        {
            if (!HasSpace)
            {
                return;
            }

            _storageList.Add(newItem);
        }

        public bool TryRemoveFromStorage(object key, out T entity)
        {
            entity = _storageList.FirstOrDefault(x => x.EqualKey(key));
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
        private Storage<ItemStorageEntity> _storage;
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;
        public bool HasSpace => _storage.HasSpace;

        private void Start()
        {
            GameManager.Instance.OnNewDay += OnNewDay;
        }

        private void OnNewDay()
        {
            int size = StoragePositionManager.Instance.PositonList.Count;
            _storage = new Storage<ItemStorageEntity>(size);
        }

        public void AddToStorage(Item newItem)
        {
            ItemStorageEntity entiy = new(newItem);
            
            _storage.AddToStorage(entiy);
            OnAddToStorage?.Invoke(entiy);
        }

        public bool TryRemoveFromStorage(Item item)
        {
            bool hasItem = _storage.TryRemoveFromStorage(key: item, entity: out ItemStorageEntity entity);
            if (hasItem)
            {
                OnRemoveFromStorage?.Invoke(entity);
            }

            return hasItem;
        }
    }
}