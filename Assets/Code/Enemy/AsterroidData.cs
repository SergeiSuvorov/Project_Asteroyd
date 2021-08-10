using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "NewAsterroid", menuName = "Data/Enemy")]
    public class AsterroidData : ScriptableObject,IEnemyData
    {

        [SerializeField] private GameObject _enemyGameObject;
        public GameObject EnemyGameObject => _enemyGameObject;

        [SerializeField] private  float _health;
        public Health Health => CreateRandomHealth();

        public IEnemyData GetEnemyData()
        {
        return this;
        }

        private Health CreateRandomHealth()
        {
            var maxRandomHealth = _health + _health / 2;
            var minRandomHealth = _health - _health / 2;

            var randomHealthValue = Random.Range(minRandomHealth,maxRandomHealth);
            Health randomHealth = new Health(randomHealthValue, randomHealthValue);
            return randomHealth;
        }
    }

}
