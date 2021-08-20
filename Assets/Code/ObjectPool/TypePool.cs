using Asteroids;
using Asteroids.Prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypePool<T> 
{
    private readonly List<T> _objectList = new List<T>(); 
    private readonly T _prototype;
    private readonly ListExecuteObject _listExecuteObject;

    public int PoolCount { get { return _objectList.Count; } }
    public TypePool(T prefab)
    {
         _prototype = prefab;


        if (_prototype is IExecute)
        {
            _listExecuteObject = UnityEngine.Object.FindObjectOfType<GameController>().ListExecuteObject;
            _listExecuteObject.AddExecuteObject(_prototype as IExecute);
        }

        if (prefab is ITypePoolObject)
        {
            (prefab as ITypePoolObject).ExecuteAfterGetToPool();
        }
    }

    public void ReturnToPool(T typeObject)
    {
        if (typeObject is ITypePoolObject)
        {
            (typeObject as ITypePoolObject).ExecuteBeforReturnToPool();
        }

        if (!_objectList.Contains(typeObject))
            _objectList.Add(typeObject);
    }

    public bool CheckToContainsInPool(T typeObject)
    {
        return _objectList.Contains(typeObject);
    }
    public T GetFromPool() 
    {
        T typeObject;
        if (_objectList.Count == 0)
        {
            typeObject = _prototype.DeepCopy();
            if (typeObject is ITypePoolObject)
            {
                (typeObject as ITypePoolObject).ExecuteAfterDeepCopy();
            }

            if (_listExecuteObject != null)
            {
                _listExecuteObject.AddExecuteObject(typeObject as IExecute);
            }
        }
        else
        {
            typeObject = _objectList[0];
            _objectList.Remove(typeObject);
        }

        if (typeObject is ITypePoolObject)
        {
            (typeObject as ITypePoolObject).ExecuteAfterGetToPool();
        }

        return typeObject;
    }
}

