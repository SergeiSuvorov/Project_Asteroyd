using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids.Proxy.ProxyProtection
{
    public sealed class WeaponProxy : IWeapon
    {
        private readonly IWeapon _weapon;
        private readonly UnlockWeapon _unlockWeapon;
        private readonly TimeRemaining _timeRemaining;
        private readonly int _ammoCount = 20;//20 использовано для проверки
        private int _currentAmmoCount;

        public WeaponProxy(IWeapon weapon, UnlockWeapon unlockWeapon)
        {
            _weapon = weapon;
            _unlockWeapon = unlockWeapon;
            _timeRemaining = new TimeRemaining(ReloadEnding, 5f);
            _currentAmmoCount = _ammoCount;
        }
        public WeaponProxy(IWeapon weapon)
        {
            _weapon = weapon;
            _unlockWeapon = new UnlockWeapon(true);
            _timeRemaining = new TimeRemaining(ReloadEnding, 5f);
            _currentAmmoCount = _ammoCount;
        }

        public void Shoot(Vector3 direction)
        {
            if (_unlockWeapon.IsUnlock)
            {
                _weapon.Shoot(direction);
                _currentAmmoCount--;
                if(_currentAmmoCount<=0)
                {
                    _unlockWeapon.IsUnlock = false;
                    Reloading();
                }
            }
            else
            {
                Debug.Log("Weapon is lock - reloading");
            }
        }

        public void Reloading()
        {
            _timeRemaining.AddTimeRemaining();
            
        }
        private void ReloadEnding()
        {
            _unlockWeapon.IsUnlock = true;
            _currentAmmoCount = _ammoCount;
            Debug.Log("Weapon is unlock");
        }
        public void Instantiate(Transform barrel, GameObject ammo, float shootStartForce)
        {
            _weapon.Instantiate(barrel, ammo, shootStartForce);
        }

    }
}

