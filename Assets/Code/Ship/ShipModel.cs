using Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel: IHealth 
{
    private float _engineForce;
    private float _acceleration;
    private Health _health;

    public float EngineForce => _engineForce;
    public float Acceleration => _acceleration;
    public Health HealthPoint => _health;

    public Action ShipDestroy;

    public ShipModel(ShipData shipData)
    {
        _engineForce = shipData.Speed;
        _acceleration = shipData.Acceleration;
        _health = new Health( shipData.HealthPoint, shipData.HealthPoint);
    }

    public void GetDamage(float damage)
    {
        _health.Damage(damage);
       
        if (_health.Current <= 0)
        {
            ShipDestroy?.Invoke(); 
        }
        
    }

    public void SetHealthAid(float healthAid)
    {
        _health.SetHealthAid(healthAid);
    }
}
