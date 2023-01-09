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
        public FieldProgessStep field;
    }

    [Serializable]
    public struct FieldProgessStep
    {
        public Vector2Int centerTilePos;
        public bool size4X4;
    }

    public class Progressmanager : Singleton<Progressmanager>
    {
        [SerializeField]
        private List<ProgressStep> progressSteps;

        public Action OnUpdate;

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnDayEnd;
        }

        private void OnDayEnd(GameState state)
        {
            if (state != GameState.DayEnd)
            {
                return;
            }

            OnUpdate?.Invoke();
        }


        public bool IsCropActiv(Crop crop)
        {
            int today = TimeManager.Instance.Day;
            Debug.Log("cro: " + crop + " " + progressSteps.Any(x => x.crop == crop && today >= x.day));
            return progressSteps.Any(x => x.crop == crop && today >= x.day);
        }

        public FieldProgessStep? GetTodayFieldUpdate()
        {
            int today = TimeManager.Instance.Day;
            ProgressStep? step = progressSteps.FirstOrDefault(x => today == x.day);
            if (step == null || step.Value.field.centerTilePos == Vector2Int.zero)
            {
                return null;
            }

            return step.Value.field;
        }

        public List<FieldProgessStep> GetAllActivFieldUpgrades()
        {
            int today = TimeManager.Instance.Day;
            return progressSteps.Where(x => today >= x.day && x.field.centerTilePos != Vector2Int.zero).Select(x => x.field).ToList();
        }
    }
}