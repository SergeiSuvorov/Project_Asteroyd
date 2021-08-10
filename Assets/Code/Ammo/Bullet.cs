using Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IExecute
{
    [SerializeField] private Rigidbody2D _bulletRigidbody;
    [SerializeField] private float _damage;
    private Vector2 _direction;
    private float _shootStartForce;
    private float _lifeTime = 3f;

    public Action<Bullet> LifeTimeIsEnd;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
        _damage = 50;
    }
    public void Shooting(Vector2 direction, float shootStartForce)
    {
        _direction = direction;
        _shootStartForce = shootStartForce;
        _lifeTime = 3;
    }

    private void OnDisable()
    {
        _bulletRigidbody.velocity = Vector2.zero;
       _shootStartForce = 0;
        IsActive = false;
    }

    private void OnEnable()
    {
        IsActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IHealth>() != null)
            collision.gameObject.GetComponent<IHealth>().GetDamage(_damage);

        LifeTimeIsEnd?.Invoke(this);
    }

    public void Execute()
    {
        _lifeTime -= Time.deltaTime;
        _bulletRigidbody.velocity = _direction * _shootStartForce;

        if (_lifeTime < 0)
        {
            LifeTimeIsEnd?.Invoke(this);
        }
    }
}
