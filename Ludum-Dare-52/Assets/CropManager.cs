using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

public class CropManager : Singleton<CropManager>
{
    [SerializeField]
    private GameObject cropPrefab;

    private readonly Dictionary<Vector2,CropBehavior> Crops = new();

    public void PlantCrop(Vector2 pos, Crop crop)
    {
        Debug.Log("PlantCrop");
        if (!CanPlantOnPos(pos))
        {
        Debug.Log("Cant Pant");
            return;
        }

        GameObject newCropObj = Instantiate(cropPrefab);
        newCropObj.transform.position = pos - Vector2.up*0.5f;
        CropBehavior cropBehavior = newCropObj.GetComponent<CropBehavior>();
        cropBehavior.Crop = crop;
        Crops.Add(pos,cropBehavior);
    }

    private bool CanPlantOnPos(Vector2 checkPos)
    {
        if (Crops.TryGetValue(checkPos, out var crop))
        {
            return crop == null;
        }

        return true;
    }
}