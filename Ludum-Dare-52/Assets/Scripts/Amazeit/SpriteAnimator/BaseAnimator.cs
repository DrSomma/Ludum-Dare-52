using System;
using System.Collections;
using UnityEngine;

namespace AmazeIt.SpriteAnimator
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseAnimator : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _renderer;

        private string _currentPlayingId = string.Empty;

        private Coroutine _runningRoutine;

        private void OnDisable()
        {
            Stop();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        protected void PlayLoop(
            string id,
            Sprite[] frames,
            float delayBetweenFrames,
            bool yoyo,
            float delayBetweenLoops = 0f)
        {
            if (_currentPlayingId == id)
            {
                return;
            }

            Stop();
            _currentPlayingId = id;
            _runningRoutine = StartCoroutine(
                Animate(
                    loop: true,
                    yoyo: yoyo,
                    frames: frames,
                    delayBetweenFrames: delayBetweenFrames,
                    delayBetweenLoops: delayBetweenLoops));
        }

        protected void PlayOneTime(
            string id,
            Sprite[] frames,
            float delayBetweenFrames,
            Action callback = null)
        {
            Stop();
            _currentPlayingId = id;
            _runningRoutine = StartCoroutine(
                Animate(
                    loop: false,
                    yoyo: false,
                    frames: frames,
                    delayBetweenFrames: delayBetweenFrames,
                    delayBetweenLoops: 0f,
                    callback: callback));
        }

        private void Stop()
        {
            if (_runningRoutine == null)
            {
                return;
            }

            _currentPlayingId = string.Empty;
            StopCoroutine(_runningRoutine);
        }

        private IEnumerator Animate(
            bool loop,
            bool yoyo,
            Sprite[] frames,
            float delayBetweenFrames,
            float delayBetweenLoops,
            Action callback = null)
        {
            WaitForSeconds delay = new(delayBetweenFrames);
            WaitForSeconds delayLoop = new(delayBetweenLoops);
            while (true)
            {
                for (int index = 0; index < frames.Length; index++)
                {
                    _renderer.sprite = frames[index];
                    yield return delay;
                }

                if (yoyo)
                {
                    yield return delayLoop;
                    for (int index = frames.Length - 1; index >= 0; index--)
                    {
                        _renderer.sprite = frames[index];
                        yield return delay;
                    }
                }

                if (!loop)
                {
                    break;
                }

                yield return delayLoop;
            }

            callback?.Invoke();
        }
    }
}