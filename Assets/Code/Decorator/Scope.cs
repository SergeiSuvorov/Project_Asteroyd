using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids.Decorator
{
    internal sealed class Scope: IScope
    {
        public Transform GunPositionScope { get; }
        public GameObject ScopeInstance { get; }

        public Scope(Transform gunPositionScope, GameObject scopeInstance)
        {
            GunPositionScope = gunPositionScope;
            ScopeInstance = scopeInstance;
        }
    }
}
