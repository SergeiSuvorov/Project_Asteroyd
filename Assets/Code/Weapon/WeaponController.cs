using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Proxy.ProxyProtection;

public class WeaponController
{
    private IWeapon _weapon;
    private WeaponModel _weaponModel;
    private Transform _barrelTransform;
    public WeaponController(WeaponData weaponData, GameObject bulletGameObject, Transform barrelTransform)
    {
        _weaponModel = new WeaponModel(weaponData);
        var force = weaponData.Force;
        ChoseWeaponType();
        _barrelTransform = barrelTransform;
        _weapon.Instantiate(barrelTransform, bulletGameObject, force);
    }

    /// <summary>
    /// Метод проверяющий какой тип оружия был передан в WeaponModel и создающий оружие в соответсвии с ним
    /// </summary>
    private void ChoseWeaponType()
    {
        Debug.Log(_weaponModel.WeaponType);
        if (_weaponModel.WeaponType == WeaponType.Gun)
        {
            _weapon = new Gun();
        }
        if (_weaponModel.WeaponType == WeaponType.TwinGun)
        {
            _weapon = new TwinGun();
        }

        _weapon = new WeaponProxy(_weapon);
    }

    /// <summary>
    /// Метод осуществляющий выстрел из оружия
    /// </summary>
    /// <param name="shipPosition"></param>
    public void Shoot(Vector3 shipPosition)
    {
        var direction = _barrelTransform.position - shipPosition;
        _weapon.Shoot(direction);
    }
}
