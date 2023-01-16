using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Npc.Order
{
    [Serializable]
    public class Order
    {
        private readonly int _allOrderItemsCount;

        public Order(Item[] crop)
        {
            OrderList = crop.GroupBy(l => l).ToDictionary(keySelector: grp => grp.Key, elementSelector: grp => grp.Count());
            ProgressList = crop.GroupBy(l => l).ToDictionary(keySelector: grp => grp.Key, elementSelector: _ => 0);
            _allOrderItemsCount = OrderList.Sum(x => x.Value);
        }

        public Dictionary<Item, int> OrderList { get; }
        public Dictionary<Item, int> ProgressList { get; }

        public void FulfillItemOrder(Item orderItemKey)
        {
            if (IsRowFulfilled(orderItemKey))
            {
                return; //WTF? Du bist doch schon durch!
            }

            ProgressList[orderItemKey] += 1;
        }

        public bool IsRowFulfilled(Item key)
        {
            return OrderList[key] <= ProgressList[key];
        }

        public bool IsOrderFullfilled()
        {
            int sum = ProgressList.Sum(x => x.Value);
            return sum == _allOrderItemsCount;
        }

        public int GetValue()
        {
            return ProgressList.Sum(x => x.Key.price * x.Value);
        }
    }
}