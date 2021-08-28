using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : StateMove
{
    public MoveHorizontal(Transform objectTransform) : base(objectTransform)
    {
    }

    public override void ExecuteMovements(float MovingValue)
    {
        Vector3 move = new Vector3();
        move.Set(MovingValue, 0, 0.0f);
        _transform.localPosition += move;
    }
}
