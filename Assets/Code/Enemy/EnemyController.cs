using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public abstract class EnemyController : IHealth
    {


        [SerializeField] protected Health _health;
        [SerializeField] protected GameObject _enemyGameObject;
        public GameObject EnemyGameObject => _enemyGameObject;


        public Health Health
        {
            get
            {
                
                return _health;
            }
            protected set => _health = value;
        }

    
        public EnemyController(GameObject gameObject, Health health)
        {
            _enemyGameObject = gameObject;
            _health = health;
        }

  
        public abstract void GetDamage(float damage);


        public abstract void SetHealthAid(float healthAid);
       

    }
}
