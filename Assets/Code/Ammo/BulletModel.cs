using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletModel
{
   [SerializeField] private  float _damage;
    public float Damage { get { return _damage; } }

    public BulletModel(AmmoData ammoData)
    {
        _damage = ammoData.Damage;
    }
    public BulletModel(BulletModel ammoData)
    {
        _damage = ammoData.Damage;
    }
}
