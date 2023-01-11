﻿using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Npc.Movement.Waypoints;
using UnityEngine;

namespace LudumDare52.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        [SerializeField]
        private BaseWaypointHandler randomNpcWaypointSystem;

        [SerializeField]
        private GameObject storageEntiyContainerPrefab;
        
        public Crop[] CropsList { get; private set; }
        public Item[] ItemList { get; private set; }
        public BaseWaypointHandler RandomNpcWaypointSystem => randomNpcWaypointSystem;

        public GameObject StorageEntiyContainerPrefab => storageEntiyContainerPrefab;


        public AudioClip plantCrop;
        public AudioClip money;
        public AudioClip progess;
        public AudioClip harvest;
        public AudioClip cant;
        public AudioClip dayStart;
        public AudioClip collectItem;
        public AudioClip placeItemInStorage;

        protected override void Awake()
        {
            base.Awake();
            AssemblyResources();
        }

        private void AssemblyResources()
        {
            //lade scriptableobjects 
            CropsList = Resources.LoadAll<Crop>("Items/Crops");
            ItemList = Resources.LoadAll<Item>("Items");
        }
    }
}