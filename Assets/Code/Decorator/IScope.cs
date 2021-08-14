using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids.Decorator
{
    internal interface IScope
    {

        Transform GunPositionScope { get; }
        GameObject ScopeInstance { get; }

    }
}
