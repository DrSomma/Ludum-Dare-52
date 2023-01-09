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

    public class CropStorageEntity : IStorageable
    {
        private Crop _crop;
        public Sprite DisplaySprite => _crop.displaySpriteUi;

        public Crop Crop => _crop;
        
        public CropStorageEntity(Crop crop)
        {
            _crop = crop;
        }
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
            CropStorageEntity removeObj = Storage.OfType<CropStorageEntity>().FirstOrDefault(x => x.Crop == orderItemKey);
            if (removeObj == null)
            {
                return false;
            }

            OnRemoveFromStorage?.Invoke(removeObj);
            Storage.Remove(removeObj);

            return true;
        }
    }
}