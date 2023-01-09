using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class StoragePositionManager : BasePositionManager<StoragePositionManager>
    {
        public List<Vector2> PositonList => Positions.OrderByDescending(pos => pos.y).ThenBy(pos => pos.x).ToList();

        protected override void AddPositons(Vector2 tile)
        {
            Positions.Add(tile + new Vector2(x: 1f, y: 1f));
            Positions.Add(tile + new Vector2(x: 0, y: 0));
            Positions.Add(tile + new Vector2(x: 1f, y: 0));
            Positions.Add(tile + new Vector2(x: 0, y: 1f));
            Positions.Add(tile + new Vector2(x: 0.5f, y: 1f));
            Positions.Add(tile + new Vector2(x: 0.5f, y: 0f));
        }
    }
}