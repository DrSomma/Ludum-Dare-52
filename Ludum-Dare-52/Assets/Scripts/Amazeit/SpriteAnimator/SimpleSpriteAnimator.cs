using System;
using System.Collections.Generic;
using System.Linq;
using Amazeit.SpriteAnimator;
using Amazeit.SpriteAnimator.ScriptableObject;
using UnityEngine;

namespace AmazeIt.SpriteAnimator
{
    public class SimpleSpriteAnimator : BaseAnimator
    {
        [SerializeField] private SpriteAnimation[] _animation;
        private SpriteAnimation _currentAnimation;
        private Dictionary<string, SpriteAnimation> _animationDic;
        [SerializeField] private bool isOverwriteSpeed;
        [SerializeField] private float overwriteSpeed;

        private float Speed => isOverwriteSpeed ? overwriteSpeed : _currentAnimation.DelayBetweenFrames;
        
        protected virtual void Awake()
        {
            _animationDic = _animation.ToDictionary(x => x.Id, x=> x);
        }
        
        private void OnEnable()
        {
            PlayLoop(_animation.FirstOrDefault()!.Id);
        }

        public void PlayLoop(string id)
        {
            _currentAnimation = _animationDic[id];
            PlayLoop(_currentAnimation.Id, _currentAnimation.Frames, Speed,_currentAnimation.Yoyo, _currentAnimation.DelayBetweenLoops);
        }
        
        public void PlayOnetime(string id, Action callback = null)
        {
            _currentAnimation = _animationDic[id];
            PlayOneTime(_currentAnimation.Id, _currentAnimation.Frames, Speed, callback);
        }
        

 
    }
}