using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.SpriteAnimator.ScriptableObject;
using UnityEngine;

namespace Amazeit.SpriteAnimator
{
    public class SimpleSpriteAnimator : BaseAnimator
    {
        [SerializeField]
        private SpriteAnimation[] _animation;

        [SerializeField]
        private bool isOverwriteSpeed;

        [SerializeField]
        private float overwriteSpeed;

        private Dictionary<string, SpriteAnimation> _animationDic;
        private SpriteAnimation _currentAnimation;

        private float Speed => isOverwriteSpeed ? overwriteSpeed : _currentAnimation.DelayBetweenFrames;

        protected virtual void Awake()
        {
            _animationDic = _animation.ToDictionary(keySelector: x => x.Id, elementSelector: x => x);
        }

        private void OnEnable()
        {
            PlayLoop(_animation.FirstOrDefault()!.Id);
        }

        public void PlayLoop(string id)
        {
            _currentAnimation = _animationDic[id];
            PlayLoop(
                id: _currentAnimation.Id,
                frames: _currentAnimation.Frames,
                delayBetweenFrames: Speed,
                yoyo: _currentAnimation.Yoyo,
                delayBetweenLoops: _currentAnimation.DelayBetweenLoops);
        }

        public void PlayOnetime(string id, Action callback = null)
        {
            _currentAnimation = _animationDic[id];
            PlayOneTime(id: _currentAnimation.Id, frames: _currentAnimation.Frames, delayBetweenFrames: Speed, callback: callback);
        }
    }
}