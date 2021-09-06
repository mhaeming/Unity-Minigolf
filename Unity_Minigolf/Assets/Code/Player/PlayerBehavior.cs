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

        private void OnEnable()
        {
            _sceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
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
            if (PlayerInfo.AvoidedObstacles + PlayerInfo.AvoidedPits > PlayerInfo.LevelThreshold)
            {
                if (NextLevel != null) NextLevel();
            }
        }
        
    }
}
