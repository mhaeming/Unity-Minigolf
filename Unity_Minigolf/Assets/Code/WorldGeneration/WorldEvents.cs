using System;
using UnityEngine;

namespace Code.WorldGeneration
{
    public class WorldEvents : MonoBehaviour
    {

        public double time;

        private void Start()
        {
            WorldGenerator.generator.AutoCleanUp = true;
        }

        private void Update()
        {
            if (!(time > 0)) return;
            if (time < 5)
            {
                StandardPlay();
            }
            StartEvent();

        }

        private void FixedUpdate()
        {
            time -= Time.deltaTime;
        }


        /// <summary>
        /// Generate a series of base blocks to ease the player into the level
        /// </summary>
        private void StartEvent()
        {
            WorldGenerator.generator.GenerateWorld();
        }

        private void StandardPlay()
        {
            WorldGenerator.generator.ObstacleFreq = 0.5f;
            WorldGenerator.generator.PitFreq = 0.5f;
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
        public void ResetEvent()
        {
            WorldGenerator.generator.ClearAll();
            // StartEvent();
        }
    }
}