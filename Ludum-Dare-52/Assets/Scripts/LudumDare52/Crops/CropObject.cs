using System;
using LudumDare52.Storage;
using LudumDare52.Systems;
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
                AudioSystem.Instance.PlaySound(ResourceSystem.Instance.cant, 0.3f);
                Debug.Log($"Can't harvest. Crop:{behavior.IsHarvestable} Storage:{CanHarvestHaveStorageLeft()}");
                return;
            }
            
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.harvest);

            behavior.Harvest();
            StorageManager.Instance.AddToStorage(new CropStorageEntity(behavior.Crop));
        }
        
        private void PlantCrop()
        {
            if (behavior.Crop != null)
            {
                AudioSystem.Instance.PlaySound(ResourceSystem.Instance.cant, 0.3f);
                return;
            }
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.plantCrop);
            behavior.PlantNewCrop(CropSelector.Instance.GetCrop());
        }
        
        private bool CanHarvestHaveStorageLeft()
        {
            return StorageManager.Instance.HasSpace;
        }
    }
}