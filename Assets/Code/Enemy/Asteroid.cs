using UnityEngine;

namespace Asteroids
{
    public sealed class Asteroid : Enemy
    {
        [SerializeField] private Rigidbody2D rigidbody;
        private float damage = 50;
        void Start()
        {
            rigidbody.velocity = Vector2.down;
        }
        public void DependencyInjectHealth(Health hp)
        {
            Health = hp;
        }

        private void OnEnable()
        {
            rigidbody.velocity = Vector2.down;
        }

        private void OnDisable()
        {
            rigidbody.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IHealth>() != null)
                collision.gameObject.GetComponent<IHealth>().GetDamage(damage);

            Debug.Log(collision.gameObject.name);
        }
    }
}
