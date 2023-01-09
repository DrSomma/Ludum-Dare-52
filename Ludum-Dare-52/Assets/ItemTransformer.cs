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
        bool hasItem = StorageManager.Instance.TryRemoveFromStorage(input);
        if (hasItem)
        {
            StorageManager.Instance.AddToStorage(new ItemStorageEntity(output));
        }
    }

    private void OnPlayerIsClose()
    {
        uiCanvas.DOFade(endValue: 1, duration: 0.3f);
    }
}