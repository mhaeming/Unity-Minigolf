using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Player
{
    public class PlayerBehavior : MonoBehaviour
    {
    
        public delegate void PlayerEvent();

        public static event PlayerEvent Reset;
        public static event PlayerEvent HitObstacle;
        public static event PlayerEvent HitPit;

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
                SceneManager.LoadScene("Priming");
            }

            if (other.gameObject.CompareTag("Pit"))
            {
                if (HitPit != null) HitPit();
                if (Reset != null) Reset();
                // GetComponent<AudioSource>().Play();
            }
        }
    }
}
