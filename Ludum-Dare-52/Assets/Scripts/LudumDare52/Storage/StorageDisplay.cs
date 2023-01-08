using System.Collections.Generic;
using DG.Tweening;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class StorageDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject entityContainerPrefab;

        private readonly Dictionary<IStorageable, GameObject> _entitys = new();

        private void Start()
        {
            StorageManager.Instance.OnAddToStorage += OnAddToStorage;
            StorageManager.Instance.OnRemoveFromStorage += OnRemoveFromStorage;
        }

        private void OnRemoveFromStorage(IStorageable obj)
        {
            GameObject entity = _entitys[obj];
            entity.transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(entity); });
            _entitys.Remove(obj);
        }

        private void OnAddToStorage(IStorageable obj)
        {
            Debug.Log("Display");
            GameObject newEntity = Instantiate(entityContainerPrefab);
            _entitys.Add(key: obj, value: newEntity);

            newEntity.transform.position = GetStoragePosInGridSpace();
            newEntity.GetComponent<SpriteRenderer>().sprite = obj.DisplaySprite;

            Sequence sq = DOTween.Sequence();
            sq.Append(newEntity.transform.DOScale(endValue: 0, duration: 0));
            sq.Append(newEntity.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.OutBounce));
        }

        private Vector2 GetStoragePosInGridSpace()
        {
            int index = _entitys.Count - 1;
            return StoragePositionManager.Instance.PositonList[index];
        }
    }
}