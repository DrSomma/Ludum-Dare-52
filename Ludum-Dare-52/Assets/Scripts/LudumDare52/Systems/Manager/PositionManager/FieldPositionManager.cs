using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class FieldPositionManager : BasePositionManager<FieldPositionManager>
    {
        public Vector2 GetNearestCropPos(Vector2 playerPos)
        {
            return Positions.OrderBy(x => Vector2.Distance(a: x, b: playerPos)).First();
        }
    }
}