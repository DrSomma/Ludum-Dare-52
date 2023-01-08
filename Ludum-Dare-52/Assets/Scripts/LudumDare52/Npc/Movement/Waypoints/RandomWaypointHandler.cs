using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    public class RandomWaypointHandler : BaseWaypointHandler
    {
        [SerializeField]
        private float delayMin = 2f;

        [SerializeField]
        private float delayMax = 2f;

        [SerializeField]
        private Bounds bounds;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(center: bounds.center, size: bounds.size);
        }

        private Waypoint RandomPointInBounds(Bounds bounds)
        {
            return new Waypoint
            {
                pos = new Vector2(x: Random.Range(minInclusive: bounds.min.x, maxInclusive: bounds.max.x), y: Random.Range(minInclusive: bounds.min.y, maxInclusive: bounds.max.y)),
                IdleDelay = Random.Range(minInclusive: delayMin, maxInclusive: delayMax),
                SpeedFactor = 1
            };
        }

        public override Waypoint GetNext()
        {
            return RandomPointInBounds(bounds);
        }
    }
}