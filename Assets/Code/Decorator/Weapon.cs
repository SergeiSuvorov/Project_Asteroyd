using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal sealed class Weapon : IFire
    {
        private Transform _barrelPosition;
        private IAmmunition _bullet;
        private float _force;
       

        public Weapon(IAmmunition bullet, Transform barrelPosition, float force)
        {
            _bullet = bullet;
            _barrelPosition = barrelPosition;
            _force = force;
        }

        public void SetBarrelPosition(Transform barrelPosition)
        {
            _barrelPosition = barrelPosition;
        }
       

        public void SetBullet(IAmmunition bullet)
        {
            _bullet = bullet;
        }

        public void SetForce(float force)
        {
            _force = force;
        }

       
        public void Fire()
        {
            var bullet = Object.Instantiate(_bullet.BulletInstance, _barrelPosition.position, Quaternion.identity);
            bullet.AddForce(_barrelPosition.forward * _force, ForceMode.Impulse);
            Object.Destroy(bullet.gameObject, _bullet.TimeToDestroy);
        }
    }
}

