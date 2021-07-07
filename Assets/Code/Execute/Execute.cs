using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour
{
    private ListExecuteObject _listExecuteObject;

    // Update is called once per frame
    void Update()
    {
        if (_listExecuteObject == null)
            return;

        for (var i = 0; i < _listExecuteObject.Length; i++)
        {
            if (_listExecuteObject[i] == null || !_listExecuteObject[i].IsActive)
            {
                continue;
            }
            var interactiveObject = _listExecuteObject[i];
            interactiveObject.Execute();
        }
    }

    public void SetListtExecuteObject(ListExecuteObject listExecuteObject)
    {
        _listExecuteObject = listExecuteObject;
    }
}
