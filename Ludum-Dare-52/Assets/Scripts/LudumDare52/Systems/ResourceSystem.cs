using Amazeit.Utilities;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Npc.Movement.Waypoints;
using UnityEngine;

namespace LudumDare52.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        [SerializeField] private BaseWaypointHandler randomNpcWaypointSystem;
        public Crop[] CropsList { get; private set; }
        public BaseWaypointHandler RandomNpcWaypointSystem => randomNpcWaypointSystem;

        
        protected override void Awake()
        {
            base.Awake();
            AssemblyResources();
        }

        private void AssemblyResources()
        {
            //lade scriptableobjects 
            CropsList = Resources.LoadAll<Crop>("Crops");
        }
    }
}