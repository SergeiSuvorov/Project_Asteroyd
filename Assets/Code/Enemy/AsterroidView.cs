using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class AsterroidView : MonoBehaviour,IHealth
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        //private GameObject _asteroidGameObject;
        [SerializeField] private float _damage = 50;
        public Action<float> GettingDamage;
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.down;
        }
     
        public AsterroidView (float damage)
        {
            if (GetComponent<Rigidbody2D>()!=null)
            _rigidbody = GetComponent<Rigidbody2D>();
            _damage = damage;
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
                collision.gameObject.GetComponent<IHealth>().GetDamage(_damage);

            //Debug.Log(collision.gameObject.name);
        }

        public void GetDamage(float damage)
        {
            //Debug.Log("View get damage");
                GettingDamage?.Invoke(damage);
        }

        public void SetHealthAid(float healthAid)
        {
            
        }
    }
}


