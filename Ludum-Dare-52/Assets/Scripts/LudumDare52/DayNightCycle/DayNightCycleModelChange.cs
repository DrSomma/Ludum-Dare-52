using System.Collections.Generic;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class DayNightCycleModelChange : MonoBehaviour
    {
        public List<DayNightCycleEntity> DayNightCycleEntities;
        public List<GameObject> ObjectsToActivateWhenEnterNightTime;
        public List<GameObject> ObjectsToActivateWhenEnterDayTime;

        public bool IsDayTime;
        private bool isDayTimeCache;

        public void Start()
        {
            // TODO Register OnEnterDay & OnEnterNight

            // GameManager.Instance.OnEnterDayTime += OnEnterDayTime();
            // GameManager.Instance.OnEnterNightTimte += OnEnterNightTime();

            // Debug
            isDayTimeCache = IsDayTime;
        }

        public void Update()
        {
            if (IsDayTime == isDayTimeCache)
            {
                return;
            }

            if (IsDayTime)
            {
                OnEnterDayTime();
            }
            else
            {
                OnEnterNightTime();
            }

            isDayTimeCache = IsDayTime;
        }

        private void OnEnterDayTime()
        {
            foreach (DayNightCycleEntity dayNightCycleEntity in DayNightCycleEntities)
            {
                dayNightCycleEntity.SpriteRenderer.sprite = dayNightCycleEntity.DaySprite;
            }

            ActivateObjects(ObjectsToActivateWhenEnterDayTime);
            DeactivateObjects(ObjectsToActivateWhenEnterNightTime);
        }

        private void OnEnterNightTime()
        {
            foreach (DayNightCycleEntity dayNightCycleEntity in DayNightCycleEntities)
            {
                dayNightCycleEntity.SpriteRenderer.sprite = dayNightCycleEntity.NightSprite;
            }

            ActivateObjects(ObjectsToActivateWhenEnterNightTime);
            DeactivateObjects(ObjectsToActivateWhenEnterDayTime);
        }

        private static void ActivateObjects(List<GameObject> objectsToActivate)
        {
            foreach (GameObject objectToActivate in objectsToActivate)
            {
                objectToActivate.SetActive(true);
            }
        }

        private static void DeactivateObjects(List<GameObject> objectsToDeactivate)
        {
            foreach (GameObject objectToDeactivate in objectsToDeactivate)
            {
                objectToDeactivate.SetActive(false);
            }
        }
    }
}