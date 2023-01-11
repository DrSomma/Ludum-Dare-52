using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class MainStorage : Singleton<MainStorage>
    {
        [SerializeField]
        private BasePositionManager positionManager;
        
        [SerializeField]
        private ItemStorageContainer container;

        public ItemStorageContainer Container => container;
        
        private void Start()
        {
            OnNewDay();
            GameManager.Instance.OnNewDay += OnNewDay;
        }

        private void OnNewDay()
        {
            int size = positionManager.Count;
            container.SetStorageSize(size);
        }
    }
}