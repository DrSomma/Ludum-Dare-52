using System.Linq;
using Amazeit.Utilities.Random;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.DayNightCycle;
using LudumDare52.Progress;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    //Todo: mach ne Factory 
    public class OrderManager : MonoBehaviour
    {
        public static Order GetNewOrder()
        {
            (int min, int max) = LevelScaleManager.Instance.GetMinMaxCrops(GameManager.Instance.Day);
            Item[] filteredList = ResourceSystem.Instance.ItemList.Where(x => Progressmanager.Instance.IsActiv(x)).ToArray();
            Item[] orderList = filteredList.Random(countMin: min, countMax: max);
            return new Order(orderList);
        }
    }
}