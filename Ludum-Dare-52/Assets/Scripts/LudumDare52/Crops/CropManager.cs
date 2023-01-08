using System.Collections.Generic;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.Crops
{
    public class CropManager : Singleton<CropManager>
    {
        [SerializeField]
        private GameObject cropPrefab;

        private readonly Dictionary<Vector2, CropBehavior> _crops = new();
        private CropBehavior _cropBehavior;

        public void PlantCrop(Vector2 pos, Crop crop)
        {
            Debug.Log("PlantCrop");
            if (!CanPlantOnPos(pos))
            {
                Debug.Log("Can't plant.");
                return;
            }

            GameObject newCropObj = Instantiate(cropPrefab);
            newCropObj.transform.position = pos - Vector2.up * 0.2f;
            _cropBehavior = newCropObj.GetComponent<CropBehavior>();
            _cropBehavior.Crop = crop;
            _crops.Add(key: pos, value: _cropBehavior);
        }

        public void HarvestCrop(Vector2 pos)
        {
            Debug.Log("HarvestCrop");
            if (!CanHarvestOnPos(pos) || !CanHarvestHaveStorageLeft() || !_crops.TryGetValue(key: pos, value: out CropBehavior crop))
            {
                Debug.Log($"Can't harvest. Crop:{CanHarvestOnPos(pos)} Storage:{CanHarvestHaveStorageLeft()}");
                return;
            }

            _crops.Remove(pos);
            crop.Harvest();
            StorageManager.Instance.AddToStorage(new CropStorageEntity(crop.Crop));
        }

        public bool CanPlantOnPos(Vector2 checkPos)
        {
            return !_crops.ContainsKey(checkPos);
        }

        private bool CanHarvestHaveStorageLeft()
        {
            return StorageManager.Instance.HasSpace;
        }

        public bool CanHarvestOnPos(Vector2 checkPos)
        {
            return _crops.TryGetValue(key: checkPos, value: out CropBehavior crop) && crop.IsHarvestable;
        }
    }
}