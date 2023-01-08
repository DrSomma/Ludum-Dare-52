using System;
using System.Collections.Generic;
using Amazeit.Utilities;
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
        private static int MaxStorage => StoragePositionManager.Instance.PositonList.Count;
        
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;

        public List<IStorageable> Storage { get; private set; }
        public bool HasSpace => MaxStorage > Storage.Count;

        protected override void Awake()
        {
            base.Awake();
            Storage = new List<IStorageable>();
        }
        
        public void AddToStorage(IStorageable newItem)
        {
            if (!HasSpace)
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