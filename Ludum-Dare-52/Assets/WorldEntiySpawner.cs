using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

public class WorldEntiySpawner : Singleton<WorldEntiySpawner>
{
    [SerializeField]
    private GameObject worldEntiyPrefab;

    public void Spawn(Item item, Vector2 position)
    {
        GameObject entiy = Instantiate(worldEntiyPrefab, position, Quaternion.identity);
        WorldEntiyInteraction worldEnity = entiy.GetComponent<WorldEntiyInteraction>();
        worldEnity.SetEnity(item: item, position: position);
    }
}