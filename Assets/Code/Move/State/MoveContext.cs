using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveContext 
{
    private StateMove _state;
    
    public MoveContext (StateMove state)
    {
        _state = state;
    }

    public void ExecuteMovements(float MovingValue)
    {
        if(_state!=null)
        _state.ExecuteMovements(MovingValue);
    }
}
