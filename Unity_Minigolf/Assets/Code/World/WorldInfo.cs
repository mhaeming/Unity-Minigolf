using System;
using UnityEngine;

namespace Code.World
{
    public class WorldInfo : MonoBehaviour
    {
        public Vector3 ClosestPitAt { get; private set; }
        public Vector3 ClosestObstacleAt { get; private set; }
        public int TotalPitsPlaced { get; private set; }
        public int TotalObstaclesPlaced { get; private set; }
        
        public delegate void NewCloseObject(Vector3 pos);

        public static event NewCloseObject NewCloseObstacle;
        public static event NewCloseObject NewClosePit;
        
        private void OnEnable()
        {
            WorldGenerator.ObstaclePlaced += OnObstaclePlaced;
            WorldGenerator.PitPlaced += OnPitPlaced;
        }

        private void OnDisable()
        {
            WorldGenerator.ObstaclePlaced -= OnObstaclePlaced;
            WorldGenerator.ObstaclePlaced -= OnPitPlaced;
        }

        private void Start()
        {
            ClosestObstacleAt = Vector3.positiveInfinity;
            ClosestPitAt = Vector3.positiveInfinity;
        }

        private void OnObstaclePlaced(Vector3 pos)
        {
            // Debug.Log("Obstacle at: " + pos + " Closest at: " + ClosestObstacleAt);
            TotalObstaclesPlaced++;
            // if (pos.z < ClosestObstacleAt.z)
            // {
            //     Debug.Log("New close Obstacle");
            //     ClosestObstacleAt = pos;
            //     NewCloseObstacle(ClosestObstacleAt);
            // }
        }

        private void OnPitPlaced(Vector3 pos)
        {
            TotalPitsPlaced++;
            // if (pos.z < ClosestPitAt.z)
            // {
            //     ClosestPitAt = pos;
            //     NewClosePit(ClosestPitAt);
            // }
        }
    }
}
