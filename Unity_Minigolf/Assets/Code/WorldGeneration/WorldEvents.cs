using UnityEngine;

namespace Code.WorldGeneration
{
    public class WorldEvents : MonoBehaviour
    {
        public bool timer = true;
        [MinAttribute(0.1f)] public double time;

        private void Update()
        {
            if (time < 0) return;
            if (time < 5)
            {
                StandardPlay();
            }

            StartEvent();
        }

        private void FixedUpdate()
        {
            if (timer)
            {
                time -= Time.deltaTime;
            }
        }


        /// <summary>
        /// Generate a series of base blocks to ease the player into the level
        /// </summary>
        private static void StartEvent()
        {
            WorldGenerator.generator.GenerateWorld();
        }

        private static void StandardPlay()
        {
            WorldGenerator.generator.ObstacleFreq = 0.1f;
            WorldGenerator.generator.PitFreq = 0.1f;
            WorldGenerator.generator.GenerateWorld();
        }


        /// <summary>
        /// Transition into base blocks with an ending sequence
        /// </summary>
        public void EndEvent()
        {
        }


        /// <summary>
        /// Reset the player and stage after hitting an obstacle
        /// </summary>
        public static void ResetEvent()
        {
            WorldGenerator.generator.ClearAll();
            // StartEvent();
        }
    }
}