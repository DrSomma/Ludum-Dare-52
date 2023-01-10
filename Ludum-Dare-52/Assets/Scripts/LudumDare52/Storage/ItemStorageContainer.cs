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
    }
    
    public class Storage<T> where T : IStorageable
    {
        private readonly List<T> _storageList = new();
        private int _maxStorage;
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;

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
            OnAddToStorage?.Invoke(newItem);
            _storageList.Add(newItem);
        }

        public bool TryRemoveFromStorage(T entity)
        {
            bool removed = _storageList.Remove(entity);
            if (removed)
            {
                OnRemoveFromStorage?.Invoke(entity);
            }
            return removed;
        }

        public void SetSize(int size)
        {
            _maxStorage = size;
        }
    }

    public class ItemStorageContainer : Singleton<ItemStorageContainer>
    {
        public bool HasSpace => Storage.HasSpace;
        
        [SerializeField]
        private StoragePositionManager positionManager;

        public Storage<Item> Storage { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SetStorage();
        }

        private void Start()
        {
            GameManager.Instance.OnNewDay += OnNewDay;
        }

        private void OnNewDay()
        {
            int size = positionManager.PositonList.Count;
            Storage.SetSize(size);
        }

        private void SetStorage()
        {
            int size = positionManager.PositonList.Count;
            Storage = new Storage<Item>(size);
        }

        public void AddToStorage(Item newItem)
        {
            Storage.AddToStorage(newItem);
        }

        public bool TryRemoveFromStorage(Item item)
        {
            return Storage.TryRemoveFromStorage(entity: item);
        }
    }
}