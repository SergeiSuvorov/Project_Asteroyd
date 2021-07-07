using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel: IHealth 
{
    private float _engineForce;
    private float _acceleration;
    private float _healthPoint;

    public float EngineForce => _engineForce;
    public float Acceleration => _acceleration;
    public float HealthPoint => _healthPoint;

    Action ShipDestroy;

    public ShipModel(ShipData shipData)
    {
        _engineForce = shipData.Speed;
        _acceleration = shipData.Acceleration;
        _healthPoint = shipData.HealthPoint;
    }

    public void GetDamage()
    {
        if (_healthPoint <= 0)
        {
            ShipDestroy?.Invoke(); 
        }
        else
        {
            _healthPoint--;
        }
    }

    public void SetHealth(float healthAid)
    {
        _healthPoint += healthAid;
    }
}
