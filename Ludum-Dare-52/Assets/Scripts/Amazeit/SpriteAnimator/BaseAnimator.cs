using System;
using System.Collections;
using UnityEngine;

namespace AmazeIt.SpriteAnimator
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BaseAnimator : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _renderer;
        private Coroutine _runningRoutine;
        private string _currentPlayingId = string.Empty;
        
        protected void PlayLoop(string id, Sprite[] frames, float delayBetweenFrames, bool yoyo, float delayBetweenLoops = 0f)
        {
            if (_currentPlayingId == id)
            {
                return;
            }

            Stop();
            _currentPlayingId = id;
            _runningRoutine = StartCoroutine(Animate(true,yoyo, frames, delayBetweenFrames, delayBetweenLoops));
        }

        protected void PlayOneTime(string id,Sprite[] frames, float delayBetweenFrames, Action callback = null)
        {
            Stop();
            _currentPlayingId = id;
            _runningRoutine = StartCoroutine(Animate(false, false,frames, delayBetweenFrames, 0f, callback));
        }

        private void Stop()
        {
            if (_runningRoutine == null)
            {
                return;
            }
            _currentPlayingId = String.Empty;
            StopCoroutine(_runningRoutine);
        }

        private void OnDisable()
        {
            Stop();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        
        IEnumerator Animate(bool loop,bool yoyo,Sprite[] frames,  float delayBetweenFrames,float delayBetweenLoops, Action callback = null)
        {
            WaitForSeconds delay = new WaitForSeconds(delayBetweenFrames);
            WaitForSeconds delayLoop = new WaitForSeconds(delayBetweenLoops);
            while (true)
            {
                for (var index = 0; index < frames.Length; index++)
                {
                    _renderer.sprite = frames[index];
                    yield return delay;
                }

                if (yoyo)
                {
                    yield return delayLoop;
                    for (var index = frames.Length-1; index >= 0; index--)
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