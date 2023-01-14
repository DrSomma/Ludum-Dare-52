using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
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

        //u cant set a Dictionary in the unity inspector
        private Dictionary<int, ProgressStep> _progressSteps; //day -> step 

        public Action<ProgressStep> OnUpdate;

        protected override void Awake()
        {
            base.Awake();
            _progressSteps = progressSteps.ToDictionary(keySelector: x => x.day, elementSelector: x => x);
        }

        public ProgressStep? GetStep(int day)
        {
            if (_progressSteps.TryGetValue(key: day, value: out ProgressStep step))
            {
                return step;
            }

            return null;
        }


        public bool IsActiv(Item item)
        {
            int today = GameManager.Instance.Day;
            return _progressSteps.Values.Any(x => x.crop == item && today >= x.day);
        }

        public int GetFieldUpgradeLevel()
        {
            int today = GameManager.Instance.Day;
            return _progressSteps.Values.Count(x => today >= x.day && x.upgradeField);
        }
    }
}