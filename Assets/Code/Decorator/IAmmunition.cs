using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal interface IAmmunition
    {
        Rigidbody BulletInstance { get; }
        float TimeToDestroy { get; }
    }
}

