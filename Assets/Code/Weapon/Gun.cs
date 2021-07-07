using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : IWeapon
{
    private Transform _barrel;
    private GameObject _bullet;
    private Rigidbody2D _bulletRigidbody;
    private float _force;
    

    
    public void Shoot(Vector3 direction)
    {
        var temAmmunition = Object.Instantiate(_bullet, _barrel.position, _barrel.rotation);
        var bullet = temAmmunition.GetComponent<Bullet>();
        bullet.Shooting(direction, _force);
    }

   
    public void Instance(Transform barrel, GameObject ammo, float shootStartForce)
    {
        _barrel = barrel;
        _bullet = ammo;
        _force = shootStartForce;
    }
}
