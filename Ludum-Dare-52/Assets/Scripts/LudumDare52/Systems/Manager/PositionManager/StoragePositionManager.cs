using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class StoragePositionManager : BasePositionManager<StoragePositionManager>
    {
        public List<Vector2> PositonList => Positions.OrderByDescending(pos => pos.y).ThenBy(pos => pos.x).ToList();
    }
}