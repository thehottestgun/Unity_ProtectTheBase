using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct NamedSound
    {
        public Sound sound;
        public AudioClip clip;
    }
    public class SoundController : MonoBehaviour
    {
        public NamedSound[] sounds;
        public Dictionary<Sound, AudioClip> Sounds = new Dictionary<Sound, AudioClip>();
        private AudioSource _audioSource;
        private void OnEnable()
        {
            PlasmaGun.onShootSound += PlaySound;
            EnemyLogic.onEnemyDeath += PlaySound;
        }
        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            foreach (var sound in sounds) Sounds.Add(sound.sound, sound.clip);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PlaySound(Sound sound)
        {
            _audioSource.PlayOneShot(Sounds[sound]);
        }

    }
}

