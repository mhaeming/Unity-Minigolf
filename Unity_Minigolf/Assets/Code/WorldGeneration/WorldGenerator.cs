using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.WorldGeneration
{
    /// <summary>
    /// A time controlled infinite level generator.
    /// Automatically creates a smooth transition between start - level - ending.
    /// Optional the level difficulty can be adjusted over time
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        public static WorldGenerator generator;
    
        [Header("Game Objects")]
        // Player
        public GameObject playerobj;
    
        [Header("Generation Settings")]
        // How many blocks should be kept behind the Player
        public int keepFloorElements = 2;

        private float _activeFloorPositionZ;
        private float _activeFloorPositionY;
        private float _activeFloorPositionX;
        
        public delegate void ObjectPlaced(Vector3 pos);
        public static event ObjectPlaced PitPlaced;
        public static event ObjectPlaced ObstaclePlaced;

        
        public float ObstacleFreq { get; set; }

        public float PitFreq { get; set; }

        public bool AutoCleanUp { get; set; }

        public List<GameObject> ActiveFloors { get; } = new List<GameObject>();

        public List<GameObject> ActiveObstacles { get; } = new List<GameObject>();

        public List<GameObject> ActivePits { get; } = new List<GameObject>();

        private void Awake()
        {
            // Destroy any other existing Generators
            if (generator != null && generator != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                generator = this;
            }

            AutoCleanUp = true;
        }

        public void Update()
        {
            if (AutoCleanUp)
            {
                // If there are no objects available from the pool, go through all active Floor objects in the scene and check if you can deactivate some of them
                ClearBehind(ActiveFloors);
                ClearBehind(ActivePits);
                ClearBehind(ActiveObstacles);
            }
        }
    
        // Update is called once per frame
        public void GenerateWorld()
        {
            if (Random.value < PitFreq)
            {
                GameObject pit = ObjectPool.sharedInstance.GetPooledObject("Pit");
                if (!pit) return;
                PlaceBlock(pit);
                ActivePits.Add(pit);

                // Announce the event that a Pit has been placed
                if (PitPlaced != null)
                {
                    PitPlaced(pit.transform.position);
                }
            }
            else
            {
                // Retrieve floor objects from pool
                GameObject floor = ObjectPool.sharedInstance.GetRandomPoolObject("Floor");
                if (!floor) return;
                PlaceBlock(floor);
                ActiveFloors.Add(floor);
                
                // Randomly decide whether to place an Obstacle
                if (Random.value > ObstacleFreq) return;
                GameObject obstacle = ObjectPool.sharedInstance.GetPooledObject("Obstacle");
                PlaceObstacle(obstacle);
                ActiveObstacles.Add(obstacle);

                // Announce the event that an Obstacle has been placed
                if (ObstaclePlaced != null)
                {
                    ObstaclePlaced(obstacle.transform.position);
                }
            }
        }
    
    
        /// <summary>
        /// Place floor elements relative to the player position
        /// </summary>
        private void PlaceBlock(GameObject block)
        {
            // Place objects in front of the Player
            block.transform.position = new Vector3(_activeFloorPositionX, _activeFloorPositionY, _activeFloorPositionZ);
            block.SetActive(true);
            
            // adjust the future position based on prefab scale
            _activeFloorPositionZ += block.transform.lossyScale.z;
            
            // height variation still feels a little jaggy
            // _activeFloorPositionY += 0.05f * Random.insideUnitCircle.x;
            
            // Not sure x variation should be realized yet
            // _activeFloorPositionX;
        }

        private void PlaceObstacle(GameObject obstacle)
        {
            int lane = (int)((2 * Random.value - 1) + _activeFloorPositionX);
            obstacle.transform.position = new Vector3(lane, _activeFloorPositionY + obstacle.transform.lossyScale.y, _activeFloorPositionZ);
            obstacle.SetActive(true);
        }

        private void ClearBehind(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].transform.position.z - playerobj.transform.position.z) < -keepFloorElements)
                {
                    list[i].SetActive(false);
                    list.RemoveAt(i);
                }
            }
        }

        protected internal void ClearAll()
        {
            for (int i = 0; i < ActiveFloors.Count; i++)
            {
                ActiveFloors[i].SetActive(false);
                ActiveFloors.RemoveAt(i);
            }

            for (int i = 0; i < ActiveObstacles.Count; i++)
            {
                ActiveObstacles[i].SetActive(false);
                ActiveObstacles.RemoveAt(i);
            }
        
            for (int i = 0; i < ActivePits.Count; i++)
            {
                ActivePits[i].SetActive(false);
                ActivePits.RemoveAt(i);
            }
        }
    }
}
