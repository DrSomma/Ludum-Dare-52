using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare52.Systems.Manager
{
    public class FieldPositionManager : Singleton<FieldPositionManager>
    {
        [SerializeField]
        private Tilemap tilemap;

        [SerializeField]
        private Sprite centerTile;

        private readonly HashSet<Vector2> _cropPos = new();


        protected override void Awake()
        {
            base.Awake();
            CalculateCropPos();
        }

        public Vector2 GetNearestCropPos(Vector2 playerPos)
        {
            return _cropPos.OrderBy(x => Vector2.Distance(x, playerPos)).First();
        }

        private void CalculateCropPos()
        {
            List<Vector2> allCenterTileWorldPos = GetAllCenterTileWorldPos();
            foreach (Vector2 tile in allCenterTileWorldPos)
            {
                _cropPos.Add(tile + new Vector2(x: 1f, y: 1f));
                _cropPos.Add(tile + new Vector2(x: 0, y: 0));
                _cropPos.Add(tile + new Vector2(x: 1f, y: 0));
                _cropPos.Add(tile + new Vector2(x: 0, y: 1f));
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Vector2 pos in _cropPos)
            {
                Gizmos.DrawCube(center: pos, size: Vector3.one * 0.3f);
            }
        }

        private List<Vector2> GetAllCenterTileWorldPos()
        {
            List<Vector2> cellPos = new();
            foreach (Vector3Int vector3Int in tilemap.cellBounds.allPositionsWithin)
            {
                Sprite sprite = tilemap.GetSprite(vector3Int);
                if (sprite == null || sprite.name != centerTile.name)
                {
                    continue;
                }

                Vector3 cellWorldPos = tilemap.CellToWorld(vector3Int);
                cellPos.Add(cellWorldPos);
            }

            return cellPos;
        }
    }
}