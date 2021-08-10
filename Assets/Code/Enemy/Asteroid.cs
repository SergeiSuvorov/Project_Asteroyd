using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public sealed class Asteroid : Enemy
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        //private GameObject _asteroidGameObject;
        [SerializeField] private float damage = 50;
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.down;
        }
        public void DependencyInjectHealth(Health hp)
        {
            Health = hp;
        }

        private void OnEnable()
        {
            _rigidbody.velocity = Vector2.down;
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IHealth>() != null)
                collision.gameObject.GetComponent<IHealth>().GetDamage(damage);

            Debug.Log(collision.gameObject.name);
        }
    }
}
