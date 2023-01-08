using System;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.Crops
{
    public class CropObject : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private CropGrowBehavior behavior;
        
        private void Start()
        {
            interactable.OnLeftClick += OnLeftClick;
            interactable.OnRightClick += OnRightClick;
        }

        private void OnLeftClick()
        {
            Debug.Log("Try Plant");
            PlantCrop();
        }

        private void OnRightClick()
        {
            Debug.Log("Try Harvest");
            HarvestCrop();
        }
        
        private void HarvestCrop()
        {
            Debug.Log("HarvestCrop");
            if (!behavior.IsHarvestable || !CanHarvestHaveStorageLeft())
            {
                Debug.Log($"Can't harvest. Crop:{behavior.IsHarvestable} Storage:{CanHarvestHaveStorageLeft()}");
                return;
            }
            
            behavior.Harvest();
            StorageManager.Instance.AddToStorage(new CropStorageEntity(behavior.Crop));
        }
        
        private void PlantCrop()
        {
            if (behavior.Crop != null)
            {
                return;
            }
            behavior.PlantNewCrop(CropSelector.Instance.GetCrop());
        }
        
        private bool CanHarvestHaveStorageLeft()
        {
            return StorageManager.Instance.HasSpace;
        }
    }
}