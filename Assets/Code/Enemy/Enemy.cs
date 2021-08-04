
using Asteroids.Pools;
using UnityEngine;
using ObjectPool;
using System;

namespace Asteroids
{
    public abstract class Enemy : MonoBehaviour, IHealth
    {
        public static IEnemyFactory Factory;
        private Transform _rotPool;
        private Health _health;

        public Action<Enemy> IsDestroy;

        public Health Health
        {
            get
            {
                if (_health.Current <= 0.0f)
                {
                    IsDestroy?.Invoke(this);
                }
                return _health;
            }
            protected set => _health = value;
        }

        public static Asteroid CreateAsteroidEnemy(Health hp)
        {
            var enemy = UnityEngine.Object.Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));

            enemy.Health = hp;

            return enemy;
        }

        public static Enemy CreateAsteroidEnemyWithPool(ObjectPool.ObjectPool enemyPool, Health hp)
        {
            var enemy = enemyPool.GetFromPool().GetComponent<Asteroid>();
            
            enemy._health = hp;

            float xEnemyPosition = UnityEngine.Random.Range(-8, 8);
            float yEnemyPosition = UnityEngine.Random.Range(10, 20);
            enemy.transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
            return enemy;
        }

        public void SetRandomStartPosition(float xMin, float xMax, float yMin, float yMax)
        {
            float xEnemyPosition = UnityEngine.Random.Range(xMin, xMax);
            float yEnemyPosition = UnityEngine.Random.Range(yMin, yMax);
           
            transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
        }

        public void GetDamage(float damage)
        {
            _health.Damage(damage);
            if (_health.Current <= 0.0f)
            {
                IsDestroy?.Invoke(this);
            }
        }

        public void SetHealthAid(float healthAid)
        {
            _health.SetHealthAid( healthAid);
        }
        
    }
}
