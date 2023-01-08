using System;
using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    [Serializable]
    public struct Waypoint
    {
        public Vector2 pos;
        public float IdleDelay;
        public float SpeedFactor;
    }
}
