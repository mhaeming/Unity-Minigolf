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

    private void OnEnable()
    {
        _fullSceneManager = GameObject.FindWithTag("SceneChange").GetComponent<FullSceneManager>();
        _objectPool = gameObject.GetComponent<ObjectPool>();
        _obstacles = _objectPool.itemPool[2];
        _obstacles.objectToPool = obstacles[_fullSceneManager.playerChoice];
    }
}
