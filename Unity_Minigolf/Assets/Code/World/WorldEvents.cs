using Code.Player;
using UnityEngine;

namespace Code.World
{
    public class WorldEvents : MonoBehaviour
    {
        public bool timer = true;
        [Min(0.1f)] public double time;

        private void OnEnable()
        {
            PlayerBehavior.Reset += ResetEvent;
        }

        private void OnDisable()
        {
            PlayerBehavior.Reset -= ResetEvent;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetEvent();
            }
            if (time < 0) return;
            // if (time > 9)
            // {
            //     StartEvent();
            // } else
            // {
            //     StandardPlay();
            // }
            StandardPlay();
            
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
            WorldGenerator.generator.PitFreq = 0.01f;
            WorldGenerator.generator.GenerateWorld();
        }
        

        /// <summary>
        /// Reset the player and stage after hitting an obstacle
        /// </summary>
        public static void ResetEvent()
        {
            Debug.Log("Reset game!");
            WorldGenerator.generator.ClearAll();
            // TODO: The reset does not yet work as expected
            WorldGenerator.generator.player.transform.up += Vector3.up;
            WorldGenerator.generator.GenerateWorld();
        }
    }
}