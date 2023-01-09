using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare52;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using UnityEngine;

public class ItemTransformer : MonoBehaviour
{
    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private CanvasGroup uiCanvas;

    [SerializeField]
    private Crop input;
    
    [SerializeField]
    private Crop output;

    // private bool is
    // [SerializeField]
    // private float Time;
    
    // Start is called before the first frame update
    void Start()
    {
        interactable.OnPlayerIsClose += OnPlayerIsClose;
        interactable.OnPlayerLeft += OnPlayerLeft;
        interactable.OnLeftClick += OnLeftClick;
        uiCanvas.DOFade(0, 0.3f);
    }

    private void OnPlayerLeft()
    {
        uiCanvas.DOFade(0, 0.3f);
    }

    private void OnLeftClick()
    {
        Debug.Log("losaaaaaa");

        bool hasItem = StorageManager.Instance.TryRemoveFromStorage(input);
        if (hasItem)
        {
            StorageManager.Instance.AddToStorage(new CropStorageEntity(output));
        }
    }

    private void OnPlayerIsClose()
    {
        Debug.Log("losaaaaaa");

        uiCanvas.DOFade(1, 0.3f);
    }
    
}
