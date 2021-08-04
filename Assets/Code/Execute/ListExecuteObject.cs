using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class ListExecuteObject : IEnumerator, IEnumerable
{
    private IExecute[] _executeObjects;
    private int _index = -1;
    private IExecute _current;

    public ListExecuteObject()
    {
        var interactiveObjects = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
        for (var i = 0; i < interactiveObjects.Length; i++)
        {
            if (interactiveObjects[i] is IExecute interactiveObject)
            {
                
                   AddExecuteObject(interactiveObject);
                
            }
        }
    }

   
    public void AddExecuteObject(IExecute execute)
    {
        if (_executeObjects == null)
        {
            _executeObjects = new[] { execute };
            return;
        }
        Array.Resize(ref _executeObjects, Length + 1);
        _executeObjects[Length - 1] = execute;
    }

    public IExecute this[int index]
    {
        get => _executeObjects[index];
        private set => _executeObjects[index] = value;
    }

    public int Length => _executeObjects.Length;

    public bool MoveNext()
    {
        if (_index == _executeObjects.Length - 1)
        {
            Reset();
            return false;
        }

        _index++;
        return true;
    }

    public void Reset() => _index = -1;

    public object Current => _executeObjects[_index];

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

