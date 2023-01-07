using System.Collections.Generic;
using Amazeit.Utilities;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

public class CropManager : Singleton<CropManager>
{
    [SerializeField]
    private GameObject cropPrefab;

    private readonly Dictionary<Vector2, CropBehavior> _crops = new();

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
        CropBehavior cropBehavior = newCropObj.GetComponent<CropBehavior>();
        cropBehavior.Crop = crop;
        _crops.Add(key: pos, value: cropBehavior);
    }

    public void HarvestCrop(Vector2 pos)
    {
        Debug.Log("HarvestCrop");
        if (!CanHarvestOnPos(pos) || !_crops.TryGetValue(key: pos, value: out CropBehavior crop))
        {
            Debug.Log("Can't harvest.");
            return;
        }

        // TODO Crop in Inventar packen
        _crops.Remove(pos);
        crop.Harvest();
        Debug.Log("Harvested crop.");
    }

    public bool CanPlantOnPos(Vector2 checkPos)
    {
        return !_crops.ContainsKey(checkPos);
    }

    public bool CanHarvestOnPos(Vector2 checkPos)
    {
        return _crops.TryGetValue(key: checkPos, value: out CropBehavior crop) && crop.IsHarvestable;
    }
}