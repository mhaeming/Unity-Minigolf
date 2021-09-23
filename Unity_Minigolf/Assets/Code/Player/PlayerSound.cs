using System;
using UnityEngine;

namespace Code.Player
{
    public class PlayerSound : MonoBehaviour
    {
        private AudioSource _playerAudio;
        public AudioClip pitSound;
        public AudioClip obstacleSound;

        public AudioClip levelUpSound;

        private void OnEnable()
        {
            PlayerBehavior.NextLevel += OnNextLevel;
        }

        private void OnDisable()
        {
            PlayerBehavior.NextLevel -= OnNextLevel;
        }

        private void Start()
        {
            _playerAudio = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Pit"))
            {
                _playerAudio.PlayOneShot(pitSound, 0.75f);
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                _playerAudio.PlayOneShot(obstacleSound, 0.75f);
            }
        }

        private void OnNextLevel()
        {
            _playerAudio.PlayOneShot(levelUpSound, 0.75f);
        }
    }
}