using System.Collections.Generic;
using UnityEngine;
using System;
using Asteroids;

namespace ObjectPool
{
    public class ObjectPool 
    {
        private readonly List<GameObject> _objectList = new List<GameObject>();
        private readonly GameObject _prefab;
        private readonly GameObject _poolGameObject;
        private readonly ListExecuteObject _listExecuteObject;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _poolGameObject = new GameObject();
            _poolGameObject.name = prefab.name + " pool";
            

            if (_prefab.GetComponent<IExecute>()!=null)
            {
                _listExecuteObject = UnityEngine.Object.FindObjectOfType<GameController>().ListExecuteObject;
            }
        }

        
        public void ReturnToPool(GameObject go)
        {
            if (!_objectList.Contains(go))
                _objectList.Add(go);

            go.SetActive(false);
            go.transform.parent = _poolGameObject.transform;
        }

        public GameObject GetFromPool()
        {
            GameObject go;
            if (_objectList.Count == 0)
            {
                go = UnityEngine.Object.Instantiate(_prefab);
                
                if (_listExecuteObject!=null)
                {
                    _listExecuteObject.AddExecuteObject(go.GetComponent<IExecute>());
                }
            }
            else
            {
                go= _objectList[0];
                _objectList.Remove(go);
            }
            go.SetActive(true);
            go.transform.parent = null;
            return go;
        }
    }
}
