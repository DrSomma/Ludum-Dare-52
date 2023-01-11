using DG.Tweening;
using LudumDare52;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Storage;
using UnityEngine;

public class WorldEntiyInteraction : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriterenderer;

    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private Rigidbody2D rb;

    private Item _item;
    private bool _playerIsClose;

    private Transform _transform;

    private bool CanCollect { get; set; }

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (!_playerIsClose || !MainStorage.Instance.Container.HasSpace || !CanCollect)
        {
            return;
        }

        CanCollect = false;

        MainStorage.Instance.Container.AddToStorage(_item);
        _transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void SetEnity(Item item, Vector2 position)
    {
        spriterenderer.sprite = item.DisplaySprite;
        _item = item;
        interactable.OnPlayerEnter += OnPlayerIsClose;
        interactable.OnLeftClick += OnPlayerExit;
        DoAnimation(position);
    }

    private void OnPlayerExit()
    {
        _playerIsClose = false;
    }

    private void DoAnimation(Vector2 position)
    {
        float endScale = _transform.localScale.x;
        _transform.transform.localScale = Vector3.zero;

        Vector2 endPos = position - new Vector2(x: Random.Range(minInclusive: -1f, maxInclusive: 1f), y: Random.Range(minInclusive: -1f, maxInclusive: 1f));
        Sequence sq = DOTween.Sequence();
        sq.Append(rb.DOJump(endValue: endPos, jumpPower: 1, numJumps: 2, duration: 0.5f));
        sq.Join(transform.DOScale(endValue: endScale, duration: 0.3f));
        sq.OnComplete(() => CanCollect = true);
    }

    private void OnPlayerIsClose()
    {
        _playerIsClose = true;
    }
}