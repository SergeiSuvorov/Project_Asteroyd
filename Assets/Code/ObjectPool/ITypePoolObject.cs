using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITypePoolObject 
{
    void ExecuteBeforReturnToPool();
    void ExecuteAfterGetToPool();

    void ExecuteAfterDeepCopy();
}
