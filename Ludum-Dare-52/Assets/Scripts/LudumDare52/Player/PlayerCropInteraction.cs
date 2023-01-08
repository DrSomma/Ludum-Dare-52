using System.Collections;
using LudumDare52.Crops;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager.PositionManager;
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
            if (!_nearest.HasValue)
            {
                return;
            }

            //Todo: Crop auswählen!

            if (Input.GetMouseButtonDown(0) && CropManager.Instance.CanPlantOnPos(_nearest.Value))
            {
                CropManager.Instance.PlantCrop(pos: _nearest.Value, crop: ResourceSystem.Instance.CropsList[0]);
            }
            else if (Input.GetMouseButtonDown(1) && CropManager.Instance.CanHarvestOnPos(_nearest.Value))
            {
                CropManager.Instance.HarvestCrop(_nearest.Value);
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