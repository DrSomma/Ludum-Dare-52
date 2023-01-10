using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare52;
using LudumDare52.Animals;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using UnityEngine;


public class TransformerStorageDisplay : MonoBehaviour
{
    [SerializeField]
    private ItemTransformer transformer;

    [SerializeField]
    private GameObject entityContainerPrefab;
    
    private void Start()
    {
        transformer.
    }
}

public class AnimalItemTransformer : ItemTransformer
{
    [SerializeField]
    private List<AnimalBehavior> animals;

    protected override void Start()
    {
        base.Start();
    }
    
    protected override IEnumerator Produce()
    {
        animals.TriggerEeat();
        yield return new WaitForSeconds(timeInSeconds);
        
        ItemStorageContainer.Instance.AddToStorage(output);
    }
    
}

// public class 

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

    [SerializeField]
    private float timeInSeconds;

    private Storage<Item> _storageInput;
    private Storage<Item> _storageOutput;

    protected virtual void Start()
    {
        interactable.OnPlayerIsClose += OnPlayerIsClose;
        interactable.OnPlayerLeft += OnPlayerLeft;
        interactable.OnLeftClick += OnLeftClick;
        uiCanvas.DOFade(endValue: 0, duration: 0.3f);
        _storageInput = new Storage<Item>(1);
        _storageOutput = new Storage<Item>(1);
    }

    private void OnPlayerLeft()
    {
        uiCanvas.DOFade(endValue: 0, duration: 0.3f);
    }

    private void OnLeftClick()
    {
        if (!_storageInput.HasSpace)
        {
            return;
        }
        
        bool hasItem = ItemStorageContainer.Instance.TryRemoveFromStorage(input);
        if (hasItem)
        {
            _storageInput.AddToStorage(input);
            StartCoroutine(Produce());
        }
    }

    protected virtual IEnumerator Produce()
    {
        yield return new WaitForSeconds(timeInSeconds);
        
        ItemStorageContainer.Instance.AddToStorage(output);
    }

    private void OnPlayerIsClose()
    {
        uiCanvas.DOFade(endValue: 1, duration: 0.3f);
    }
}