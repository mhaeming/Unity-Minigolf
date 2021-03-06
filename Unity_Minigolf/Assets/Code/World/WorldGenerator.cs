using System;
using System.Collections.Generic;
using System.Linq;
using Code.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.World
{
    /// <summary>
    ///     A time controlled infinite level generator.
    ///     Automatically creates a smooth transition between start - level - ending.
    ///     Optional the level difficulty can be adjusted over time
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        public static WorldGenerator generator;

        public delegate void ObjectPlaced(Vector3 pos);
        public static event ObjectPlaced PitPlaced;
        public static event ObjectPlaced ObstaclePlaced;

        
        [Header("Game Objects")]
        // Player
        public GameObject player;

        [Header("Generation Settings")]
        // How many blocks should be kept behind the Player
        public int keepFloorElements = 2;

        private static float _activeFloorPositionX;
        private static float _activeFloorPositionY;
        private static float _activeFloorPositionZ;
        
        [HideInInspector] public float _obstacleSize;

        public float ObstacleFreq { get; set; }

        public float PitFreq { get; set; }

        public bool AutoCleanUp { get; set; }

        public List<GameObject> ActiveFloors { get; } = new List<GameObject>();

        public List<GameObject> ActiveObstacles { get; } = new List<GameObject>();

        public List<GameObject> ActivePits { get; } = new List<GameObject>();

        public bool WorldActive { get; set; }
        private void Awake()
        {
            // Destroy any other existing Generators
            if (generator != null && generator != this)
            {
                Destroy(gameObject);
            }
            else
            {
                //DontDestroyOnLoad(gameObject);
                generator = this;
            }

            AutoCleanUp = true;
            WorldActive = true;
        }

        public void OnEnable()
        {
            player = GameObject.FindWithTag("Player");
            // ensure that player is moveable in Main Scene
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            //PlayerBehavior.Reset += Start;
        }

        public void Start()
        {
            PlayerBehavior.PlayerUnFreeze();
            //PlayerBehavior.Reset();
        }

        public void OnDisable()
        {
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                //player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                player.GetComponent<Rigidbody>().useGravity = false;
                Debug.Log("You may rest now.");
                _activeFloorPositionZ = 0;
                _activeFloorPositionX = 0;
                _activeFloorPositionY = 0;
            }
        }

        public void Update()
        {
            if (!AutoCleanUp) return;
            // If there are no objects available from the pool, go through all active Floor objects in the scene and check if you can deactivate some of them
            ClearBehind(ActiveFloors);
            ClearBehind(ActivePits);
            ClearBehind(ActiveObstacles);
        }
        
        // Update is called once per frame
        public void GenerateWorld()
        {
            if (!WorldActive) return;
            
            if (Random.value < PitFreq & !PitNear(2))
            {
                var pit = ObjectPool.sharedInstance.GetPooledObject("Pit");
                if (!pit) return;
                // adjust the future position based on prefab scale
                _activeFloorPositionZ += pit.transform.lossyScale.z;
                PlaceBlock(pit);
                ActivePits.Add(pit);

                // Announce the event that a Pit has been placed
                PitPlaced?.Invoke(pit.transform.position);
            }
            else
            {
                // Retrieve floor objects from pool
                var floor = ObjectPool.sharedInstance.GetRandomPoolObject("Floor");
                if (!floor) return;
                _activeFloorPositionZ += floor.transform.lossyScale.z;
                PlaceBlock(floor);
                ActiveFloors.Add(floor);

                // Randomly decide whether to place an Obstacle
                if (Random.value > ObstacleFreq) return;
                var obstacle = ObjectPool.sharedInstance.GetPooledObject("Obstacle");
                if (!obstacle) return;
                PlaceObstacle(obstacle);
                ActiveObstacles.Add(obstacle);

                // Announce the event that an Obstacle has been placed
                ObstaclePlaced?.Invoke(obstacle.transform.position);
            }
        }


        /// <summary>
        ///     Place floor elements relative to the player position
        /// </summary>
        private void PlaceBlock(GameObject block)
        {
            // Place objects in front of the Player
            block.transform.position = new Vector3(_activeFloorPositionX, _activeFloorPositionY, _activeFloorPositionZ);
            block.SetActive(true);
            
            // height variation still feels a little jaggy
            // _activeFloorPositionY += 0.05f * Random.insideUnitCircle.x;
        }

        private void PlaceObstacle(GameObject obstacle)
        {
            int lane = Random.Range(-1, 2);
            obstacle.transform.position = new Vector3(lane, _activeFloorPositionY + _obstacleSize,
                _activeFloorPositionZ);
            obstacle.SetActive(true);
        }

        private void ClearBehind(IList<GameObject> list)
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i].transform.position.z - player.transform.position.z < -keepFloorElements)
                {
                    list[i].SetActive(false);
                    list.RemoveAt(i);
                }
        }

        protected internal void ClearAll()
        {
            ClearObstacles();
            ClearPits();
            
            foreach (var obj in ActiveFloors)
            {
                obj.SetActive(false);
            }
            ActiveFloors.Clear();
            
            _activeFloorPositionZ = player.transform.position.z - keepFloorElements;
        }

        protected internal void ClearObstacles()
        {
            foreach (var obj in ActiveObstacles)
            {
                obj.SetActive(false);
            }
            ActiveObstacles.Clear();
        }

        protected internal void ClearPits()
        {
            foreach (var obj in ActivePits)
            {
                obj.SetActive(false);
            }
            ActivePits.Clear();
        }

        /// <summary>
        /// Checks whether another pit is close.
        /// This allows indirect control about how close pits can be placed in the world.
        /// </summary>
        /// <param name="threshold"> Distance in which to check for other pits</param>
        /// <returns></returns>
        private bool PitNear(float threshold)
        {
            if (ActivePits.Count > 0)
            {
                return Mathf.Abs(ActivePits.Last().transform.position.z - _activeFloorPositionZ) < threshold;
            }

            return false;
        }
    }
}