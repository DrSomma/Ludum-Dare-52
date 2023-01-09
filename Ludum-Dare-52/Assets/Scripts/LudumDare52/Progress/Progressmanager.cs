using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.DayNightCycle;
using UnityEngine;

namespace LudumDare52.Progress
{
    [System.Serializable]
    public struct ProgressStep
    {
        public int day;
        public Crop crop;
    }
    
    public class Progressmanager : Singleton<Progressmanager>
    {
        [SerializeField]
        private List<ProgressStep> progressSteps;

        public bool IsCropActiv(Crop crop)
        {
            var today = TimeManager.Instance.Day;
            Debug.Log("cro: " + crop + " " + progressSteps.Any(x => x.crop == crop && today >= x.day));
            return progressSteps.Any(x => x.crop == crop && today >= x.day);
        }
    }
}