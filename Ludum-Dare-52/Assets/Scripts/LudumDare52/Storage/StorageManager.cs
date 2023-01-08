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

    public record CropStorageEntity : IStorageable
    {
        public CropStorageEntity(Crop crop)
        {
            DisplaySprite = crop.displaySpriteUi;
        }

        public Sprite DisplaySprite { get; }
    }

    public class StorageManager : Singleton<StorageManager>
    {
        public Action<IStorageable> OnAddToStorage;
        public Action<IStorageable> OnRemoveFromStorage;
        private static int MaxStorage => StoragePositionManager.Instance.PositonList.Count;

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
        
        public bool TryRemoveFromStorage(Crop orderItemKey)
        {
            var removeObj= Storage.OfType<CropStorageEntity>().FirstOrDefault(x => orderItemKey);
            if (removeObj == null) return false;
            
            OnRemoveFromStorage?.Invoke(removeObj);
            Storage.Remove(removeObj);

            return true;
        }
    }
}