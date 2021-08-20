using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Chain_of_Responsibility
{
    public class AddEnemyModifire : EnemyManagerModifire
    {
        private int _enemyCount;
        public AddEnemyModifire(EnemyManager enemy, int enemyCount) : base(enemy)
        {
            _enemy = enemy;
            _enemyCount = enemyCount;
        }

        private IEnumerator WaitingEnemyDeath()
        {
            while (_enemy.EnemyCount > 0)
                yield return null;
            base.Handle();
        }
        public override void Handle()
        {
            _enemy.EnemyCount = _enemyCount;
            CoroutineExtensions.StartCoroutine(WaitingEnemyDeath(), out _);
            Debug.Log("Handle");
        }

    }
}
   
