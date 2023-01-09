using System.Collections.Generic;
using Amazeit.Utilities.Singleton;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class BasePositionManager<T> : Singleton<T> where T : MonoBehaviour
    {
        [SerializeField]
        private Tilemap tilemap;

        [SerializeField]
        private Sprite centerTile;

        protected readonly HashSet<Vector2> Positions = new();
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Vector2 pos in Positions)
            {
                Gizmos.DrawCube(center: pos, size: Vector3.one * 0.3f);
            }
        }

        protected void CalculatePositions()
        {
            List<Vector2> allCenterTileWorldPos = GetAllCenterTileWorldPos();
            foreach (Vector2 tile in allCenterTileWorldPos)
            {
                AddPositons(tile);
            }
        }

        protected virtual void AddPositons(Vector2 tile)
        {
            Positions.Add(tile + new Vector2(x: 1f, y: 1f));
            Positions.Add(tile + new Vector2(x: 0, y: 0));
            Positions.Add(tile + new Vector2(x: 1f, y: 0));
            Positions.Add(tile + new Vector2(x: 0, y: 1f));
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