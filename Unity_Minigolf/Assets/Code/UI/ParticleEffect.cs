using System;
using Code.Player;
using UnityEngine;

namespace Code.UI
{
    public class ParticleEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        
        private void OnEnable()
        {
            PlayerBehavior.NextLevel += OnLevelUp;
        }

        private void OnDisable()
        {
            PlayerBehavior.NextLevel -= OnLevelUp;
        }

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnLevelUp()
        {
            _particleSystem.Play();
        }
    }
}
