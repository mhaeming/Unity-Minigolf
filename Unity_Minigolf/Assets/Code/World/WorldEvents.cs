using System;
using System.Collections;
using Code.Player;
using UnityEngine;

namespace Code.World
{
    public class WorldEvents : MonoBehaviour
    {
        public float resetCooldown;
        private Timer _timer;
        
        private void OnEnable()
        {
            PlayerBehavior.Reset += ResetEvent;
            PlayerBehavior.NextLevel += OnNextLevel;
        }

        private void OnDisable()
        {
            PlayerBehavior.Reset -= ResetEvent;
            PlayerBehavior.NextLevel -= OnNextLevel;
        }

        private void Start()
        {
            
            // World events use the general timer provided as a script on the camera
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            _timer = Camera.main.GetComponent<Timer>();
            
            // In the beginning no obstacles should be spawned
            WorldGenerator.generator.ObstacleFreq = 0;
            WorldGenerator.generator.PitFreq = 0;
            
            StartCoroutine(nameof(ResetCooldown));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetEvent();
            }
            
            if (_timer.remainingTime > 0)
            {
                WorldGenerator.generator.GenerateWorld();
                PlayerBehavior.AdaptiveDifficulty();
            }

            // if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback |
            //     FullSceneManager.CurrentScene ==  FullSceneManager.sceneEnum.End)
            // {
            //     PlayerBehavior.PlayerFreeze();
            // }
            // else if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Play)
            // {
            //     PlayerBehavior.PlayerUnFreeze();
            // }
        }
        

        private void StandardPlay()
        {
            WorldGenerator.generator.ObstacleFreq = 0.1f;
            WorldGenerator.generator.PitFreq = 0.005f;
            // WorldGenerator.generator.GenerateWorld();
        }
        
        
        /// <summary>
        /// Reset the player and stage after hitting an obstacle
        /// </summary>
        private void ResetEvent()
        {
            Debug.Log("Reset game!");
            WorldGenerator.generator.ClearAll();
            WorldGenerator.generator.ObstacleFreq = 0;
            WorldGenerator.generator.PitFreq = 0;
            PlayerBehavior.LevelThreshold = 3;
            PlayerInfo.AvoidedObstacles = 0;
            PlayerInfo.AvoidedPits = 0;
            PlayerBehavior.currentLevel = 0;
            Debug.Log("level: " + PlayerBehavior.currentLevel);

            StartCoroutine(nameof(ResetCooldown));
            // TODO: The reset does not yet work as expected
        }

        private IEnumerator ResetCooldown()
        {
            yield return new WaitForSecondsRealtime(resetCooldown);
            StandardPlay();
            Debug.Log("Returning to Standard Game Mode");
        }

        private void OnNextLevel()
        {
            WorldGenerator.generator.ObstacleFreq += 0.1f;
            WorldGenerator.generator.PitFreq += 0.005f;
        }
    }
}