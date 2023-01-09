using System;
using System.Collections.Generic;
using Amazeit.Utilities.Singleton;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare52.Progress
{
    [Serializable]
    public struct FieldProgessStep
    {
        public Vector2Int centerTilePos;
        public bool size4X4;
    }

    public class UpgradeFieldManager : Singleton<UpgradeFieldManager>
    {
        [SerializeField]
        private TileBase field;

        [SerializeField]
        private Tilemap fieldMap;

        [SerializeField]
        private List<FieldProgessStep> upgradeSteps;
        
        private int _upgradeLevel;

        public Action OnUpgradeField;

        protected void Start()
        {
            Progressmanager.Instance.OnUpdate += OnUpdate;
            _upgradeLevel = Progressmanager.Instance.GetFieldUpgradeLevel();

            //clear field
            fieldMap.ClearAllTiles();

            for (int index = 0; index < _upgradeLevel; index++)
            {
                FieldProgessStep step = upgradeSteps[index];
                UpgradeField(step);
            }
            OnUpgradeField?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Color colorLight = new(r: 0, g: 0, b: 1, a: 0.1f);
            foreach (FieldProgessStep step in upgradeSteps)
            {
                Gizmos.color = Color.blue;
                Vector3 center = fieldMap.CellToWorld((Vector3Int) step.centerTilePos) + new Vector3(x: 0.5f, y: 0.5f);
                Gizmos.DrawCube(center: center, size: Vector3.one * 1f);
                BoundsInt area = GetArea(centerPos: step.centerTilePos, is4x4: step.size4X4);

                Gizmos.color = colorLight;
                for (int x = area.xMin; x < area.xMax; x++)
                {
                    for (int y = area.yMin; y < area.yMax; y++)
                    {
                        Vector3 fc = fieldMap.CellToWorld(new Vector3Int(x: x, y: y)) + new Vector3(x: 0.5f, y: 0.5f);
                        ;
                        Gizmos.DrawCube(center: fc, size: Vector3.one * 1f);
                    }
                }
            }
        }

        private void OnUpdate(ProgressStep step)
        {
            if (!step.upgradeField || _upgradeLevel + 1 >= upgradeSteps.Count)
            {
                return;
            }

            _upgradeLevel++;
            UpgradeField(upgradeSteps[_upgradeLevel]);
            OnUpgradeField?.Invoke();
        }

        private BoundsInt GetArea(Vector2Int centerPos, bool is4x4)
        {
            if (is4x4)
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

            BoundsInt area = GetArea(centerPos: tilePos, is4x4: step.size4X4);
            TileBase[] tiles = new TileBase[area.size.x * area.size.y * area.size.z];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = field;
            }

            fieldMap.SetTilesBlock(position: area, tileArray: tiles);
        }
    }
}