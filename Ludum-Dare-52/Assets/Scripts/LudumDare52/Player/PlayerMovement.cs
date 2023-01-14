using Amazeit.SpriteAnimator;
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

        private bool _canMove;

        private Vector2 _movement;

        private void Start()
        {
            _canMove = true;
            GameManager.Instance.OnStateUpdate += OnGameStateUpdate;
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

        private void OnGameStateUpdate(GameState newState)
        {
            _canMove = newState switch
                       {
                           GameState.Running => true,
                           GameState.Pause or GameState.DayEnd or GameState.GameOver => false,
                           _ => _canMove
                       };
        }

        protected virtual void MovePlayer()
        {
            rb.MovePosition(rb.position + _movement.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}