using UnityEngine;

namespace Amazeit.SpriteAnimator.ScriptableObject
{
    [CreateAssetMenu(fileName = "New Character Animation")]
    public class CharacterAnimation : UnityEngine.ScriptableObject
    {
        [SerializeField]
        public float delayBetweenFrames;

        [Header("Walk")]
        [SerializeField]
        public Sprite[] upFrames;

        [SerializeField]
        public Sprite[] downFrames;

        [SerializeField]
        public Sprite[] leftFrames;

        [SerializeField]
        public Sprite[] rightFrames;

        [Header("Idle")]
        [SerializeField]
        public Sprite[] upIdleFrames;

        [SerializeField]
        public Sprite[] downIdleFrames;

        [SerializeField]
        public Sprite[] leftIdleFrames;

        [SerializeField]
        public Sprite[] rightIdleFrames;
    }
}