using System.Collections.Generic;
using System.Linq;
using LudumDare52.Progress;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class FieldPositionManager : BasePositionManager<FieldPositionManager>
    {
        [SerializeField]
        private GameObject cropPrefab;

        private HashSet<Vector2> cropIsOnPos;

        protected override void Awake()
        {
            base.Awake();
            cropIsOnPos = new HashSet<Vector2>();
            UpgradeFieldManager.Instance.OnUpgradeField += OnUpgradeField;
        }

        private void OnUpgradeField()
        {
            CalculatePositions();

            SpawnCropsSlots();
        }

        private void SpawnCropsSlots()
        {
            foreach (Vector2 position in Positions)
            {
                if (cropIsOnPos.Contains(position))
                {
                    continue;
                }

                GameObject newCropObj = Instantiate(cropPrefab);
                newCropObj.transform.position = position - Vector2.up * 0.2f;
                cropIsOnPos.Add(position);
            }
        }

        public Vector2 GetNearestCropPos(Vector2 playerPos)
        {
            return Positions.OrderBy(x => Vector2.Distance(a: x, b: playerPos)).First();
        }
    }
}