using UnityEngine;

namespace Code.WorldGeneration
{
    public class WorldEvents : MonoBehaviour
    {
        public bool timer = true;
        [MinAttribute(0.1f)] public double time;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetEvent();
            }
            if (time < 0) return;
            if (time > 9)
            {
                StartEvent();
            } else
            {
                StandardPlay();
            }
            
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
            WorldGenerator.generator.ObstacleFreq = 0;
            WorldGenerator.generator.PitFreq = 0;
            WorldGenerator.generator.GenerateWorld();
        }

        private void StandardPlay()
        {
            WorldGenerator.generator.ObstacleFreq = 0.1f;
            WorldGenerator.generator.PitFreq = 0.005f;
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
            Debug.Log("Reset game!");
            WorldGenerator.generator.ClearAll();
        }
    }
}