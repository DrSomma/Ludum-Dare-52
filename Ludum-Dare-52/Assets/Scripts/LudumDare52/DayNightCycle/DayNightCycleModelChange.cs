using System.Collections.Generic;
using LudumDare52.Daytime;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class DayNightCycleModelChange : MonoBehaviour
    {
        public List<DayNightCycleEntity> DayNightCycleEntities;
        public List<GameObject> ObjectsToActivateWhenEnterNightTime;
        public List<GameObject> ObjectsToActivateWhenEnterDayTime;

        public void Start()
        {
            TimeManager.Instance.onEnterDayTime += OnEnterDayTime;
            TimeManager.Instance.onEnterNightTime += OnEnterNightTime;
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

        private void OnEnterNightTime(float timeUntilFullDarkness)
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