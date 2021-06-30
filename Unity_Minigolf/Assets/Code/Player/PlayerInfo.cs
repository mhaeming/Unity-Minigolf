using System;
using Code.World;
using UnityEngine;

namespace Code.Player
{
    public class PlayerInfo : MonoBehaviour
    {

        public static PlayerInfo info;
        
        public int HitObstacles { get; private set; }
        public int HitPits { get; private set; }
        
        public Vector3 ClosestObstacleAt { get; private set; }
        public Vector3 ClosestPitAt { get; private set; }
        public float DistanceToNextObstacle { get; private set; }
        public float DistanceToNextPit { get; private set; }
        public float DistanceToNextObject { get; private set; }

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
            WorldGenerator.ObstaclePlaced += OnNewCloseObstacle;
            WorldGenerator.PitPlaced += OnNewClosePit;
        }

        private void OnDisable()
        {
            PlayerBehavior.HitObstacle -= OnObstacleHit;
            PlayerBehavior.HitPit -= OnPitHit;
            WorldInfo.NewCloseObstacle -= OnNewCloseObstacle;
            WorldInfo.NewClosePit -= OnNewClosePit;
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

        private void OnNewCloseObstacle(Vector3 pos)
        {
            float dist = Vector3.Distance(pos, transform.position);
            
            if (DistanceToNextObstacle < DistanceToNextObject)
            {
                DistanceToNextObject = DistanceToNextObstacle;
                Debug.Log("Closest Object(Obstacle) at: " + DistanceToNextObject);
            }
        }
        
        private void OnNewClosePit(Vector3 pos)
        {
            DistanceToNextPit = Vector3.Distance(pos, transform.position);
            if (DistanceToNextPit < DistanceToNextObject)
            {
                DistanceToNextObject = DistanceToNextPit;
                Debug.Log("Closest Object(Pit) at:" + DistanceToNextObject);

            }
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
