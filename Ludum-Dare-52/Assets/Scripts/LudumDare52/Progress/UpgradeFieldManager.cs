using System;
using System.Collections.Generic;
using Amazeit.Utilities.Singleton;
using LudumDare52.Systems.Manager;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare52.Progress
{
    [Serializable]
    public struct FieldProgessStep
    {
        public Vector2Int centerTilePos;
        public bool size3X3;
    }

    public class UpgradeFieldManager : Singleton<UpgradeFieldManager>
    {
        [SerializeField]
        private TileBase field;

        [SerializeField]
        private Tilemap fieldMap;

        [SerializeField]
        private List<FieldProgessStep> upgradeSteps;

        [SerializeField]
        private FieldPositionManager positionManager;

        protected void Start()
        {
            GameManager.Instance.OnStartDay += OnStartDay;

            //clear field
            fieldMap.ClearAllTiles();
        }

        private void OnDrawGizmosSelected()
        {
            Color colorLight = new(r: 0, g: 0, b: 1, a: 0.1f);
            foreach (FieldProgessStep step in upgradeSteps)
            {
                Gizmos.color = Color.blue;
                Vector3 center = fieldMap.CellToWorld((Vector3Int) step.centerTilePos) + new Vector3(x: 0.5f, y: 0.5f);
                Gizmos.DrawCube(center: center, size: Vector3.one * 1f);
                BoundsInt area = GetArea(centerPos: step.centerTilePos, is3X3: step.size3X3);

                Gizmos.color = colorLight;
                for (int x = area.xMin; x < area.xMax; x++)
                {
                    for (int y = area.yMin; y < area.yMax; y++)
                    {
                        Vector3 fc = fieldMap.CellToWorld(new Vector3Int(x: x, y: y)) + new Vector3(x: 0.5f, y: 0.5f);
                        Gizmos.DrawCube(center: fc, size: Vector3.one * 1f);
                    }
                }
            }
        }

        private void OnStartDay(int day)
        {
            ProgressStep? step = Progressmanager.Instance.GetStep(day);
            if (!step.HasValue || !step.Value.upgradeField)
            {
                return;
            }

            int upgradeLevel = Progressmanager.Instance.GetFieldUpgradeLevel();
            if (upgradeLevel >= upgradeSteps.Count)
            {
                Debug.LogWarning("more field upgrades than there were defined upgrades");
                return;
            }

            int levelIndex = upgradeLevel - 1;
            UpgradeField(upgradeSteps[levelIndex]);
            positionManager.OnUpgradeField();
        }

        private BoundsInt GetArea(Vector2Int centerPos, bool is3X3)
        {
            if (is3X3)
            {
                return new BoundsInt(position: new Vector3Int(x: centerPos.x - 1, y: centerPos.y - 1), size: new Vector3Int(x: 3, y: 3, z: 1));
            }
            else
            {
                return new BoundsInt(position: new Vector3Int(x: centerPos.x - 1, y: centerPos.y), size: new Vector3Int(x: 3, y: 1, z: 1));
            }
        }

        private void UpgradeField(FieldProgessStep step)
        {
            Vector2Int tilePos = step.centerTilePos;

            BoundsInt area = GetArea(centerPos: tilePos, is3X3: step.size3X3);
            TileBase[] tiles = new TileBase[area.size.x * area.size.y * area.size.z];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = field;
            }

            fieldMap.SetTilesBlock(position: area, tileArray: tiles);
        }
    }
}