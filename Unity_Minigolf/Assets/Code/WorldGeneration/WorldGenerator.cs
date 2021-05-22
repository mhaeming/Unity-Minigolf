using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;
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

    [Header("Time Settings")]
    // Time
    public float timeLimit = 60;
    // Fade-In-Time
    [HideInInspector]
    public float fadeInTime = 5;
    // Fade-Out-Time 
    [HideInInspector]
    public float fadeOutTime = 5;

    [Header("Generation Settings")]
    // How many blocks should be kept behind the Player
    public int keepFloorElements = 2;

    public float obstacleFreq;

    private float _activeFloorPositionZ;
    private float _activeFloorPositionY;
    private float _activeFloorPositionX;

    private GameObject[] activeFloors;

    
    // Update is called once per frame
    void Update()
    {
        // Retrieve floor objects from pool
        GameObject floor = ObjectPool.sharedInstance.GetRandomPoolObject("Floor");
        if (floor != null && timeLimit > 0)
        {
            placeBlock(floor);
        }
        // Clean check whether to clean up unused floor tiles
        else {
            // If there are no objects available from the pool, go through all active Floor objects in the scene and check if you can deactivate some of them
            activeFloors = GameObject.FindGameObjectsWithTag("Floor");
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

    private void FixedUpdate()
    {
        timeLimit -= Time.deltaTime;
    }

    /// <summary>
    /// Place floor elements relative to the player position
    /// </summary>
    public void placeBlock(GameObject block)
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

    public GameObject[] getActiveFloors()
    {
        return activeFloors;
    }
}
