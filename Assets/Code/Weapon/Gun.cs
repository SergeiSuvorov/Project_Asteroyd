using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : BallisticWeapon, IWeapon
{
    private Transform _barrel;

    public void Shoot(Vector3 direction)
    {
        ShootFromBarrel(_barrel, direction);
    }

    public void Instantiate(Transform barrel, AmmoData ammo, float shootStartForce)
    {
        CreateBulletTypePoolWhithBuilder(ammo);
        _barrel = barrel;
        _force = shootStartForce;
    }
}
