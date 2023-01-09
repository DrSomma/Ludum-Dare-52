using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.DayNightCycle;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Progress
{
    [Serializable]
    public struct ProgressStep
    {
        public int day;
        public Crop crop;
        public bool upgradeField;
    }
    
    public class Progressmanager : Singleton<Progressmanager>
    {
        [SerializeField]
        private List<ProgressStep> progressSteps;

        public Action<ProgressStep> OnUpdate;

        //u cant set a Dictionary in the unity inspector
        private Dictionary<int, ProgressStep> _progressSteps; //day -> step 

        protected override void Awake()
        {
            base.Awake();
            _progressSteps = progressSteps.ToDictionary(x => x.day, x => x);
        }

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnDayEnd;
        }

        private void OnDayEnd(GameState state)
        {
            int today = TimeManager.Instance.Day;
            if (state != GameState.DayEnd || _progressSteps.ContainsKey(today))
            {
                return;
            }
            OnUpdate?.Invoke(_progressSteps[today]);
        }
        
        
        public bool IsCropActiv(Crop crop)
        {
            int today = TimeManager.Instance.Day;
            return _progressSteps.Values.Any(x => x.crop == crop && today >= x.day);
        }
        
        public int GetFieldUpgradeLevel()
        {
            int today = TimeManager.Instance.Day;
            return _progressSteps.Values.Count(x => today >= x.day && x.upgradeField);
        }
    }
}