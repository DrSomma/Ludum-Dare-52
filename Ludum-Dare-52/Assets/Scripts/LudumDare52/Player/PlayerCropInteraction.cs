using System.Collections;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Player
{
    public class PlayerCropInteraction : MonoBehaviour
    {
        [SerializeField]
        private float minDistance;

        private readonly WaitForSeconds _waitCache = new(0.1f);
        private Vector2? _nearest;

        private Vector2 PlayerCenterPosWithOffset => transform.position + Vector3.up * 0.5f;

        private void Start()
        {
            StartCoroutine(GetNearestCropPos());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _nearest.HasValue)
            {
                Vector2 nearestWithOffset = _nearest.Value + Vector2.up * 0.3f;
                //Todo: Crop auswählen!

                if (CropManager.Instance.CanPlantOnPos(nearestWithOffset))
                {
                    CropManager.Instance.PlantCrop(pos: nearestWithOffset, crop: ResourceSystem.Instance.CropsList[0]);
                }
                else if (CropManager.Instance.CanHarvestOnPos(nearestWithOffset))
                {
                    CropManager.Instance.HarvestCrop(nearestWithOffset);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center: PlayerCenterPosWithOffset, radius: minDistance);
        }

        private IEnumerator GetNearestCropPos()
        {
            while (true)
            {
                _nearest = FieldPositionManager.Instance.GetNearestCropPos(PlayerCenterPosWithOffset);
                if (Vector2.Distance(a: _nearest.Value, b: PlayerCenterPosWithOffset) > minDistance)
                {
                    _nearest = null;
                }

                yield return _waitCache;
            }
        }
    }
}