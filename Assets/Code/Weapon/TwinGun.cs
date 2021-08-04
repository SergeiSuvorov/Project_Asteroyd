using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinGun : BallisticWeapon, IWeapon
{
    private Transform _firstBarrelTransform;
    private Transform _secondBarrelTransform;

    public void Shoot(Vector3 direction)
    {
        var bullet = ShootFromBarrel(_firstBarrelTransform);
        bullet.Shooting(direction, _force);

        bullet = ShootFromBarrel(_secondBarrelTransform);
        bullet.Shooting(direction, _force);
    }

    public void Instantiate(Transform barrel, GameObject ammo, float shootStartForce)
    {
        CreateBulletPool(ammo);
        _force = shootStartForce;
        Vector3 firstBarrelPosition = new Vector3(barrel.position.x - 0.5f, barrel.position.y );
        Vector3 secondBarrelPosition = new Vector3(barrel.position.x + 0.5f , barrel.position.y );

        _firstBarrelTransform = CreateBarrel("1stBarrel", barrel, firstBarrelPosition);
        _secondBarrelTransform = CreateBarrel("2ndBarrel", barrel, secondBarrelPosition);
    }

    private Transform CreateBarrel(string barrelName, Transform barrelParent, Vector2 barrelPosition)
    {
        GameObject barrel = new GameObject(barrelName);
        barrel.transform.parent = barrelParent;
        barrel.transform.position = barrelPosition;
        var barrelTransform = barrel.transform;

        return barrelTransform;
    }
}
