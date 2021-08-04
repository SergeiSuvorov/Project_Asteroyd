using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinGun : IWeapon
{
    private Transform _1stBarrelTransform;
    private Transform _2ndBarrelTransform;
    private GameObject _bullet;
    private float _force;


    public void Shoot(Vector3 direction)
    {
        var temAmmunition1 = Object.Instantiate(_bullet, _1stBarrelTransform.position, _1stBarrelTransform.rotation);
        var bullet = temAmmunition1.GetComponent<Bullet>();
        bullet.Shooting(direction, _force);

        var temAmmunition2 = Object.Instantiate(_bullet, _2ndBarrelTransform.position, _1stBarrelTransform.rotation);
        bullet = temAmmunition2.GetComponent<Bullet>();
        bullet.Shooting(direction, _force);
    }

    public void Instance(Transform barrel, GameObject ammoGameObject, float shootStartForce)
    {
        _bullet = ammoGameObject;
        _force = shootStartForce;
        Vector3 _1stBarelPosition = new Vector3(barrel.position.x - 0.5f, barrel.position.y );
        Vector3 _2ndBarelPosition = new Vector3(barrel.position.x + 0.5f , barrel.position.y );

        GameObject _1stBarrel= new GameObject("_1stBarrel");
        _1stBarrel.transform.parent = barrel;
        _1stBarrel.transform.position = _1stBarelPosition;
        _1stBarrelTransform = _1stBarrel.transform;

        GameObject _2ndBarrel = new GameObject("_2ndBarrel");
        _2ndBarrel.transform.parent = barrel;
        _2ndBarrel.transform.position = _2ndBarelPosition;
        _2ndBarrelTransform = _2ndBarrel.transform;

    }
}
