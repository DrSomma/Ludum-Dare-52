using System.Collections.Generic;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Npc.Movement;
using LudumDare52.Storage;
using LudumDare52.Storage.Money;
using LudumDare52.Systems;
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
            bool couldSell = false;
            foreach (KeyValuePair<Item, int> orderItem in order.OrderList)
            {
                while (!order.IsRowFulfilled(orderItem.Key))
                {
                    if (ItemStorageManager.Instance.TryRemoveFromStorage(orderItem.Key))
                    {
                        orderContainer.FulfillItemOrder(orderItem.Key);
                        couldSell = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (!couldSell)
            {
                AudioSystem.Instance.PlaySound(ResourceSystem.Instance.cant);
            }

            if (!order.IsOrderFullfilled())
            {
                return;
            }

            AddMoney(order.GetValue());
            _movement.SendCustomerHome();
        }

        private void AddMoney(int add)
        {
            AudioSystem.Instance.PlaySound(ResourceSystem.Instance.money);
            MoneyManager.Instance.AddMoney(add);
        }
    }
}