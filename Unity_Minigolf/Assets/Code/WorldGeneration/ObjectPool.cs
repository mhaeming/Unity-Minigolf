using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.WorldGeneration
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int poolSize;
        public float spawnChance;
    }


    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool sharedInstance;
        public List<GameObject> pooledObjects;
        public List<ObjectPoolItem> itemPool;

        private void Awake()
        {
            sharedInstance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemPool) 
            {
                for (int i = 0; i < item.poolSize; i++)
                {
                    GameObject tmp = Instantiate(item.objectToPool);
                    tmp.SetActive(false);
                    pooledObjects.Add(tmp);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    
        // Allows additional filter by name
        public GameObject GetPooledObject(string tag, string name)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag) && pooledObjects[i].name.Contains(name))
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }

        public GameObject GetRandomPoolObject(string tag)
        {
            GameObject[] availableObjs = pooledObjects.Where(obj => !obj.activeInHierarchy && obj.CompareTag(tag)).ToArray();

            // TODO: Use probality distribution
            // The current implementation depends on the pool size. More objects in a pool -> higher chance to pick
        
            if (availableObjs.Length > 0)
            {
                int randomVal = (int)(Random.value * availableObjs.Length);
                return availableObjs[randomVal];
            }

            return null;
        }
    }
}