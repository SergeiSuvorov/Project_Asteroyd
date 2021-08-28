using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class AsterroidView : MonoBehaviour,IHealth
    {
        private Rigidbody2D _rigidbody;
        public Action<float> GettingDamage;
        public Action<GameObject> InCollision;
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.down;
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
            Debug.Log(collision.gameObject);
            InCollision?.Invoke(collision.gameObject);
        }

        public void GetDamage(float damage)
        {
                GettingDamage?.Invoke(damage);
        }

        public void SetHealthAid(float healthAid)
        {
            
        }
    }
}


