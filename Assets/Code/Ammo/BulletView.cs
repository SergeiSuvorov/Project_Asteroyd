using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _bulletRigidbody;
    [SerializeField] private float _damage=50;// будет измененно при появлении модели

    public Action InCollision;

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
        if (collision.gameObject.GetComponent<IHealth>() != null)
            collision.gameObject.GetComponent<IHealth>().GetDamage(_damage);

        InCollision?.Invoke();
    }

}
