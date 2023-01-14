using System;
using DG.Tweening;
using UnityEngine;

namespace LudumDare52.Entitys.Animations
{
    public class AnimationScale : BaseAnimation
    {
        public override void DoAnimation(Vector2 position, Action callback)
        {
            float endScale = _transform.localScale.x;
            Sequence sq = DOTween.Sequence();
            sq.Append(_transform.DOScale(endValue: 0, duration: 0));
            sq.Append(_transform.DOScale(endValue: endScale, duration: 0.3f).SetEase(Ease.OutBounce));
            sq.OnComplete(() =>
            {
            
                callback?.Invoke();
            });
        }
    }
}