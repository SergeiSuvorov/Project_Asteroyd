using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : StateMove
{
    public MoveVertical(Transform objectTransform) : base(objectTransform)
    {
    }

    public override void ExecuteMovements(float MovingValue)
    {
        Vector3 move = new Vector3();
        move.Set(0, MovingValue, 0.0f);
        _transform.localPosition += move;
    }
}
