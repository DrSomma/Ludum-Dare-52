using System;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare52.Systems.Manager.PositionManager;
using UnityEngine;

namespace LudumDare52.Storage
{
    public class StorageDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject entityContainerPrefab;

        private List<GameObject> _entitys = new (); 

        private void Start()
        {
            StorageManager.Instance.OnAddToStorage += OnAddToStorage;
            StorageManager.Instance.OnRemoveFromStorage += OnRemoveFromStorage;
        }

        private void OnRemoveFromStorage(IStorageable obj)
        {
        }

        private void OnAddToStorage(IStorageable obj)
        {
            Debug.Log("Display");
            GameObject newEntity = Instantiate(entityContainerPrefab);
            _entitys.Add(newEntity);

            newEntity.transform.position = GetStoragePosInGridSpace();
            newEntity.GetComponent<SpriteRenderer>().sprite = obj.DisplaySprite;
            
            Sequence sq = DOTween.Sequence();
            sq.Append(newEntity.transform.DOScale(0,0));
            sq.Append(newEntity.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce));
        }

        private Vector2 GetStoragePosInGridSpace()
        {
            return StoragePositionManager.Instance.PositonList[_entitys.Count-1];
        }
    }
}