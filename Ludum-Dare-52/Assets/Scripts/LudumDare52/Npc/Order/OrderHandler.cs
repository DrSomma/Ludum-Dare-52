using System.Collections.Generic;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Npc.Movement;
using LudumDare52.Storage;
using LudumDare52.Storage.Money;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class OrderHandler : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        private CustomerOrderContainer orderContainer;

        [SerializeField]
        private CustomerMovement _movement;

        private void Start()
        {
            interactable.OnLeftClick += OnLeftClick;
        }

        private void OnLeftClick()
        {
            Debug.Log("OnLeft -> Handle Order");
            Order order = orderContainer.Order;
            foreach (KeyValuePair<Crop, int> orderItem in order.OrderList)
            {
                if(order.IsRowFulfilled(orderItem.Key))
                    continue;
                
                bool hasItem = StorageManager.Instance.TryRemoveFromStorage(orderItem.Key);
                Debug.Log($"Item: {orderItem.Key} hasItem: {hasItem}");
                if (hasItem)
                {
                    orderContainer.FulfillItemOrder(orderItem.Key);
                }
            }

            if (order.IsOrderFullfilled())
            {
                AddMoney(order.GetValue());
                _movement.SendCustomerHome();
            }
        }

        private void AddMoney(int add)
        {
            MoneyManager.Instance.AddMoney(add);
        }
    }
}