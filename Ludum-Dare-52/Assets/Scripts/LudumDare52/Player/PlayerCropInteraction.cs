using System;
using System.Collections;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Player
{
    public class PlayerCropInteraction : MonoBehaviour
    {
        [SerializeField] private float minDistance;
        private Vector2? _nearest;
        
        void Start()
        {
            StartCoroutine(GetNearestCropPos());
        }

        private Vector2 PlayerCenterPosWithOffset =>transform.position + Vector3.up * 0.5f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _nearest.HasValue)
            {
                //Todo: Crop auswählen!
                CropManager.Instance.PlantCrop(_nearest.Value, ResourceSystem.Instance.CropsList[0]);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PlayerCenterPosWithOffset, minDistance);
        }

        private readonly WaitForSeconds _waitCache = new WaitForSeconds(0.1f);
        private IEnumerator GetNearestCropPos()
        {
            while (true)
            {
                _nearest = FieldPositionManager.Instance.GetNearestCropPos(PlayerCenterPosWithOffset);
                if (Vector2.Distance(_nearest.Value, PlayerCenterPosWithOffset) > minDistance)
                {
                    _nearest = null;
                }
                yield return _waitCache;
            }
        }
    }
}
