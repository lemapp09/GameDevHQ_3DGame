using System.Collections;
using System.Collections.Generic;
using LemApperson_3DGame.Manager;
using UnityEngine;

namespace LemApperson_3DGame.Manager
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        
        [SerializeField] private AudioSource _ambientAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioClip[] _sounds;
        
        // 0 - Ambient      1 -             2 - 
        // 3 -              4 -             5 - 
        
        private void Start() {
            Ambient(0);
        }

        public void Ambient(int ClipNumber) {
            _ambientAudioSource.Stop();
            _ambientAudioSource.clip = _sounds[ClipNumber];
            _ambientAudioSource.Play();
        }

        public void SFX(int ClipNumber) {
            _sfxAudioSource.PlayOneShot(_sounds[ClipNumber]);
        }
    }
}