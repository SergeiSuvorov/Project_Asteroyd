using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal class MoveTransform : IMove
    {
        private readonly Transform _transform;
        private Vector3 _move;

        public float EngineForce { get; protected set; }

        public MoveTransform(Transform transform, float engineForce)
        {
            _transform = transform;
            EngineForce = engineForce;
        }

        public virtual void Move(float horizontal, float vertical, float deltaTime)
        {

            var speed = deltaTime * EngineForce;
            _move.Set(horizontal * speed, vertical * speed, 0.0f);
            _transform.localPosition += _move;
            
        }
    }
}
