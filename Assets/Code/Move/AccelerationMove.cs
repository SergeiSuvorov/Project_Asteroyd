using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal sealed class AccelerationMove : MoveTransform
    {
        private readonly float _acceleration;
        private  bool _isAcceleration=false;
        

        public AccelerationMove(Transform transform, float engineForce, float acceleration) : base(transform, engineForce)
        {
            _acceleration = acceleration;
        }

        public void CangeAccelerationMode()
        {
            if (_isAcceleration)
                RemoveAcceleration();
            else
                AddAcceleration();
        }

        private void AddAcceleration()
        {
            EngineForce += _acceleration;
            _isAcceleration = true;
        }

        private void RemoveAcceleration()
        {
            EngineForce -= _acceleration;
            _isAcceleration = false;
        }
    }
}

