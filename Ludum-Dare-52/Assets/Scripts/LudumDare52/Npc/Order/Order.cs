using System.Collections.Generic;
using System.Linq;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Npc.Order
{
    public class Order
    {
        public Order(Crop[] crop)
        {
            OrderList = crop.GroupBy(l => l).ToDictionary(keySelector: grp => grp.Key, elementSelector: grp => grp.Count());
            Value = crop.Sum(x => x.price);
        }

        public Dictionary<Crop, int> OrderList { get; }

        public int Value { get; }
    }
}