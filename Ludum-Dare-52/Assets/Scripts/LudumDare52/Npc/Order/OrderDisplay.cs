using System.Collections.Generic;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    internal class UiRow
    {
        public List<UiOrderItem> items { get; }

        public UiRow()
        {
            items = new List<UiOrderItem>();
        }
    }

    public class OrderDisplay : MonoBehaviour
    {
        [SerializeField]
        private CustomerOrderContainer _container;

        [SerializeField]
        private Interactable interactable;

        [Header("Ui")]
        [SerializeField]
        private GameObject uiOrderRowPrefab;

        [SerializeField]
        private GameObject uiOrderItemPrefab;

        [SerializeField]
        private Transform uiContainer;

        [SerializeField]
        private Canvas canvas;

        private Dictionary<Crop, UiRow> _cropToUiRow;

        private void Awake()
        {
            _container.OnNewOrder += OnNewOrder;
            _container.OnOrderUpdate += OnOrderUpdate;
            interactable.OnPlayerIsClose += OnPlayerIsClose;
            interactable.OnPlayerLeft += OnPlayerLeft;
        }

        private void OnOrderUpdate(Crop obj)
        {
            Debug.Log("Update Order Ui");
            foreach (KeyValuePair<Crop, int> orderItem in _container.Order.ProgressList)
            {
                for (int i = 0; i < orderItem.Value; i++)
                {
                    _cropToUiRow[orderItem.Key].items[i].SetCheck();
                }
            }
        }

        private void OnPlayerIsClose()
        {
            uiContainer.DOKill();
            uiContainer.transform.DOScale(endValue: 2, duration: 0.3f);
            canvas.sortingOrder = 1;
        }
        
        private void OnPlayerLeft()
        {
            uiContainer.DOKill();
            uiContainer.transform.DOScale(endValue: 1, duration: 0.3f);
            canvas.sortingOrder = 0;
        }
        

        private void OnNewOrder(Order newOrder)
        {
            Debug.Log("Display new Order");
            _cropToUiRow = new Dictionary<Crop, UiRow>();
            foreach (KeyValuePair<Crop, int> orderItem in newOrder.OrderList)
            {
                GameObject row = Instantiate(original: uiOrderRowPrefab, parent: uiContainer, worldPositionStays: false);
                UiRow uirow = new UiRow();
                for (int i = 0; i < orderItem.Value; i++)
                {
                    GameObject itemContainer = Instantiate(original: uiOrderItemPrefab, parent: row.transform, worldPositionStays: false);
                    UiOrderItem item = itemContainer.GetComponent<UiOrderItem>();
                    item.SetNewOrderItem(orderItem.Key.displaySpriteUi);
                    uirow.items.Add(item);
                }

                _cropToUiRow.Add(key: orderItem.Key, value: uirow);
            }
        }
    }
}