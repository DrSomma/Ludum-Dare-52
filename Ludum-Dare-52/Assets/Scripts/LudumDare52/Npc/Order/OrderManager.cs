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
    public class OrderManager : MonoBehaviour
    {
        public Order GetNewOrder()
        {
            (int min, int max) = LevelScaleManager.Instance.GetMinMaxCrops(TimeManager.Instance.Day);
            Crop[] filteredList = ResourceSystem.Instance.CropsList.Where(x => Progressmanager.Instance.IsCropActiv(x)).ToArray();
            Crop[] orderList = filteredList.Random(countMin: min, countMax: max);
            return new Order(orderList);
        }
    }
}