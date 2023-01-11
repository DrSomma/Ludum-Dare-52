using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class SimpleStorage : MonoBehaviour
    {
        [SerializeField]
        private SimpleStorageDisplay simpleStorageDisplay;

        [SerializeField]
        private ItemStorageContainer container;

        public bool HasSpace => container.HasSpace;
        public Storage<Item> Storage => container.Storage;

        private void Start()
        {
            simpleStorageDisplay.RegisterContainer(container);
            container.SetStorageSize(simpleStorageDisplay.SlotCount);
        }
    }
}