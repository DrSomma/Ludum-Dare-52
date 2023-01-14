using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class MainStorage : Singleton<MainStorage>
    {
        [SerializeField]
        private StoragePositionManager positionManager;

        [SerializeField]
        private ItemStorageContainer container;

        public ItemStorageContainer Container => container;

        private void Start()
        {
            OnNewDay(-1);
            GameManager.Instance.OnStartDay += OnNewDay;
        }

        private void OnNewDay(int day)
        {
            positionManager.ReCalculatePositions();
            int size = positionManager.Count;
            container.SetStorageSize(size);
        }
    }
}