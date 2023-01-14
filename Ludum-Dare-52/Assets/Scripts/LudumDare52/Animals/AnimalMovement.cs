using System.Threading.Tasks;
using Amazeit.SpriteAnimator;
using DG.Tweening;
using LudumDare52.Npc.Movement.Waypoints;
using UnityEngine;

namespace LudumDare52.Animals
{
    public class AnimalMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimator animator;

        [SerializeField]
        private Ease ease = Ease.InSine;

        [SerializeField]
        private RandomWaypointHandler waypoins;

        [SerializeField]
        private float moveSpeed = 1f;

        private Waypoint _currentWaypoint;
        private Vector2 _movement;
        private Waypoint _target;

        public async Task MoveToNextWaypoint()
        {
            _currentWaypoint = _target;
            _target = waypoins.GetNext();
            await MoveToPosition(delay: _target.IdleDelay, pos: _target.pos, speedFactor: _target.SpeedFactor);
        }

        public async Task MoveToPosition(Vector2 postion)
        {
            await MoveToPosition(delay: 0, pos: postion, speedFactor: 2f);
        }

        private Task MoveToPosition(float delay, Vector2 pos, float speedFactor)
        {
            if (delay > 0)
            {
                SetIdleAnimation();
            }

            return transform.DOMove(endValue: pos, duration: moveSpeed * speedFactor).SetSpeedBased().SetEase(ease).SetDelay(delay).OnStart(
                () =>
                {
                    if (!isActiveAndEnabled)
                    {
                        return;
                    }

                    animator.SetAnimation(GetAnimationVector(pos));
                }).AsyncWaitForCompletion();
        }

        private void SetIdleAnimation()
        {
            animator.SetAnimation(Vector2.zero);
        }

        private Vector2 GetAnimationVector(Vector2 targetPos)
        {
            Vector2 position = transform.position;
            Vector2 newVector = (targetPos - position).normalized;
            return newVector;
        }

        public void StopMovement()
        {
            transform.DOKill();
        }
    }
}