using System;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class CustomerOrderContainer : MonoBehaviour
    {
        public Action<Order> OnNewOrder;
        public Action<Item> OnOrderUpdate;
        public Order Order { get; private set; }


        public void SetOrder(Order newOrder)
        {
            Debug.Log("Set new order");
            Order = newOrder;
            OnNewOrder?.Invoke(newOrder);
        }

        public void FulfillItemOrder(Item orderItemKey)
        {
            Order.FulfillItemOrder(orderItemKey);
            OnOrderUpdate?.Invoke(orderItemKey);
        }
    }
}