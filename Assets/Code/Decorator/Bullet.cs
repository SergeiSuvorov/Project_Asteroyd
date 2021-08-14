using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal sealed class Bullet : IAmmunition
    {
        public Rigidbody BulletInstance { get; }
        public float TimeToDestroy { get; }

        public Bullet(Rigidbody bulletInstance, float timeToDestroy)
        {
            BulletInstance = bulletInstance;
            TimeToDestroy = timeToDestroy;
        }
    }
}

