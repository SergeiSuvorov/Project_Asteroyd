using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class AsterroidModel:IHealth
    {
        [SerializeField] private Health _health;
        public Health Health => _health;
        [SerializeField] private float _damage;
        public float Damage => _damage;
        public AsterroidModel(AsterroidData asterroidData)
        {
            _health = CreateRandomHealth(asterroidData.Health);
            _damage = asterroidData.Damage;
        }

        private Health CreateRandomHealth(float health)
        {
            var maxRandomHealth = health + health / 2;
            var minRandomHealth = health - health / 2;

            var randomHealthValue = UnityEngine.Random.Range(minRandomHealth, maxRandomHealth);
            Health randomHealth = new Health(randomHealthValue, randomHealthValue);
            return randomHealth;
        }

        public void GetDamage(float damage)
        {
            _health.Damage(damage);
        }

        public void SetHealthAid(float healthAid)
        {
            throw new NotImplementedException();
        }
    }

}