using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour, IHealth
{
    public Action<float> GetDamage;
    public Action<float> GetHealth;
    void IHealth.GetDamage(float damage)
    {
        GetDamage?.Invoke(damage);
    }

    void IHealth.SetHealthAid(float healthAid)
    {
        GetHealth?.Invoke(healthAid);
    }
}
