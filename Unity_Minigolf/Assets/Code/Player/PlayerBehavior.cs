using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        private FullSceneManager _sceneManager;
    
        public delegate void PlayerEvent();

        public static event PlayerEvent Reset;
        public static event PlayerEvent HitObstacle;
        public static event PlayerEvent HitPit;
        public static event PlayerEvent NextLevel;
        public static event PlayerEvent Freeze;
        public static event PlayerEvent UnFreeze;

        public static int LevelThreshold { get; set; }
        public static int currentLevel;
        public static int maxLevel;
        
        private void OnEnable()
        {
            _sceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
        }

        private void Start()
        {
            LevelThreshold = 3;
            Reset();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle") & SceneManager.GetActiveScene().name != "TrainingScene")
            {
                if (HitObstacle != null) HitObstacle();
                if (Reset != null) Reset();
            }

            if (other.gameObject.CompareTag("TrainingFinish"))
            {
                Debug.Log("Training stage is finished.");
                _sceneManager.ChangeScene();
            }

            if (other.gameObject.CompareTag("Pit"))
            {
                if (HitPit != null) HitPit();
                if (Reset != null) Reset();
                // GetComponent<AudioSource>().Play();
            }
        }

        public static void AdaptiveDifficulty()
        {
            if (PlayerInfo.AvoidedObstacles + PlayerInfo.AvoidedPits > LevelThreshold)
            {
                LevelThreshold += 3;
                currentLevel += 1;
                // keep track of maximum level reached for experimental data csv
                if (currentLevel > maxLevel)
                {
                    maxLevel = currentLevel;
                }
                Debug.Log("level: " + currentLevel);
                
                // Call the NextLevel event to inform world events and player info
                if (NextLevel != null) NextLevel();
            }
        }

        public static void PlayerFreeze()
        {
            if (Freeze != null) Freeze();
        }

        public static void PlayerUnFreeze()
        {
            if (UnFreeze != null) UnFreeze();
        }
        
    }
}
