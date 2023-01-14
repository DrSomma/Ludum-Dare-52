using System;
using System.Collections;
using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Entitys.Interactable;
using UnityEngine;

namespace LudumDare52.Player
{
    public class PlayerInteraction : Singleton<PlayerInteraction>
    {
        [SerializeField]
        private float minDistance;

        [SerializeField]
        private LayerMask layerMask;

        private Interactable _nearest;

        private Collider2D[] _results;

        public Action<Interactable> OnNearestChange;
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
            else if (Input.GetMouseButtonDown(1))
            {
                _nearest.TriggerRightClick();
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
                if (countNear > 0)
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
                        OnNearestChange?.Invoke(newNearst);
                    }
                }
                else
                {
                    if (_nearest != null)
                    {
                        _nearest.TriggerPlayerLeft();
                        OnNearestChange?.Invoke(null);
                    }

                    _nearest = null;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}