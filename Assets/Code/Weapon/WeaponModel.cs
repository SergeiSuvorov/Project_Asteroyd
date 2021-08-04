using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel 
{
    [SerializeField] private float _force;
    public float Force => _force;
    [SerializeField] private WeaponType _weaponType;
    public WeaponType WeaponType => _weaponType;

    public WeaponModel(WeaponData weaponData)
    {
        _force = weaponData.Force;
        _weaponType = weaponData.WeaponType;
    }
}
