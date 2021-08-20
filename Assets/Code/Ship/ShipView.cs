using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour, IHealth
{
    private float _collisionDamage=500; 
    public Action<float> GettingDamage;
    public Action<float> GettingHealth;

    
    public void GetDamage(float damage)
    {
        GettingDamage?.Invoke(damage);
    }

    public void SetHealthAid(float healthAid)
    {
        GettingHealth?.Invoke(healthAid);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IHealth>() != null)
            collision.gameObject.GetComponent<IHealth>().GetDamage(_collisionDamage);


        Debug.Log(gameObject.name);
    }
}
