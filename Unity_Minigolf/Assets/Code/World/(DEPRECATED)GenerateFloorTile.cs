using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Code.World
{
    public class GenerateFloorTile : MonoBehaviour
    {
        // Player
        public GameObject playerObj;
        // Select elements
        public GameObject floorTile;
        // Obstacle element
        public GameObject obstacle;
        public float obstacleSpawnRate;
        // Pit element
        public GameObject pit;
        public float pitSpawnRate;
        private int lane;
        // flowers
        public GameObject floorTileFlowers;
        public float flowerSpawnRate;
    
        // Storage for all GameObjects
        public Dictionary<int, GameObject> activeFloors = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> activeObstacles = new Dictionary<int, GameObject>();
        public Dictionary<int, GameObject> activePits = new Dictionary<int, GameObject>();
    
        /// Places a floor tile at a given z-Position
        public void PlaceFloorTile(int zPos)
        {
            // with a given chance place the floor tile with flowers
            if (Random.value < flowerSpawnRate)
            {
                Debug.Log("added floor with flowers");
                activeFloors.Add(zPos, Instantiate(floorTileFlowers, transform.position + new Vector3(0, 0, zPos), Quaternion.identity));
            }
            else
            {
                activeFloors.Add(zPos, Instantiate(floorTile, transform.position + new Vector3(0, 0, zPos), Quaternion.identity));
            }
        }

        /// Checks whether a floor tile is generated at the given z-Position and tries to spawn an obstacle there
        public void PlaceObstacles(int zPos)
        {
            if (zPos > 5)
            {
                PlacePit(zPos);
                PlaceObstacle(zPos);
            }
        }

        /// Spawns a obstacle randomly on one of the three lanes at a given z-Position
        void PlaceObstacle(int zPos)
        {
            // an obstacle should not be directly behind, before or at the same position as a pit
            if ((Random.value < obstacleSpawnRate) & !activePits.ContainsKey(zPos) 
                                                   & !activePits.ContainsKey(zPos+1) 
                                                   & !activePits.ContainsKey(zPos-1))
            {
                if (activeObstacles.ContainsKey(zPos+1))
                {
                    lane = (int) activeObstacles[zPos+1].transform.position.x;
                }
                else if (activeObstacles.ContainsKey(zPos-1))
                {
                    lane = (int) activeObstacles[zPos-1].transform.position.x;
                }
                // chose randomly only if there is not an obstacle directly behind or before the current zPos
                else
                {
                    lane = Random.Range(-1, 2);
                }
            
                activeObstacles.Add(zPos, Instantiate(obstacle, transform.position + new Vector3(lane, 0.65f, zPos), Quaternion.identity));
                Debug.Log(String.Format("Placed Obstacle at z: {0} in lane {1}", zPos, lane));
                GameObject.Find("WorldManager").GetComponent<WorldManager>().numberObstacles += 1;
            }
        }

        /// Spawns a pit at given z-Position and removes the underlying floor tile
        void PlacePit(int zPos)
        {
            if ((Random.value < pitSpawnRate) & !activePits.ContainsKey(zPos) 
                                              & !activePits.ContainsKey(zPos+1) 
                                              & !activePits.ContainsKey(zPos-1))
            {
                // Remove the floor tile at the selected position to make room for the pit
                Destroy(activeFloors[zPos]);
                activeFloors.Remove(zPos);
                activePits.Add(zPos, Instantiate(pit, position: transform.position + new Vector3(0, 0, zPos) , Quaternion.identity));
                Debug.Log(("Instantiated pit at position" + zPos));
                GameObject.Find("WorldManager").GetComponent<WorldManager>().numberPits += 1;
            }
        }

    }
}

    