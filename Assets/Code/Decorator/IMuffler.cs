using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal interface IMuffler
    {

        Transform BarrelPositionMuffler { get; }
       Transform StartPositionMuffler { get; }
        GameObject MufflerInstance { get; }
    }
}

