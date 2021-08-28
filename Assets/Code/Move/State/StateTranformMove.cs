using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTranformMove : IMove
{
    private readonly Transform _transform;
    private Vector3 _move;

    public float EngineForce { get; protected set; }

    public StateTranformMove(Transform transform, float engineForce)
    {
        _transform = transform;
        EngineForce = engineForce;
        Debug.Log("_____________________________________________________");
    }

    public void Move(float horizontal, float vertical, float deltaTime)
    {
        
        if(Mathf.Abs(horizontal)>0.01f)
        {
            var moveState = new MoveHorizontal(_transform);
            MoveContext moveContext = new MoveContext(moveState);
            var movingValue = horizontal * EngineForce * deltaTime;
            moveContext.ExecuteMovements(movingValue);
        }
        if (Mathf.Abs(vertical) > 0.01f)
        {
            var moveState = new MoveVertical(_transform);
            MoveContext moveContext = new MoveContext(moveState);
            var movingValue = vertical * EngineForce * deltaTime;
            moveContext.ExecuteMovements(movingValue);
        }

    }

}
