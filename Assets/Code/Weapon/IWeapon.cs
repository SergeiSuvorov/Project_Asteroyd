using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void Instantiate(Transform barrel, AmmoData ammo, float shootStartForce);
    void Shoot(Vector3 direction);

}
