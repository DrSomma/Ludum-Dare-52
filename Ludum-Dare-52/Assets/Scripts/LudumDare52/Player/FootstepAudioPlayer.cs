using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Player
{
    public class FootstepAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] foodsteps;

        private float _deltaDistance;
        private Vector2 _laspos;
        private Vector2 _newpos;

        [SerializeField]
        private float minDis = 0.4f;

        private void Start()
        {
            _laspos = transform.position;
        }

        private void FixedUpdate()
        {
            _newpos = transform.position;
            PlayAudio();
        }

        private void PlayAudio()
        {
            _deltaDistance = Vector2.Distance(a: _laspos, b: _newpos);
            if (!(_deltaDistance > minDis))
            {
                return;
            }

            AudioClip sound = foodsteps[Random.Range(minInclusive: 0, maxExclusive: foodsteps.Length)];
            AudioSystem.Instance.PlaySound(clip: sound, vol: 0.3f);
            _laspos = transform.position;
        }
    }
}