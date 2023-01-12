using Amazeit.Utilities.Singleton;
using UnityEngine;

namespace LudumDare52.Systems
{
    public class AudioSystem : Singleton<AudioSystem>
    {
        [SerializeField]
        private AudioSource _musicSource;

        [SerializeField]
        private AudioSource _soundsSource;

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void PlaySound(AudioClip clip, float vol = 1)
        {
            _soundsSource.PlayOneShot(clip: clip, volumeScale: vol);
        }

        public void PlaySound(AudioClip clip, float pitch, float vol = 1)
        {
            _soundsSource.pitch = pitch;
            _soundsSource.PlayOneShot(clip: clip, volumeScale: vol);
        }

        public void StopSound()
        {
            _soundsSource.Stop();
        }

        public void PlayCantSound()
        {
            PlaySound(ResourceSystem.Instance.cant, 0.1f);
        }
    }
}