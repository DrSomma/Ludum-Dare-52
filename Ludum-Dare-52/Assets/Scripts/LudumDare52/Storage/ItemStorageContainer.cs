using System;
using System.Linq;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class Storage<T> where T : class
    {
        private int _maxStorage;
        private T[] _storageList;
        public Action<T, int> OnAddToStorage; //T storagePos
        public Action OnClearStorage;
        public Action<T, int> OnRemoveFromStorage; //T storagePos

        public Storage(int space)
        {
            _maxStorage = space;
            _storageList = new T[space];
        }

        public bool HasSpace => _maxStorage > _storageList.Count(x => x != null);

        public void AddToStorage(T newItem)
        {
            if (!HasSpace)
            {
                return;
            }

            for (int i = 0; i < _storageList.Length; i++)
            {
                if (_storageList[i] == null)
                {
                    _storageList[i] = newItem;
                    OnAddToStorage?.Invoke(arg1: newItem, arg2: i);
                    break;
                }
            }
        }

        public bool TryRemoveFromStorage(T entity)
        {
            for (int i = 0; i < _storageList.Length; i++)
            {
                if (_storageList[i] != null && _storageList[i] == entity)
                {
                    _storageList[i] = null;
                    OnRemoveFromStorage?.Invoke(arg1: entity, arg2: i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveFromStorageByIndex(int index)
        {
            if (_storageList[index] != null)
            {
                _storageList[index] = null;
                OnRemoveFromStorage?.Invoke(arg1: _storageList[index], arg2: index);
                return true;
            }


            return false;
        }

        public void SetSize(int size)
        {
            _maxStorage = size;
            _storageList = new T[size];
            OnClearStorage?.Invoke();
        }

        public bool HasEntiy(T entity)
        {
            return _storageList.Contains(entity);
        }

        public T GetEnityByIndex(int index)
        {
            return _storageList[index];
        }
    }

    public class ItemStorageContainer : MonoBehaviour
    {
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

        public bool TryRemoveFromStorage(Item item)
        {
            return Storage.TryRemoveFromStorage(entity: item);
        }

        public bool RemoveFromStorageByIndex(int index)
        {
            return Storage.RemoveFromStorageByIndex(index);
        }
    }
}