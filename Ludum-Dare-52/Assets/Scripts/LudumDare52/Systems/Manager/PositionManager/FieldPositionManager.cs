using System;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class FieldPositionManager : BasePositionManager<FieldPositionManager>
    {
        [SerializeField]
        private GameObject cropPrefab;
        
        private void Start()
        {
            SpawnCrops();
        }

        private void SpawnCrops()
        {
            foreach (Vector2 position in Positions)
            {
                GameObject newCropObj = Instantiate(cropPrefab);
                newCropObj.transform.position = position - Vector2.up * 0.2f;
            }
        }

        public Vector2 GetNearestCropPos(Vector2 playerPos)
        {
            return Positions.OrderBy(x => Vector2.Distance(a: x, b: playerPos)).First();
        }
    }
}