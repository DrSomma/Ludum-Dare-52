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
        [SerializeField] private CharacterAnimator animator;
        [SerializeField] private float moveSpeed = 5f;
        
        
        private BaseWaypointHandler _waypointHandlerSystem;

        private Waypoint _target;
        private Waypoint _cur;
        private Vector2 _movement;
        [SerializeField] private Ease ease;
        private Vector2 _animationVector;

        public Action OnSceenEntered;

        private void Start()
        {
            _waypointHandlerSystem = ResourceSystem.Instance.RandomNpcWaypointSystem;
            StartMoveNpcInScene();
        }

        private void StartMoveNpcInScene()
        {
            GetMoveTween().OnComplete(
                () =>
                {
                    OnSceenEntered?.Invoke();
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
            _cur = _target;
            _target = _waypointHandlerSystem.GetNext();
            _animationVector = Vector2.zero;
            animator.SetAnimation(Vector2.zero); //Idle
            return transform.DOMove(_target.pos, moveSpeed * _target.SpeedFactor).SetSpeedBased().SetDelay(_cur.IdleDelay).SetEase(ease).OnStart(
                () =>
                {
                    if (!isActiveAndEnabled) return;
                    _animationVector = GetAnimationVector();
                    animator.SetAnimation(_animationVector);
                });
        }

        private Vector2 GetAnimationVector()
        {
            Vector2 position = transform.position;
            var newVector = (_target.pos - position).normalized;
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
                animator.SetAnimation(_animationVector);
                transform.DOPlay();
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_target.pos, Vector3.one * 0.2f);
        }
    }
}