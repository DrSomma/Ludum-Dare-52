using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

public class EntiyContainer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriterenderer;

    [SerializeField]
    private BaseAnimation spawnAnimation;
    
    public Item Item { get; private set; }

    public bool IsSpawend { get; private set; }
    
    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void SetEnity(Item item, Vector2 position)
    {
        Item = item;
        spriterenderer.sprite = item.DisplaySprite;
        spawnAnimation.DoAnimation(position, () =>
        {
            IsSpawend = true;
        });
    }
}