using System.Linq;
using LudumDare52.Crops.ScriptableObject;

namespace LudumDare52.Npc.Order
{
    public struct Order
    {
        public Crop[] OrderList { get; set; }
        public int Value => OrderList.Sum(x => x.price);
    }
}