using System;
using System.Collections;
using System.Collections.Generic;
using AmazeIt.SpriteAnimator;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudumDare52;
using LudumDare52.Npc.Movement.Waypoints;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterAnimator animator;
    [SerializeField]
    private Ease ease = Ease.InSine;

    [SerializeField]
    private RandomWaypointHandler waypoins;
    [SerializeField]
    private Interactable interactable;
    [SerializeField]
    private float moveSpeed = 1f;

    private Vector2 _animation;
    private Waypoint _currentWaypoint;
    private Vector2 _movement;
    private Waypoint _target;

    private void Start()
    {
        StartMovementLoop();
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
        _target = waypoins.GetNext();
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
}
