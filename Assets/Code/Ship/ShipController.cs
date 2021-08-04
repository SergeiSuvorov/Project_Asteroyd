using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal sealed class ShipController : IMove, IRotation
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly ShipModel _shipModel;
        public float EngineForce => _moveImplementation.EngineForce;


        public ShipController(GameObject shipGameObject, ShipData shipData)
        {
            _shipModel = new ShipModel(shipData);
            var engineForce = _shipModel.EngineForce;
            var acceleration = _shipModel.Acceleration;
            _moveImplementation = new MoveForce(shipGameObject.transform, engineForce);
            //_moveImplementation = new AccelerationMove(shipGameObject.transform, engineForce, acceleration);
            _rotationImplementation = new RotationShip(shipGameObject.transform);
        }

        /// <summary>
        /// Метод осуществляющий движение корабля
        /// </summary>
        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
        }

        /// <summary>
        /// Метод осуществляющий поворот корабля
        /// </summary>
        public void Rotation(Vector3 direction)
        {
            _rotationImplementation.Rotation(direction);
        }

        /// <summary>
        /// Метод осуществляющий изменени режима ускорения(вкл/выкл)
        /// </summary>
        public void ChangeAccelerationMode()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.CangeAccelerationMode();
            }
        }

    }
}

