using System.Collections;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

public class CropBehavior : UnityEngine.MonoBehaviour
{
    public Crop Crop { get; set; }

    public bool IsHarvestable { get; private set; }

    [SerializeField]
    private SpriteRenderer renderer;

    private void Start()
    {
        IsHarvestable = false;
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float timeStage = Crop.growtimeInSeconds / Crop.stages.Length;
        foreach (Sprite stage in Crop.stages)
        {
            renderer.sprite = stage;
            yield return new WaitForSeconds(timeStage);
        }

        IsHarvestable = true;
    }
}