using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal sealed class Muffler : IMuffler
    {
        public Transform BarrelPositionMuffler { get; }
        public GameObject MufflerInstance { get; }

        public Transform StartPositionMuffler { get; }

        public Muffler(Transform barrelPositionMuffler, Transform startPositionMuffler, GameObject mufflerInstance)
        {
            StartPositionMuffler = startPositionMuffler;
            BarrelPositionMuffler = barrelPositionMuffler;
            MufflerInstance = mufflerInstance;
        }
    }
}

