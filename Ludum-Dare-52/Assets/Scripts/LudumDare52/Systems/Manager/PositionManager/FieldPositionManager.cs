using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class FieldPositionManager : BaseTilemapPositionCalculator
    {
        [SerializeField]
        private GameObject cropPrefab;

        private Dictionary<Vector2, GameObject> _gridPosToCropEntity;

        protected void Awake()
        {
            _gridPosToCropEntity = new Dictionary<Vector2, GameObject>();
        }

        public void OnUpgradeField()
        {
            RemoveAllCrops();
            CalculatePositions();
            SpawnCropsSlots();
        }

        private void RemoveAllCrops()
        {
            foreach (GameObject crops in _gridPosToCropEntity.Values)
            {
                Destroy(crops);
            }

            _gridPosToCropEntity.Clear();
        }

        private void SpawnCropsSlots()
        {
            foreach (Vector2 position in Positions)
            {
                if (_gridPosToCropEntity.ContainsKey(position))
                {
                    continue;
                }

                GameObject newCropObj = Instantiate(cropPrefab);
                newCropObj.transform.position = position - Vector2.up * 0.2f;
                _gridPosToCropEntity.Add(key: position, value: newCropObj);
            }
        }


        public override List<Vector2> GetPositonList()
        {
            return Positions.ToList();
        }
    }
}