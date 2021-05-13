using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

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

    [Header("Time Settings")]
    // Time
    public float timeLimit = 60;
    // Fade-In-Time
    public float fadeInTime = 5;
    // Fade-Out-Time 
    public float fadeOutTime = 5;

    [Header("Generation Settings")]
    // How many blocks should be kept behind the Player
    public int keepFloorElements = 2;

    public float obstacleFreq;

    private int _activeFloorPositions;

    
    // Update is called once per frame
    void Update()
    {
        // Retrieve floor objects from pool
        GameObject floor = ObjectPool.sharedInstance.GetPooledObject("Floor");
        if (floor != null)
        {
            // Place objects in front of the Player
            floor.transform.position = new Vector3(0, 0, _activeFloorPositions);
            floor.SetActive(true);
            _activeFloorPositions++;
        }
        else
        {
            // If there are no objects available from the pool, go through all active Floor objects in the scene and check if you can deactivate some of them
            GameObject[] activeFloors = GameObject.FindGameObjectsWithTag("Floor");
            foreach (var element in activeFloors)
            {
                if ((element.transform.position.z - playerobj.transform.position.z) < -2)
                {
                    // Return an element to the pool
                    element.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Place floor elements relative to the player position
    /// </summary>
    public void placeBlocks()
    {
        
    }

    /// <summary>
    /// Generate a series of base blocks to ease the player into the level
    /// </summary>
    public void startEvent()
    {
        
    }

    
    /// <summary>
    /// Transition into base blocks with an ending sequence
    /// </summary>
    public void endEvent()
    {
        
    }
}
