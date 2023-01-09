using AmazeIt.SpriteAnimator;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudumDare52.Npc.Movement.Waypoints;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
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

        [SerializeField]
        private Interactable interactable;

        private Vector2 _animation;
        private Waypoint _currentWaypoint;
        private Vector2 _movement;

        private Waypoint _spawnPoint;

        private Waypoint _target;

        private BaseWaypointHandler _waypointHandlerSystem;

        private void Start()
        {
            _waypointHandlerSystem = ResourceSystem.Instance.RandomNpcWaypointSystem;
            _spawnPoint = new Waypoint();
            _spawnPoint.pos = transform.position;
            StartMoveNpcInScene();
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateUpdate -= OnStateUpdate;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(center: _target.pos, size: Vector3.one * 0.2f);
        }

        private void OnStateUpdate(GameState state)
        {
            switch (state)
            {
                case GameState.DayEnd:
                    SendCustomerHome();
                    break;
                case GameState.Running:
                    SetPause(false);
                    break;
                case GameState.Pause:
                    SetPause(true);
                    break;
                case GameState.Init: break;
                case GameState.GameOver:
                    SendCustomerHome();
                    break;
            }
        }


        public void SendCustomerHome()
        {
            interactable.SetCanInteract(false);
            transform.DOKill();
            transform.DOMove(endValue: _spawnPoint.pos, duration: moveSpeed * 2).SetSpeedBased().OnStart(
                () =>
                {
                    _target = _spawnPoint;
                    _animation = GetAnimationVector();
                    animator.SetAnimation(_animation);
                }).OnComplete(() => { Destroy(gameObject); });
        }

        private void StartMoveNpcInScene()
        {
            GetMoveTween().OnComplete(
                () =>
                {
                    interactable.SetCanInteract(true);
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

        private void SetPause(bool pause)
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