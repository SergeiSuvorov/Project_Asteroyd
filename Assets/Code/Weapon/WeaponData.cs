using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/Items/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _force;
    public float Force => _force;
    [SerializeField] private WeaponType _weaponType;
    public WeaponType WeaponType => _weaponType;
}
public enum WeaponType : byte {Gun=0 , TwinGun=1};
