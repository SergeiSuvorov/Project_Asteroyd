using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExecute
{
    bool IsActive { get; }

    void Execute();
}
