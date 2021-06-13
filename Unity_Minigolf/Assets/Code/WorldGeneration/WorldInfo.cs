using UnityEngine;

namespace Code.WorldGeneration
{
    public class WorldInfo : MonoBehaviour
    {
        public Vector3 ClosestPitAt { get; private set; }
        public Vector3 ClosestObstacleAt { get; private set; }
        public int TotalPitsPlaced { get; private set; }
        public int TotalObstaclesPlaced { get; private set; }
        

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
            TotalObstaclesPlaced++;
            if (pos.z < ClosestObstacleAt.z)
            {
                ClosestObstacleAt = pos;
            }
        }

        private void OnPitPlaced(Vector3 pos)
        {
            TotalPitsPlaced++;
            if (pos.z < ClosestPitAt.z)
            {
                ClosestPitAt = pos;
            }
        }
    }
}
