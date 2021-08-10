using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.ServiceLocator;


namespace Asteroids
{
    internal sealed class PlayerController
    {
       
        private Camera _camera;
        private ShipController _shipController;
        private WeaponController _weaponController;
        private ShipData _shipData;
        private GameObject _playerGameObject;
       
        public PlayerController( GameObject bulletGameObject, GameObject shipGameObject, ShipData shipData, Transform barrelTransform,WeaponData weaponData)
        {
            _camera = Camera.main;
            _shipController = new ShipController(shipGameObject, shipData);
            _weaponController = new WeaponController(weaponData, bulletGameObject,barrelTransform);
            _playerGameObject = shipGameObject;


            ///Использование ServiceLocator
            int ammoCount =0;
            if(ServiceLocator.ServiceLocator.Resolve<ObjectPool.ObjectPool>() != null)
            {
                ammoCount = ServiceLocator.ServiceLocator.Resolve<ObjectPool.ObjectPool>().CreateObjectIndex;
                
            }
            if (ServiceLocator.ServiceLocator.Resolve<TypePool<BulletController>>() != null)
            {
                ammoCount = ServiceLocator.ServiceLocator.Resolve<TypePool<BulletController>>().PoolCount;
            }

            Debug.Log($"There are {ammoCount + 1} bullet in pool");

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
        /// Метод передающий в WeaponController о необходимости выстрела
        /// </summary>
        public void Shooting()
        {
            _weaponController.Shoot(_playerGameObject.transform.position);
        }
        
    }

    

}

