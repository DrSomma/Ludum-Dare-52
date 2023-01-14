using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Random;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Progress;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class DayOrder
    {
        
    }
    
    public class OrderManager : MonoBehaviour
    {
        List<>

        public static Order GetNewOrder()
        {
            Random.InitState(123);
            (int min, int max) = LevelScaleManager.Instance.GetMinMaxCrops(GameManager.Instance.Day);
            Item[] filteredList = ResourceSystem.Instance.ItemList.Where(x => Progressmanager.Instance.IsActiv(x)).ToArray();
            Item[] orderList = filteredList.Random(countMin: min, countMax: max);
            return new Order(orderList);
        }
    }
}