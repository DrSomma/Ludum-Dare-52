using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    public abstract class BaseWaypointHandler : MonoBehaviour
    {
        public abstract Waypoint GetNext();
    }
}
