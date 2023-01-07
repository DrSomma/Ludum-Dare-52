using AmazeIt.SpriteAnimator;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody2D rb;

        [SerializeField]
        private CharacterAnimator animator;

        [SerializeField]
        protected float moveSpeed = 5f;

        [SerializeField]
        private bool canMoveAxisY = true;

        [SerializeField]
        private bool canMoveAxisX = true;

        private Vector2 _movement;
        private bool _canMove;

        private void Start()
        {
            _canMove = true;
            GameManager.Instance.OnStateUpdate += OnGameStateUpdate;
        }

        private void OnGameStateUpdate(GameState newState)
        {
            if (newState == GameState.Running)
            {
                _canMove = true;
            }

            if (newState == GameState.Pause)
            {
                _canMove = false;
            }
        }

        private void Update()
        {
            if (canMoveAxisX)
            {
                _movement.x = Input.GetAxisRaw("Horizontal");
            }

            if (canMoveAxisY)
            {
                _movement.y = Input.GetAxisRaw("Vertical");
            }

            _movement = _movement.normalized;
            animator.SetAnimation(_movement);
        }


        private void FixedUpdate()
        {
            if (!_canMove)
            {
                return;
            }

            MovePlayer();
        }

        protected virtual void MovePlayer()
        {
            rb.MovePosition(rb.position + _movement.normalized * (moveSpeed * Time.fixedDeltaTime));
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateUpdate -= OnGameStateUpdate;
        }
    }
}