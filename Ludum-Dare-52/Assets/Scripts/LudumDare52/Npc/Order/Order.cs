using System.Collections.Generic;
using System.Linq;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Npc.Order
{
    public class Order
    {
        private readonly int _allOrderItemsCount;

        public Order(Crop[] crop)
        {
            OrderList = crop.GroupBy(l => l).ToDictionary(keySelector: grp => grp.Key, elementSelector: grp => grp.Count());
            ProgressList = crop.GroupBy(l => l).ToDictionary(keySelector: grp => grp.Key, elementSelector: _ => 0);
            // Value = crop.Sum(x => x.price);
            _allOrderItemsCount = OrderList.Sum(x => x.Value);
        }

        public Dictionary<Crop, int> OrderList { get; }
        public Dictionary<Crop, int> ProgressList { get; }

        // public int Value { get; }

        public void FulfillItemOrder(Crop orderItemKey)
        {
            if (OrderList[orderItemKey] <= ProgressList[orderItemKey])
            {
                return; //WTF? Du bist doch schon durch!
            }
            ProgressList[orderItemKey] += 1;
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