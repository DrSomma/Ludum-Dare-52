using System;
using System.Collections.Generic;
using Amazeit.Utilities;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Storage
{
    public interface IStorageable
    {
        public Sprite DisplaySprite { get; }
    }

    public record CropStorageEntity : IStorageable
    {
        public CropStorageEntity(Crop crop)
        {
            DisplaySprite = crop.displaySpriteUi;
        }
        public Sprite DisplaySprite { get; private set; }
    }

    public class StorageManager : Singleton<StorageManager>
    {
        [SerializeField]
        private int maxStorage;
        
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;

        public List<IStorageable> Storage { get; private set; }
        public bool HasSpace => maxStorage >= Storage.Count;

        private void Start()
        {
            Storage = new List<IStorageable>();
        }

        public void AddToStorage(IStorageable newItem)
        {
            if (HasSpace)
            {
                return;
            }

            Storage.Add(newItem);
            OnAddToStorage?.Invoke(newItem);
        }

        public void RemoveFromStorage(IStorageable newItem)
        {
            Storage.Remove(newItem);
            OnRemoveFromStorage?.Invoke(newItem);
        }
    }
}