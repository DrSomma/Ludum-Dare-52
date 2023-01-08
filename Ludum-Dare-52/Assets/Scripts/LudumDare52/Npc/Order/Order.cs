using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Npc.Order
{
    public class Order
    {
        private Dictionary<Crop, int> _orderList;
        public Dictionary<Crop, int> OrderList => _orderList;
        public int Value { get; private set; }

        public Order(Crop[] crop)
        {
            _orderList = crop.GroupBy(l => l).ToDictionary(grp => grp.Key, grp => grp.Count());
            Value = crop.Sum(x => x.price);
        }
    }
}