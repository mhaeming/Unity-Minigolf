using System;
using UnityEngine;

namespace Code.Player
{
    public class PlayerInfo : MonoBehaviour
    {

        public static PlayerInfo info;

        public int HitObstacles { get; private set; }
        public int HitPits { get; private set; }
        
        private void Awake()
        {
            // Destroy any other existing Player Info
            // if (info != null && info != this){
            //
            //     Destroy(gameObject);
            // }
            // else{
            //     DontDestroyOnLoad(gameObject);
            //     info = this;
            // }
        }

        private void OnEnable()
        {
            PlayerBehavior.HitObstacle += OnObstacleHit;
            PlayerBehavior.HitPit += OnPitHit;
        }

        private void OnDisable()
        {
            PlayerBehavior.HitObstacle -= OnObstacleHit;
            PlayerBehavior.HitPit -= OnPitHit;
        }

        private void OnObstacleHit()
        {
            HitObstacles++;
            Debug.Log("Obstacles hit: " + HitObstacles);
        }

        private void OnPitHit()
        {
            HitPits++;
            Debug.Log("Pits hit: " + HitPits);
        }
        
        // returns in which lane the player is at that moment (left=1, middle=2, right=3)
        public int GetLane()
        {
            if (transform.position.x > 0.5f )
            {
                return 3;
            }
            if (transform.position.x < -0.5f)
            {
                return 1;
            }

            return 2;
        }
    }
}
