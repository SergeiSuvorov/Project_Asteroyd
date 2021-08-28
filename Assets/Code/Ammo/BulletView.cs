using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _bulletRigidbody;

    public Action<GameObject> InCollision;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnDisable()
    {
        _bulletRigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        InCollision?.Invoke(collision.gameObject);
    }

}
