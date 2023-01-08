using Amazeit.Utilities.Random;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class OrderManager : MonoBehaviour
    {
        public Order GetNewOrder()
        {
            Crop[] orderList = ResourceSystem.Instance.CropsList.Random(0, 5);
            return new Order {OrderList = orderList};
        }
    }
}