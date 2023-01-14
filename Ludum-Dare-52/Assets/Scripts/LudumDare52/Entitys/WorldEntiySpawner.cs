using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Entitys
{
    public class WorldEntiySpawner : Singleton<WorldEntiySpawner>
    {
        [SerializeField]
        private GameObject worldEntiyPrefab;

        public void Spawn(Item item, Vector2 position)
        {
            GameObject entiy = Instantiate(worldEntiyPrefab, position, Quaternion.identity);
            EntiyContainer worldEnity = entiy.GetComponent<EntiyContainer>();
            worldEnity.SetEnity(item: item, position: position);
        }
    }
}