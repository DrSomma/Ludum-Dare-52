using System;
using AmazeIt.SpriteAnimator;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudumDare52.Npc.Movement.Waypoints;
using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Npc.Movement
{
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimator animator;

        [SerializeField]
        private float moveSpeed = 1f;

        [SerializeField]
        private Ease ease;

        private Vector2 _animation;
        private Waypoint _currentWaypoint;
        private Vector2 _movement;

        private Waypoint _target;

        private BaseWaypointHandler _waypointHandlerSystem;

        private Action onSceenEntered;

        private void Start()
        {
            _waypointHandlerSystem = ResourceSystem.Instance.RandomNpcWaypointSystem;
            StartMoveNpcInScene();
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(center: _target.pos, size: Vector3.one * 0.2f);
        }

        private void StartMoveNpcInScene()
        {
            GetMoveTween().OnComplete(
                () =>
                {
                    onSceenEntered?.Invoke();
                    StartMovementLoop();
                });
        }

        private void StartMovementLoop()
        {
            MoveLoop();
        }

        private void MoveLoop()
        {
            GetMoveTween().OnComplete(MoveLoop).Play();
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> GetMoveTween()
        {
            _currentWaypoint = _target;
            _target = _waypointHandlerSystem.GetNext();
            _animation = Vector2.zero;
            SetIdleAnimation();
            return transform.DOMove(endValue: _target.pos, duration: moveSpeed * _target.SpeedFactor).SetSpeedBased().SetDelay(_currentWaypoint.IdleDelay).SetEase(ease).OnStart(
                () =>
                {
                    if (!isActiveAndEnabled)
                    {
                        return;
                    }

                    _animation = GetAnimationVector();
                    animator.SetAnimation(_animation);
                });
        }

        private void SetIdleAnimation()
        {
            animator.SetAnimation(Vector2.zero);
        }

        private Vector2 GetAnimationVector()
        {
            Vector2 position = transform.position;
            Vector2 newVector = (_target.pos - position).normalized;
            return newVector;
        }

        public void SetPause(bool pause)
        {
            if (pause)
            {
                animator.SetAnimation(Vector2.zero);
                transform.DOPause();
            }
            else
            {
                animator.SetAnimation(_animation);
                transform.DOPlay();
            }
        }
    }
}