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
    [SerializeField]
    private float timeInSeconds;
    
    // private Storage<Sto>

    // Start is called before the first frame update
    private void Start()
    {
        interactable.OnPlayerIsClose += OnPlayerIsClose;
        interactable.OnPlayerLeft += OnPlayerLeft;
        interactable.OnLeftClick += OnLeftClick;
        uiCanvas.DOFade(endValue: 0, duration: 0.3f);
    }

    private void OnPlayerLeft()
    {
        uiCanvas.DOFade(endValue: 0, duration: 0.3f);
    }

    private void OnLeftClick()
    {
        bool hasItem = ItemStorageManager.Instance.TryRemoveFromStorage(input);
        if (hasItem)
        {
            StartCoroutine(Produce());
        }
    }

    private IEnumerator Produce()
    {
        yield return new WaitForSeconds(timeInSeconds);
        ItemStorageManager.Instance.AddToStorage(output);
    }

    private void OnPlayerIsClose()
    {
        uiCanvas.DOFade(endValue: 1, duration: 0.3f);
    }
}