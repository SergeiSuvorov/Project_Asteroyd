using System.Collections.Generic;
using UnityEngine;
using System;
using Asteroids;

namespace ObjectPool
{
    public class ObjectPool
    {
        private readonly List<GameObject> _objectList = new List<GameObject>();
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();

        private readonly GameObject _prefab;
        private readonly GameObject _poolGameObject;
        private readonly ListExecuteObject _listExecuteObject;

        private int _createObjectIndex;
        public int CreateObjectIndex{get{ return _createObjectIndex; } }
        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;

            _poolGameObject = new GameObject();
            _poolGameObject.name = prefab.name + " pool";
            
            if (_prefab.GetComponent<IExecute>()!=null)
            {
                _listExecuteObject = UnityEngine.Object.FindObjectOfType<GameController>().ListExecuteObject;
            }

            if (_listExecuteObject != null && _prefab.activeSelf)
            {
                _listExecuteObject.AddExecuteObject(_prefab.GetComponent<IExecute>());
                ReturnToPool(_prefab);
            }
        }
        
        public void ReturnToPool(GameObject go)
        {
            // Работает на основе List
            //AddToPoolList(go);
            // Работает на основе Stack
            Push(go);
        }

        public GameObject GetFromPool()
        {

            GameObject go;
            // Работает на основе List
            //go = RemoveFromPoolList();
            // Работает на основе Stack
            go = Pop();
            go.transform.parent = null;

            // Проводим проверку активен ли объект после извлечения из пула 
            //if (!go.activeInHierarchy)
            //{
            //    Debug.Log("object is anactive -" + go.name);

            //    Debug.Log("object  " + go.name + "  " + go.activeInHierarchy);
            //}
            //else
            //{
            //    Debug.Log("object is active" + "  " + go.activeSelf);
            //}
            return go;
        }

        private void AddToPoolList(GameObject go)
        {
            //if (_objectList.Contains(go))
            //{

            //    Debug.Log("double in list");
                
            //    if (go.activeInHierarchy)
            //        go.SetActive(false);
            //    return;
            //}
            if (!_objectList.Contains(go))
                _objectList.Add(go);
            go.transform.parent = _poolGameObject.transform;
            go.SetActive(false);
           

        }
        public void Push(GameObject go)
        {
            if (!_stack.Contains(go))
            {
                _stack.Push(go);
            }

            //if (_poolGameObject.transform.childCount >= _stack.Count)
            //    _stack.Push(go);
            //else Debug.Log(_poolGameObject.transform.childCount + " " + _stack.Count);
            go.transform.parent = _poolGameObject.transform;

            go.SetActive(false);
        }

        private GameObject RemoveFromPoolList()
        {
            GameObject go;
            if (_objectList.Count == 0)
            {
                go = UnityEngine.Object.Instantiate(_prefab);
                _createObjectIndex++;
                go.name = _prefab.name + " " + _createObjectIndex;

                //Добавление в список Execute - для использование 1го Update
                if (_listExecuteObject != null)
                {
                    _listExecuteObject.AddExecuteObject(go.GetComponent<IExecute>());
                }
            }
            else
            {
                go = _objectList[0];
                _objectList.Remove(go);
            }
            go.SetActive(true);
           
            return go;
        }
        
        public GameObject Pop()
        {
            GameObject go;

            if (_poolGameObject.transform.childCount < _stack.Count)
            {
                Debug.Log("Warning");

                while(_poolGameObject.transform.childCount < _stack.Count)
                {
                    go = _stack.Pop();
                }
            }

            if (_stack.Count ==0)
            {
                go = UnityEngine.Object.Instantiate(_prefab);
                _createObjectIndex++;
                go.name = _prefab.name + " " + _createObjectIndex;

                if (!go.activeInHierarchy)
                {
                    Debug.Log("object  " + go.name + "  " + go.activeInHierarchy + "  " + go.activeSelf);
                    Debug.Log("NotBulet");
                    go.SetActive(true);
                    if (!go.activeInHierarchy)
                        Debug.Log("NotBulet");
                }

                    //Добавление в список Execute - для использование 1го Update
                    if (_listExecuteObject != null)
                {
                    _listExecuteObject.AddExecuteObject(go.GetComponent<IExecute>());
                }
            }
            else
            {
                go = _stack.Pop();
                go.SetActive(true);
                if (!go.activeInHierarchy)
                {
                    Debug.Log("object  " + go.name + "  " + go.activeInHierarchy + "  " + go.activeSelf);
                    Debug.Log("NotBulet");
                    go.SetActive(true);
                    if (!go.activeInHierarchy)
                        Debug.Log("NotBulet");
                }
            }
            go.SetActive(true);

            return go;
        }
    }
}
