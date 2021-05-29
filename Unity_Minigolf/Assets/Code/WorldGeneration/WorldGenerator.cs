    using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A time controlled infinite level generator.
/// Automatically creates a smooth transition between start - level - ending.
/// Optional the level difficulty can be adjusted over time
/// </summary>
public class WorldGenerator : MonoBehaviour
{
    [Header("Game Objects")]
    // Player
    public GameObject playerobj;
    
    [Header("Generation Settings")]
    // How many blocks should be kept behind the Player
    public int keepFloorElements = 2;

    private float pitFreq;
    private float obstacleFreq;

    private float _activeFloorPositionZ;
    private float _activeFloorPositionY;
    private float _activeFloorPositionX;

    private List<GameObject> activeFloors = new List<GameObject>();
    private List<GameObject> activePits = new List<GameObject>();
    private List<GameObject> activeObstacles = new List<GameObject>();


    public void Update()
    {
        GenerateWorld();
        // If there are no objects available from the pool, go through all active Floor objects in the scene and check if you can deactivate some of them
        ClearBehind(activeFloors);
        ClearBehind(activePits);
        ClearBehind(activeObstacles);
    }
    
    // Update is called once per frame
    public void GenerateWorld()
    {
        if (Random.value < pitFreq)
        {
            GameObject pit = ObjectPool.sharedInstance.GetPooledObject("Pit");
            if (pit)
            {
                PlaceBlock(pit);
                activePits.Add(pit);
            }
        }
        else
        {
            // Retrieve floor objects from pool
            GameObject floor = ObjectPool.sharedInstance.GetRandomPoolObject("Floor");
            if (floor)
            {
                PlaceBlock(floor);
                activeFloors.Add(floor);
                if (Random.value < obstacleFreq)
                {
                    GameObject obstacle = ObjectPool.sharedInstance.GetPooledObject("Obstacle");
                    if (obstacle)
                    {
                        PlaceObstacle(obstacle);
                        activeObstacles.Add(obstacle);
                    }
                }
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
    
    protected void ClearBehind(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if ((list[i].transform.position.z - playerobj.transform.position.z) < -keepFloorElements)
            {
                list[i].SetActive(false);
            }
        }
    }

    protected void ClearAll()
    {
        for (int i = 0; i < activeFloors.Count; i++)
        {
            activeFloors[i].SetActive(false);
            activeFloors.RemoveAt(i);
        }

        for (int i = 0; i < activeObstacles.Count; i++)
        {
            activeObstacles[i].SetActive(false);
            activeObstacles.RemoveAt(i);
        }
        
        for (int i = 0; i < activePits.Count; i++)
        {
            activePits[i].SetActive(false);
            activePits.RemoveAt(i);
        }
    }
    
    

    public float GetObstacleFreq()
    {
        return obstacleFreq;
    }
    
    public void SetObstacleFreq(float freq)
    {
        obstacleFreq = freq;
    }

    public float GetPitFreq()
    {
        return pitFreq;
    }

    public void SetPitFreq(float freq)
    {
        pitFreq = freq;
    }

    public List<GameObject> GetActiveFloors()
    {
        return activeFloors;
    }
    
    public List<GameObject> GetActiveObstacles()
    {
        return activeObstacles;
    }
    public List<GameObject> GetActivePits()
    {
        return activePits;
    }
}
