using System;
using System.Collections;
using System.Collections.Generic;
using Code.World;
using UnityEngine;

public class ExchangeObstacles : MonoBehaviour
{
    private ObjectPool _objectPool;
    private FullSceneManager _fullSceneManager;
    private ObjectPoolItem _obstacles;
    public GameObject[] obstacles;

    private float[] _obstacleSizes = new float[6];

    private void OnEnable()
    {
        CalculateObstacleSize();
        _fullSceneManager = GameObject.FindWithTag("SceneChange").GetComponent<FullSceneManager>();
        _objectPool = gameObject.GetComponent<ObjectPool>();
        _obstacles = _objectPool.itemPool[2];
        _obstacles.objectToPool = obstacles[_fullSceneManager.playerChoice];
        WorldGenerator.generator._obstacleSize = _obstacleSizes[_fullSceneManager.playerChoice];

    }
    /// <summary>
    /// dynamically calculates Obstacles' Heights by multiplying y-scale with Box Collider's y-size
    /// </summary>
    private void CalculateObstacleSize()
    {
        int i = 0;
        foreach (GameObject obstacle in obstacles)
        {
            _obstacleSizes[i] = obstacle.transform.lossyScale.y * obstacle.GetComponent<BoxCollider>().size.y;
            i++;
        } 
    }
}
