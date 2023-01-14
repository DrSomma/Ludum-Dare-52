using System.Collections.Generic;
using UnityEngine;

namespace LudumDare52.DayNightCycle
{
    public class DayNightFlickObject : MonoBehaviour
    {
        [SerializeField]
        private List<DayNightCycleEntity> dayNightCycleEntities;

        [SerializeField]
        private List<GameObject> objectsToActivateWhenEnterNightTime;

        [SerializeField]
        private List<GameObject> objectsToActivateWhenEnterDayTime;

        public void TurnOff()
        {
            foreach (DayNightCycleEntity dayNightCycleEntity in dayNightCycleEntities)
            {
                dayNightCycleEntity.SpriteRenderer.sprite = dayNightCycleEntity.DaySprite;
            }

            ActivateObjects(objectsToActivateWhenEnterDayTime);
            DeactivateObjects(objectsToActivateWhenEnterNightTime);
        }

        public void TurnOn()
        {
            foreach (DayNightCycleEntity dayNightCycleEntity in dayNightCycleEntities)
            {
                dayNightCycleEntity.SpriteRenderer.sprite = dayNightCycleEntity.NightSprite;
            }

            ActivateObjects(objectsToActivateWhenEnterNightTime);
            DeactivateObjects(objectsToActivateWhenEnterDayTime);
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