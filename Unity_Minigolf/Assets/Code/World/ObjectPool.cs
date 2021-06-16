using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.World
{
    [Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int poolSize;
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
        private void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (var item in itemPool)
            {
                for (var i = 0; i < item.poolSize; i++)
                {
                    var tmp = Instantiate(item.objectToPool);
                    tmp.SetActive(false);
                    pooledObjects.Add(tmp);
                }
            }
        }

        /// <summary>
        /// Get an object from the pool by tag
        /// </summary>
        /// <param name="tag">of the pooled object</param>
        /// <returns>Pooled game object or null if no elements in the pool</returns>
        public GameObject GetPooledObject(string tag)
        {
            foreach (var obj in pooledObjects)
            {
                if (!obj.activeInHierarchy && obj.CompareTag(tag))
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Get an object from the pool by tag and also filter by name.
        /// </summary>
        /// <param name="tag">of the pooled object</param>
        /// <param name="name">of the pooled object</param>
        /// <returns>Pooled GameObject</returns>
        public GameObject GetPooledObject(string tag, string name)
        {
            foreach (var obj in pooledObjects)
            {
                if (!obj.activeInHierarchy && obj.CompareTag(tag) && obj.name.Contains(name))
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a random object from a pool.
        /// </summary>
        /// <param name="tag">of the pool</param>
        /// <returns>Pooled GameObject</returns>
        public GameObject GetRandomPoolObject(string tag)
        {
            var availableObjs =
                pooledObjects.Where(obj => !obj.activeInHierarchy && obj.CompareTag(tag)).ToArray();

            // The current implementation depends on the pool size. More objects in a pool -> higher chance to pick

            if (availableObjs.Length <= 0) return null;
            var randomVal = (int) (Random.value * availableObjs.Length);
            return availableObjs[randomVal];
        }
    }
}