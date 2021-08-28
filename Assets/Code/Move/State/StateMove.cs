using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMove
{
    protected Transform _transform;
    public StateMove(Transform objectTransform)
    {
        _transform = objectTransform;
    }
    public abstract void ExecuteMovements(float MovingValue);
}
