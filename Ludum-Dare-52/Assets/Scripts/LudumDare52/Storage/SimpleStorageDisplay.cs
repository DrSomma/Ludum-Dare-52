using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class SimpleStorageDisplay : MonoBehaviour
    {
        [SerializeField]
        private List<SpriteRenderer> displaySlots;

        private ItemStorageContainer _container;

        private Dictionary<SpriteRenderer, Item> _slots;

        public int SlotCount => displaySlots.Count;

        public void RegisterContainer(ItemStorageContainer c)
        {
            _slots = displaySlots.ToDictionary<SpriteRenderer, SpriteRenderer, Item>(keySelector: x => x, elementSelector: _ => null);
            _container = c;
            _container.Storage.OnAddToStorage += OnAddToStorage;
            _container.Storage.OnRemoveFromStorage += OnRemoveFromStorage;
        }

        private void OnRemoveFromStorage(Item item, int index)
        {
            KeyValuePair<SpriteRenderer, Item> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value == item);
            SpriteRenderer entity = entityKeyValuePair.Key;

            entity.transform.DOKill();
            entity.transform.DOScale(endValue: 0, duration: 0.3f);
            _slots[entityKeyValuePair.Key] = null;
        }

        private void OnAddToStorage(Item item, int index)
        {
            KeyValuePair<SpriteRenderer, Item> entityKeyValuePair = _slots.FirstOrDefault(x => x.Value == null);
            SpriteRenderer entity = entityKeyValuePair.Key;

            entity.sprite = item.DisplaySprite;

            _slots[entityKeyValuePair.Key] = item;
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.placeItemInStorage);

            Sequence sq = DOTween.Sequence();
            sq.Append(entity.transform.DOScale(endValue: 0, duration: 0));
            sq.Append(entity.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.OutBounce));
        }
    }
}