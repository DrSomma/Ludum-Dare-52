using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    public class WaypointListHandler : BaseWaypointHandler
    {
        [SerializeField] private Waypoint[] waypoints;
        private int _nextIndex = 0;
    
        public override Waypoint GetNext()
        {
            var next = waypoints [_nextIndex];
            _nextIndex = (_nextIndex + 1) % waypoints.Length;
            return next;
        }

        public void SetWaypoints(Waypoint[] newWaypoints)
        {
            waypoints = newWaypoints;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawCube(waypoint.pos, Vector3.one * 0.3f);
            }
        }
    }
}
