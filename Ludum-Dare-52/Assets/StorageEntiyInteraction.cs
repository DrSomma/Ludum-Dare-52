using System;
using DG.Tweening;
using LudumDare52;
using LudumDare52.Storage;
using UnityEngine;

public class StorageEntiyInteraction : MonoBehaviour
{
    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private EntiyContainer entiyContainer;

    private bool _deledWasTriggerted;
    private float _size;

    private void Start()
    {
        interactable.OnPlayerEnter += OnPlayerIsClose;
        interactable.OnPlayerExit += OnPlayerExit;
        interactable.OnRightClick += OnRightClick;
        _size = transform.localScale.x;
    }

    private void OnRightClick()
    {
        if (_deledWasTriggerted)
        {
            return;
        }

        _deledWasTriggerted = true;
        //storage display will destroy gameobject
        MainStorage.Instance.Container.TryRemoveFromStorage(entiyContainer.Item);
    }

    private void OnPlayerExit()
    {
        transform.DOScale(_size, 0.3f);
    }

    private void OnPlayerIsClose()
    {
        transform.DOScale(_size * 1.1f, 0.3f);
    }
}