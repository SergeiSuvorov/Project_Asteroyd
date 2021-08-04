using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsterroidManager : IExecute
{
    private GameObject _asterroidPrefab;
    private ObjectPool.ObjectPool _asterroidPool;
    private List<Enemy> _asterroids =new List<Enemy>();
    public bool IsActive { get; private set; }

    public  AsterroidManager ()
    {
        _asterroidPrefab = Resources.Load<GameObject>("Enemy/Asteroid");
        _asterroidPool = new ObjectPool.ObjectPool(_asterroidPrefab);

        for (int i = 0; i < 40; i++)
        {
            var asterroid = Enemy.CreateAsteroidEnemyWithPool(_asterroidPool, new Health(10, 10));
            asterroid.IsDestroy += onEnemyDestroy;
            _asterroids.Add(asterroid);

            asterroid.SetRandomStartPosition(-8, +8, 6 + i, 15 + i);
        }

        IsActive = true;
    }
   
    void onEnemyDestroy(Enemy asterroid)
    {
        asterroid.IsDestroy += null;
        _asterroids.Remove(asterroid);
        _asterroidPool.ReturnToPool(asterroid.gameObject);
    }

    public void Execute()
    {
        for (int i = 0; i < _asterroids.Count; i++)
        {
            if (_asterroids[i].transform.position.y < -5)
                onEnemyDestroy(_asterroids[i]);
        }

        if (_asterroids.Count < 25)
        {
            var asterroidCreateCount = 0;
            while (_asterroids.Count < 40)
            {
                var asterroid = Enemy.CreateAsteroidEnemyWithPool(_asterroidPool, new Health(100, 100));
                asterroid.IsDestroy += onEnemyDestroy;
                _asterroids.Add(asterroid);

                asterroid.SetRandomStartPosition(-8, +8, 6 + asterroidCreateCount, 20 + asterroidCreateCount);
                asterroidCreateCount++;
            }
        }
    }
}
