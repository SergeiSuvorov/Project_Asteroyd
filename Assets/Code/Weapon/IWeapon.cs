using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void Instance(Transform barrel, GameObject ammo, float shootStartForce);
    void Shoot(Vector3 direction);

}
