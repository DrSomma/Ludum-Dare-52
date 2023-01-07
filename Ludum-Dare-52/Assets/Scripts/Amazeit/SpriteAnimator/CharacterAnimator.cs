using System;
using System.Collections.Generic;
using Amazeit.SpriteAnimator.ScriptableObject;
using UnityEngine;

namespace AmazeIt.SpriteAnimator
{
    public class CharacterAnimator : BaseAnimator
    {
        [SerializeField] private CharacterAnimation characterAnimation;

        private Vector2 _lastDir;
        private Dictionary<string, Sprite[]> _animationWalkDic;
        private Dictionary<string, Sprite[]> _animationIdleDic;


        protected void Awake()
        {
            _animationWalkDic = new Dictionary<string, Sprite[]>
            {
                {"up", characterAnimation.upFrames},
                {"down", characterAnimation.downFrames},
                {"left", characterAnimation.leftFrames},
                {"right", characterAnimation.rightFrames},
                {"up_idle", characterAnimation.upIdleFrames},
                {"down_idle", characterAnimation.downIdleFrames},
                {"left_idle", characterAnimation.leftIdleFrames},
                {"right_idle", characterAnimation.rightIdleFrames}
            };
        }

        private void Start()
        {
            _lastDir = Vector2.down;
            GetAnimationString(Vector2.zero);
        }

        public void SetAnimation(Vector2 movement)
        {
            if (movement == _lastDir) return;
            GetAnimationString(movement);
        }

        private void GetAnimationString(Vector2 movement)
        {
            string animationString;
            if (movement == Vector2.zero)
                animationString = GetMoveDirString(_lastDir) + "_idle";
            else
                animationString = GetMoveDirString(movement);

            PlayLoop(animationString, _animationWalkDic[animationString], characterAnimation.delayBetweenFrames, false);

            _lastDir = movement;
        }

        private static string GetMoveDirString(Vector2 dir)
        {
            if (Math.Abs(dir.y) >= Math.Abs(dir.x))
            {
                if (dir.y > 0)
                    return "up";
                if (dir.y < 0)
                    return "down";
            }
            else
            {
                if (dir.x > 0)
                    return "right";
                if (dir.x < 0)
                    return "left";
            }

            return "down";
        }
    }
}