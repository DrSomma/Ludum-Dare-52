using UnityEngine;

namespace Amazeit.SpriteAnimator.ScriptableObject
{
    [CreateAssetMenu(fileName = "New Animation")]
    public class SpriteAnimation : UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite[] frames;
        [SerializeField] private float delayBetweenFrames;
        [SerializeField] private string id;
        [SerializeField] private bool yoyo;
        [SerializeField] private float delayBetweenLoops;
        
        public Sprite[] Frames => frames;
        public float DelayBetweenFrames => delayBetweenFrames;
        public string Id => id;
        public bool Yoyo => yoyo;
        public float DelayBetweenLoops => delayBetweenLoops;
    }
}