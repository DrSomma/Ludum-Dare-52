using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    public class WaypointListHandler : BaseWaypointHandler
    {
        [SerializeField]
        private Waypoint[] waypoints;

        private int _nextIndex;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            foreach (Waypoint waypoint in waypoints)
            {
                Gizmos.DrawCube(center: waypoint.pos, size: Vector3.one * 0.3f);
            }
        }

        public override Waypoint GetNext()
        {
            Waypoint next = waypoints[_nextIndex];
            _nextIndex = (_nextIndex + 1) % waypoints.Length;
            return next;
        }

        public void SetWaypoints(Waypoint[] newWaypoints)
        {
            waypoints = newWaypoints;
        }
    }
}