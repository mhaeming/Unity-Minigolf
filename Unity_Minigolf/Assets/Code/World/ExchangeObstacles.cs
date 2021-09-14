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

    //TODO: replace hardcoding of elements sizes
    private float[] obstacleSizes = new float[] {4f * 0.15f, 42f * 0.015f, 50f*0.013f, 4.3f*0.15f, 0.015f*20f, 1.74f*0.5f };

    private void OnEnable()
    {
        _fullSceneManager = GameObject.FindWithTag("SceneChange").GetComponent<FullSceneManager>();
        _objectPool = gameObject.GetComponent<ObjectPool>();
        _obstacles = _objectPool.itemPool[2];
        _obstacles.objectToPool = obstacles[_fullSceneManager.playerChoice];
        WorldGenerator.generator._obstacleSize = obstacleSizes[_fullSceneManager.playerChoice];

    }
}
