using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public abstract class EnemyController : IHealth
    {
        protected static IEnemyFactory Factory;
        //private Transform _rotPool;
        [SerializeField] protected Health _health;
        [SerializeField] protected GameObject _enemyGameObject;
        public GameObject EnemyGameObject => _enemyGameObject;
        //public Action<EnemyController> IsDestroy;

        public Health Health
        {
            get
            {
                
                return _health;
            }
            protected set => _health = value;
        }



        //public static Asteroid CreateAsteroidEnemy(Health hp)
        //{
        //    var enemy = UnityEngine.Object.Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));

        //    enemy.Health = hp;

        //    return enemy;
        //}

        //public static Enemy CreateAsteroidEnemyWithPool(ObjectPool.ObjectPool enemyPool, Health hp)
        //{
        //    var enemy = enemyPool.GetFromPool().GetComponent<Asteroid>();

        //    enemy._health = hp;

        //    float xEnemyPosition = UnityEngine.Random.Range(-8, 8);
        //    float yEnemyPosition = UnityEngine.Random.Range(10, 20);
        //    enemy.transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
        //    return enemy;
        //}

        public EnemyController (IEnemyData EnemyData)
        {
            _enemyGameObject = EnemyData.EnemyGameObject;
            _health = EnemyData.Health;
        }
        public EnemyController(GameObject gameObject, Health health)
        {
            _enemyGameObject = gameObject;
            _health = health;
        }

        //public  abstract  EnemyController CreateEnemyWithPool(ObjectPool.ObjectPool enemyPool, Health hp);

        //public void SetRandomStartPosition(float xMin, float xMax, float yMin, float yMax)
        //{
        //    float xEnemyPosition = UnityEngine.Random.Range(xMin, xMax);
        //    float yEnemyPosition = UnityEngine.Random.Range(yMin, yMax);

        //    EnemyGameObject.transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
        //}

        public abstract void GetDamage(float damage);


        public abstract void SetHealthAid(float healthAid);
       

    }
}
