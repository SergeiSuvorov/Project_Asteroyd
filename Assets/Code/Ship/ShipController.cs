using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal sealed class ShipController : IMove, IRotation, IHealth
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly ShipModel _shipModel;
        private readonly ShipView _shipView;
        public float EngineForce => _moveImplementation.EngineForce;


        public ShipController(GameObject shipGameObject, ShipData shipData)
        {
            _shipModel = new ShipModel(shipData);
            _shipModel.ShipDestroy += ShipDestroy;

            _shipView = shipGameObject.GetComponent<ShipView>();
            _shipView.GetHealth += SetHealthAid;
            _shipView.GetDamage += GetDamage;

            var engineForce = _shipModel.EngineForce;
            var acceleration = _shipModel.Acceleration;
            
            _moveImplementation = new AccelerationMove(shipGameObject.transform, engineForce, acceleration);

            //_moveImplementation = new MoveForce(shipGameObject.transform, engineForce);
            //_rotationImplementation = new RotationShip(shipGameObject.transform);
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
            if (_rotationImplementation != null)
            {
                _rotationImplementation.Rotation(direction);
            }
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

        public void GetDamage(float damage)
        {
            _shipModel.GetDamage(damage);
        }

        public void SetHealthAid(float healthAid)
        {
            _shipModel.SetHealthAid(healthAid);
        }

        public void ShipDestroy()
        {
            Debug.Log("уничтожен");
        }
    }
}

