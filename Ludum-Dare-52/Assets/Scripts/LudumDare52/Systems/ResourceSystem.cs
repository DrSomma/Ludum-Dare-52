using System.Collections.Generic;
using Amazeit.Utilities;
using UnityEngine;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        public Crop[] CropsList { get; private set; }
        
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