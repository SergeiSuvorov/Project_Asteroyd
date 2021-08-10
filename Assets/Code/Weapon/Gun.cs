using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : BallisticWeapon, IWeapon
{
    private Transform _barrel;

    public void Shoot(Vector3 direction)
    {
        var bullet = ShootFromBarrel(_barrel);
        bullet.Shooting(direction, _force);
    }

    public void Instantiate(Transform barrel, GameObject ammo, float shootStartForce)
    {
        CreateObjectPoolWithBuilder();
        _barrel = barrel;
        _force = shootStartForce;
    }
}
