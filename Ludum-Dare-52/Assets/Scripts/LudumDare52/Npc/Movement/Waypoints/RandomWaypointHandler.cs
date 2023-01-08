using UnityEngine;

namespace LudumDare52.Npc.Movement.Waypoints
{
    public class RandomWaypointHandler : BaseWaypointHandler
    {
        [SerializeField] private float delayMin = 0.2f;
        [SerializeField] private float delayMax = 0.8f;
        [SerializeField] private Bounds bounds;
    
        private Waypoint RandomPointInBounds(Bounds bounds)
        {
            return new Waypoint()
            {
                pos = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y)),
                IdleDelay = Random.Range(delayMin, delayMax),
                SpeedFactor = 1
            };
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        public override Waypoint GetNext()
        {
            return RandomPointInBounds(bounds);
        }
    }
}
