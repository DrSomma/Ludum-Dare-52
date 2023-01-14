using System.Collections.Generic;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Entitys.Interactable;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    internal class UiRow
    {
        public UiRow()
        {
            items = new List<UiOrderItem>();
        }

        public List<UiOrderItem> items { get; }
    }

    public class OrderDisplay : MonoBehaviour
    {
        [SerializeField]
        private CustomerOrderContainer container;

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

        private Dictionary<Item, UiRow> _itemToUiRow;

        private void Awake()
        {
            container.OnNewOrder += OnNewOrder;
            container.OnOrderUpdate += OnOrderUpdate;
            interactable.OnPlayerEnter += OnPlayerIsClose;
            interactable.OnPlayerExit += OnPlayerLeft;
            uiContainer.GetComponent<CanvasGroup>().DOFade(endValue: 0, duration: 0);
            interactable.OnCanInteractChanged += OnCanInteractChanged;
        }

        private void OnCanInteractChanged(bool canInteract)
        {
            if (canInteract)
            {
                ShowOrder();
            }
            else
            {
                HideOrder();
            }
        }

        private void HideOrder()
        {
            uiContainer.GetComponent<CanvasGroup>().DOFade(endValue: 0, duration: 0.2f);
        }

        private void ShowOrder()
        {
            uiContainer.GetComponent<CanvasGroup>().DOFade(endValue: 1, duration: 0.2f);
        }

        private void OnOrderUpdate(Item obj)
        {
            foreach (KeyValuePair<Item, int> orderItem in container.Order.ProgressList)
            {
                for (int i = 0; i < orderItem.Value; i++)
                {
                    _itemToUiRow[orderItem.Key].items[i].SetCheck();
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
            _itemToUiRow = new Dictionary<Item, UiRow>();
            foreach (KeyValuePair<Item, int> orderItem in newOrder.OrderList)
            {
                GameObject row = Instantiate(original: uiOrderRowPrefab, parent: uiContainer, worldPositionStays: false);
                UiRow uirow = new();
                for (int i = 0; i < orderItem.Value; i++)
                {
                    GameObject itemContainer = Instantiate(original: uiOrderItemPrefab, parent: row.transform, worldPositionStays: false);
                    UiOrderItem item = itemContainer.GetComponent<UiOrderItem>();
                    item.SetNewOrderItem(orderItem.Key.displaySpriteUi);
                    uirow.items.Add(item);
                }

                _itemToUiRow.Add(key: orderItem.Key, value: uirow);
            }
        }
    }
}