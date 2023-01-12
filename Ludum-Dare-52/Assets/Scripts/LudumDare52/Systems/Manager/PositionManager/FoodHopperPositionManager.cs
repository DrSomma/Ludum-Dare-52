using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Systems.Manager.PositionManager
{
    public class FoodHopperPositionManager : BasePositionManager
    {
        [SerializeField]
        private Vector2 positionOffset;

        private void Awake()
        {
            Vector2 pos = (Vector2) transform.position + positionOffset;
            Positions.Add(pos);
        }

        public override List<Vector2> GetPositonList()
        {
            return Positions.ToList();
        }
    }
}