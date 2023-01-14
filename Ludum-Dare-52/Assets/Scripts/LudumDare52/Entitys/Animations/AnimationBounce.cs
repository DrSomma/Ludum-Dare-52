using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare52.Entitys.Animations
{
    public class AnimationBounce : BaseAnimation
    {
        [SerializeField]
        private Rigidbody2D rb;

        public override void DoAnimation(Vector2 position, Action callback)
        {
            float endScale = _transform.localScale.x;
            _transform.transform.localScale = Vector3.zero;

            Vector2 endPos = position - new Vector2(x: Random.Range(minInclusive: -1f, maxInclusive: 1f), y: Random.Range(minInclusive: -1f, maxInclusive: 1f));
            Sequence sq = DOTween.Sequence();
            sq.Append(rb.DOJump(endValue: endPos, jumpPower: 1, numJumps: 2, duration: 0.5f));
            sq.Join(transform.DOScale(endValue: endScale, duration: 0.3f));
            sq.OnComplete(() => { callback?.Invoke(); });
        }
    }
}