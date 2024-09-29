using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.Pooller
{
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        private Dictionary<string, List<GameObject>> _pooledObjects = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, GameObject> _poolsParents = new Dictionary<string, GameObject>();
        private string _poolPrefix = "Pool_";
        public static ObjectPool Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else
            { 
                Destroy(gameObject);
            }
        }

        public GameObject GetObjects(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return GetFreeObjectPool(prefab, position, rotation);
        }

        private GameObject GetFreeObjectPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var pooledName = $"{_poolPrefix}{prefab.gameObject.name}";

            if (_pooledObjects.ContainsKey(pooledName))
            {
                if (_pooledObjects[pooledName].Count > 0)
                {
                    var pooledObject = _pooledObjects[pooledName][0];
                    pooledObject.transform.position = position;
                    pooledObject.transform.rotation = rotation;
                    pooledObject.gameObject.SetActive(true);
                    _pooledObjects[pooledName].RemoveAt(0);
                    return pooledObject;
                }
                else
                {
                    var newPoolerObject = Instantiate(prefab, position, rotation, GetPoolsParent(pooledName).transform);
                    return newPoolerObject;
                }
            }
            else
            {
                var newPoolerObject = Instantiate(prefab, position, rotation, GetPoolsParent(pooledName).transform);
                List<GameObject> newPooledList = new List<GameObject>();
                _pooledObjects.Add(pooledName, newPooledList);
                return newPoolerObject;
            }
        }

        public void ReturnToPool(GameObject returningObject)
        {
            var returningObjectName = $"{_poolPrefix}{returningObject.name.Replace("(Clone)", "").Trim()}";
            if (_pooledObjects.ContainsKey(returningObjectName))
            {
                _pooledObjects[returningObjectName].Add(returningObject);
            }
        }

        private GameObject GetPoolsParent(string poolName)
        {

            if (_poolsParents.ContainsKey(poolName))
            {
                return _poolsParents[poolName];
            }
            else
            {
                var newGameObject = new GameObject(poolName);
                newGameObject.transform.parent = transform;
                _poolsParents.Add(poolName, newGameObject);
                return _poolsParents[poolName];
            }
        }
    }
}