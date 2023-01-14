using System;
using UnityEngine;

namespace LudumDare52.Entitys.Animations
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        protected Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public abstract void DoAnimation(Vector2 position, Action callback);
    }
}