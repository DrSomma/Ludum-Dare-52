using System;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class CustomerOrderContainer : MonoBehaviour
    {
        public Action<Order> OnNewOrder;
        public Order Order { get; private set; }

        public void SetOrder(Order newOrder)
        {
            Debug.Log("Set new order");
            Order = newOrder;
            OnNewOrder?.Invoke(newOrder);
        }
    }
}