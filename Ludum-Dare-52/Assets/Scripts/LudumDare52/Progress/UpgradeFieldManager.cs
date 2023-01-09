using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare52.Progress
{
    public class UpgradeFieldManager : MonoBehaviour
    {
        [SerializeField]
        private TileBase field;

        [SerializeField]
        private Tilemap fieldMap;

        private void Start()
        {
            Progressmanager.Instance.OnUpdate += OnUpdate;
            List<FieldProgessStep> allUpgrades = Progressmanager.Instance.GetAllActivFieldUpgrades();
            foreach (FieldProgessStep step in allUpgrades)
            {
                UpgradeField(step);
            }
        }

        private void OnUpdate()
        {
            FieldProgessStep? update = Progressmanager.Instance.GetTodayFieldUpdate();
            if (update == null)
            {
                return;
            }

            UpgradeField(update.Value);
        }

        private void UpgradeField(FieldProgessStep step)
        {
            Vector2Int tilePos = step.centerTilePos;
            BoundsInt area;
            if (step.size4X4)
            {
                area = new BoundsInt(position: new Vector3Int(x: tilePos.x - 1, y: tilePos.y - 1), size: new Vector3Int(x: 3, y: 3, z: 1));
            }
            else
            {
                area = new BoundsInt(position: new Vector3Int(x: tilePos.x - 1, y: tilePos.y), size: new Vector3Int(x: 3, y: 1, z: 1));
            }

            TileBase[] tiles = new TileBase[area.size.x * area.size.y * area.size.z];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = field;
            }

            fieldMap.SetTilesBlock(position: area, tileArray: tiles);
        }
    }
}