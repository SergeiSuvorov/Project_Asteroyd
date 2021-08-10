using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Prototype;


public class AsterroidManager : IExecute
{
    private GameObject _asterroidPrototype;
    private List<AsterroidController> _asterroidControllersPool = new List<AsterroidController>();
    private TypePool<AsterroidController> _typePool;
    private List<AsterroidController> _asterroids =new List<AsterroidController>();
    private AsterroidController _asterroidControllerPrototype;
    public bool IsActive { get; private set; }

    public  AsterroidManager ()
    {
       var asterroidDataPrototype = Resources.Load<AsterroidData>("Entities/Enemy/Asterroid");
        _asterroidPrototype = asterroidDataPrototype.EnemyGameObject;
        _asterroidControllerPrototype = new AsterroidController(_asterroidPrototype, asterroidDataPrototype.Health);
        _typePool = new TypePool<AsterroidController>(_asterroidControllerPrototype);

        for (int i = 0; i < 5; i++)
        {
            AsterroidController asterroidController = CreateAsterroidFromTypePool();
            asterroidController.SetRandomStartPosition(-8, +8, 6 + i, 15 + i);
        }

        IsActive = true;
    }
  
    public void Execute()
    {
        for (int i = 0; i < _asterroids.Count; i++)
        {
            if (_asterroids[i].EnemyGameObject.transform.position.y < -5)
            {
                onAsteroidDestroy(_asterroids[i]);
            }
        }

        if (_asterroids.Count < 2)
        {
            var asterroidCreateCount = 0;
            while (_asterroids.Count < 5)
            {
                var asterroidController = CreateAsterroidFromTypePool();
                asterroidController.SetRandomStartPosition(-8, +8, 10 + asterroidCreateCount, 25 + asterroidCreateCount);
                asterroidCreateCount++;
            }
        }
    }

    void onAsteroidDestroy(AsterroidController asterroid)
    {
        asterroid.IsDestroy += null;
        _asterroids.Remove(asterroid);
        _typePool.ReturnToPool(asterroid);
    }

    public AsterroidController CreateAsterroidFromTypePool()
    {
        
        AsterroidController asterroidController = _typePool.GetFromPool();

        asterroidController.IsDestroy += onAsteroidDestroy;
        _asterroids.Add(asterroidController);
        return asterroidController;
    }
}
