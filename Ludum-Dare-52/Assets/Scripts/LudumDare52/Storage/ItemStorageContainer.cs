﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public Action<T> OnAddToStorage;
        public Action<T> OnRemoveFromStorage;

        public Storage(int space)
        {
            _maxStorage = space;
        }

        public bool HasSpace => _maxStorage > _storageList.Count;

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

        public bool HasEntiy(T entity)
        {
            return _storageList.Contains(entity);
        }

        public T GetEnity(T key)
        {
            return _storageList.First(x => Equals(x, key));
        }
    }

    public class ItemStorageContainer : MonoBehaviour
    {
        // [Tooltip("use this if u dont use a BasePositionManager")]
        // [SerializeField]
        private const int StartSize = 0;

        public bool HasSpace => Storage.HasSpace;

        public Storage<Item> Storage { get; private set; }

        protected void Awake()
        {
            Storage = new Storage<Item>(StartSize);
        }
        
        public void SetStorageSize(int newsize)
        {
            Storage.SetSize(newsize);
        }

        public void AddToStorage(Item newItem)
        {
            Storage.AddToStorage(newItem);
        }

        public bool HasItem(Item item)
        {
            return Storage.HasEntiy(entity: item);
        }
        
        public bool GetItem(Item item)
        {
            return Storage.GetEnity(key: item);
        }

        public bool TryRemoveFromStorage(Item item)
        {
            return Storage.TryRemoveFromStorage(entity: item);
        }
    }
}