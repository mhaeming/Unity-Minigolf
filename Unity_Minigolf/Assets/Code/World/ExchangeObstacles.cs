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

    private float[] _obstacleSizes = new float[] {4f * 0.12f, 42f * 0.013f, 50f*0.018f, 4.3f*0.07f, 0.015f*20f, 1.74f*0.5f };

    private void OnEnable()
    {
        //CalculateObstacleSize();
        _fullSceneManager = GameObject.FindWithTag("SceneChange").GetComponent<FullSceneManager>();
        _objectPool = gameObject.GetComponent<ObjectPool>();
        _obstacles = _objectPool.itemPool[2];
        _obstacles.objectToPool = obstacles[_fullSceneManager.playerChoice];
        WorldGenerator.generator._obstacleSize = _obstacleSizes[_fullSceneManager.playerChoice];

    }
    /// <summary>
    /// dynamically calculates Obstacles' Heights by multiplying y-scale with Box Colliders y size
    /// TODO: figure out why it does not fit for all obstacles equally well?
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
