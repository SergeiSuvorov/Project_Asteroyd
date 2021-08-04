using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public interface IMove
    {
        float EngineForce { get; }
        void Move(float horizontal, float vertical, float deltaTime);
    }
}


