using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public abstract class EnemyController : IHealth
    {

        [SerializeField] protected GameObject _enemyGameObject;
        public GameObject EnemyGameObject => _enemyGameObject;
   
        public EnemyController(GameObject gameObject)
        {
            _enemyGameObject = gameObject;
        }

        public abstract void GetDamage(float damage);

        public abstract void SetHealthAid(float healthAid);
    }
}
