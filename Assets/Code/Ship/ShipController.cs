using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ShipController : IMove, IRotation, IHealth
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly ShipModel _shipModel;
        private readonly ShipView _shipView;
        public float EngineForce => _moveImplementation.EngineForce;
        public Action ShipDestroy; 

        public ShipController(GameObject shipGameObject, ShipData shipData)
        {
            _shipModel = new ShipModel(shipData);
            _shipModel.ShipDestroy += OnShipDestroy;

            _shipView = shipGameObject.GetComponent<ShipView>();
            _shipView.GettingHealth += SetHealthAid;
            _shipView.GettingDamage += GetDamage;

            var engineForce = _shipModel.EngineForce;
            var acceleration = _shipModel.Acceleration;

            _moveImplementation = ChoseMoveType(shipData);

            //_moveImplementation = new MoveForce(shipGameObject.transform, engineForce);
            //_rotationImplementation = new RotationShip(shipGameObject.transform);
        }

        private IMove ChoseMoveType(ShipData shipData)
        {
            IMove move;
            switch (shipData.MovingType)
            {
                case MovingType.MoveTransform:
                    move = new MoveTransform(_shipView.gameObject.transform, _shipModel.EngineForce);
                    break;
                case MovingType.MoveForce:
                    move = new MoveForce(_shipView.gameObject.transform, _shipModel.EngineForce);
                    break;
                case MovingType.AcselerationMove:
                    move = new AccelerationMove(_shipView.gameObject.transform, _shipModel.EngineForce, _shipModel.Acceleration);
                    break;
                default:
                    move = new MoveTransform(_shipView.gameObject.transform, _shipModel.EngineForce);
                    break;
            }
            return move;
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

        public void OnShipDestroy()
        {
            ShipGameObjectSetActive(false);
            ShipDestroy?.Invoke();            
        }

        protected void ShipGameObjectSetActive(bool isActive)
        {
            _shipView.gameObject.SetActive(isActive);
        }

        protected void ShipGameObjectSetPosition(Vector2 position)
        {
            _shipView.transform.localPosition = position;
        }
    }
}

