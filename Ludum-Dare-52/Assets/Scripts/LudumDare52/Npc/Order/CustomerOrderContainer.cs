using System;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class CustomerOrderContainer : MonoBehaviour
    {
        private Order _order;
        public Order Order => _order;

        public Action<Order> OnNewOrder;

        public void SetOrder(Order newOrder)
        {
            Debug.Log("Set new order");
            _order = newOrder;
            OnNewOrder?.Invoke(newOrder);
        }
    }
}