using System;
using LudumDare52.Entitys.Interactable;
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
                AudioSystem.Instance.PlayCantSound();
                Debug.Log($"Can't harvest. Crop:{behavior.IsHarvestable} Storage:{CanHarvestHaveStorageLeft()}");
                return;
            }
            
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.harvest);

            behavior.Harvest();
            MainStorage.Instance.Container.AddToStorage(behavior.Crop);
        }
        
        private void PlantCrop()
        {
            if (behavior.Crop != null)
            {
                AudioSystem.Instance.PlayCantSound();
                return;
            }
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.plantCrop);
            behavior.PlantNewCrop(CropSelector.Instance.GetCrop());
        }
        
        private bool CanHarvestHaveStorageLeft()
        {
            return MainStorage.Instance.Container.HasSpace;
        }
    }
}