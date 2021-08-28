using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.ServiceLocator;
using System;

namespace Asteroids
{
    internal sealed class PlayerController
    {
       
        private Camera _camera;
        private ShipGameComandController _shipController;
        //private WeaponController _weaponController;
        private ShipData _shipData;
        private GameObject _playerGameObject;

        public Action ShipDestroy;
        
        public PlayerController(GameObject playerGameObject, ShipData shipData, WeaponData weaponData)
        {
            _camera = Camera.main;
        
            _shipController = new ShipGameComandController(shipData, weaponData, playerGameObject.transform);
            _shipController.ShipDestroy += OnShipDestroy;

        }
        public void OnShipDestroy()
        {
            Debug.Log("уничтожен PC");
            ShipDestroy?.Invoke();
        }
        /// <summary>
        /// Метод осуществляющий перемещение корабля передает в  ShipController перемещение по осям 
        /// </summary>
        public void Move(float horizontalMove, float verticalMove, bool IsChangeAcceleretion)
        {
            _shipController.Move(horizontalMove, verticalMove, Time.deltaTime);
        }

        // <summary>
        /// Метод осуществляющий вращение корабля передает в  ShipController направление
        /// </summary>
        public void Rotation(Vector3 mousePosition)
        {
            var direction = mousePosition - _camera.WorldToScreenPoint(_playerGameObject.transform.position);
            _shipController.Rotation(direction);
        }

        /// <summary>
        /// Метод включающий/отключающий ускорение коробля через ShipController
        /// </summary>
        public void ChangeAcceleretionMode()
        {
            _shipController.ChangeAccelerationMode();
        }

        /// <summary>
        /// Метод передающий в ShipController о необходимости выстрела
        /// </summary>
        public void Shooting()
        {
            //_weaponController.Shoot(_playerGameObject.transform.position);
            _shipController.Shooting();

        }

        public void OnRestartGame()
        {
            _shipController.OnRestartGame();
        }
        
    }

    

}

