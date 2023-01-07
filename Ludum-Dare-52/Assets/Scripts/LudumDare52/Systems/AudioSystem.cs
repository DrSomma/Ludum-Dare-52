﻿using Amazeit.Utilities;
using UnityEngine;

namespace LudumDare52.Systems
{
    public class AudioSystem : Singleton<AudioSystem>
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundsSource;

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void PlaySound(AudioClip clip, float vol = 1)
        {
            _soundsSource.PlayOneShot(clip, vol);
        }
        
        public void PlaySound(AudioClip clip, float pitch, float vol = 1)
        {
            _soundsSource.pitch = pitch;
            _soundsSource.PlayOneShot(clip, vol);
        }

        public void StopSound()
        {
            _soundsSource.Stop();
        }
    }
}