using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class MainStorage : Singleton<MainStorage>
    {
        [SerializeField]
        private ItemStorageContainer container;

        public ItemStorageContainer Container => container;
    }
}