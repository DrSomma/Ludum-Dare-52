using System.Collections;
using System.Linq;
using UnityEngine;

namespace LudumDare52.Player
{
    //TODO: maby refactor with PlayerCropInteraction some methods are the same 
    public class PlayerCustomerInteraction : MonoBehaviour
    {
        [SerializeField]
        private float minDistance;

        [SerializeField]
        private LayerMask layerMask;

        private Interactable _nearest;

        private Collider2D[] _results;
        private Vector2 PlayerCenterPosWithOffset => transform.position + Vector3.up * 0.5f;

        private void Start()
        {
            StartCoroutine(GetNearestObject());
        }

        private void Update()
        {
            if (_nearest == null)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _nearest.TriggerLeftClick();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center: PlayerCenterPosWithOffset, radius: minDistance);
        }

        private IEnumerator GetNearestObject()
        {
            _results = new Collider2D[10];
            Interactable newNearst = null;
            while (true)
            {
                int countNear = Physics2D.OverlapCircleNonAlloc(point: PlayerCenterPosWithOffset, radius: minDistance, results: _results, layerMask: layerMask);
                if(countNear > 0)
                {
                    newNearst = _results.Where(x => x != null).OrderBy(x => Vector2.Distance(a: x.transform.position, b: PlayerCenterPosWithOffset)).First().GetComponent<Interactable>();
                    if (_nearest != newNearst)
                    {
                        if (_nearest != null)
                        {
                            _nearest.TriggerPlayerLeft();
                        }
                        _nearest = newNearst;
                        _nearest.TriggerPlayerIsClose();
                    }
                }
                else
                {
                    if (_nearest != null)
                    {
                        _nearest.TriggerPlayerLeft();
                    }
                    _nearest = null;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}