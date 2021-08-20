using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Prototype;
using System;

public class AsterroidManager : EnemyManager, IExecute
{
    private GameObject _asterroidPrototype;
    private List<AsterroidController> _asterroidControllersPool = new List<AsterroidController>();
    private TypePool<AsterroidController> _typePool;
    private List<AsterroidController> _asterroids =new List<AsterroidController>();
    private AsterroidController _asterroidControllerPrototype;
    private int scoreForAsterroidDestroy = 200;// очки за уничтожение астеройда 
    private int scoreForAsterroidOverlight = 50;// очки за пролет астеройда 
    private bool IsGameActive;

    public bool IsActive { get; private set; }
    
    public Action<int> AddPoint;

    public  AsterroidManager ()
    {
        EnemyCount = 10;
        CreateAsterroidPrototype();

        for (int i = 0; i < 5; i++)
        {
            AsterroidController asterroidController = CreateAsterroidFromTypePool();
            asterroidController.SetRandomStartPosition(-8, +8, 6 + i, 15 + i);// числа в параметрах ограничивают выход астеройдов за пределы области отображаемой экраном
        }

        ReturnAllToPool();

        IsActive = true;
        IsGameActive = true;
    }

    private void CreateAsterroidPrototype()
    {
        var asterroidDataPrototype = Resources.Load<AsterroidData>("Entities/Enemy/Asterroid");
        _asterroidPrototype = asterroidDataPrototype.EnemyGameObject;
        _asterroidControllerPrototype = new AsterroidController(_asterroidPrototype, asterroidDataPrototype.Health);
        _typePool = new TypePool<AsterroidController>(_asterroidControllerPrototype);
        _asterroidControllerPrototype.IsDestroy += onAsteroidDestroy;
    }
    public void Execute()
    {
        for (int i = 0; i < _asterroids.Count; i++)
        {
            if (_asterroids[i].EnemyGameObject.transform.position.y < -5)// -5 - выход астеройдов за пределы области отображаемой экраном
            {
                onAsteroidOverlight(_asterroids[i]);
            }
        }

        if (!IsGameActive)
            return;

        if (_asterroids.Count < 2)
        {
            var asterroidCreateCount = 0;
            while (_asterroids.Count < 5 && EnemyCount>0)
            {
                var asterroidController = CreateAsterroidFromTypePool();
                asterroidController.SetRandomStartPosition(-8, +8, 10 + asterroidCreateCount, 25 + asterroidCreateCount);// числа в параметрах ограничивают выход астеройдов за пределы области отображаемой экраном
                asterroidCreateCount++;
                EnemyCount--;
                Debug.Log(EnemyCount);
            }
        }
    }

    void onAsteroidDestroy(AsterroidController asterroid)
    {
         asterroid.IsDestroy -= onAsteroidDestroy;
        _asterroids.Remove(asterroid);

        if(_asterroids.Contains(asterroid))
            Debug.Log("Еще в списке");

        if (IsGameActive && !_typePool.CheckToContainsInPool(asterroid))
            AddPoint?.Invoke(200);
        else Debug.Log("Уже в пуле");
        _typePool.ReturnToPool(asterroid);
    }

    void onAsteroidOverlight(AsterroidController asterroid)
    {
        asterroid.IsDestroy -= onAsteroidDestroy;
        _asterroids.Remove(asterroid);

        if (IsGameActive && !_typePool.CheckToContainsInPool(asterroid))
            AddPoint?.Invoke(50);

        _typePool.ReturnToPool(asterroid);
    }
    public AsterroidController CreateAsterroidFromTypePool()
    {
        
        AsterroidController asterroidController = _typePool.GetFromPool();

        asterroidController.IsDestroy += onAsteroidDestroy;
        _asterroids.Add(asterroidController);
        return asterroidController;
    }

    public void OnPlayerDie()
    {
        IsGameActive = false;
    }
    public void OnRestartGame()
    {
        IsGameActive = true;
        ReturnAllToPool();
    }

    private void ReturnAllToPool()
    {
        for (int i = _asterroids.Count - 1; i >= 0; i--)
        {
            _asterroids[i].IsDestroy -= onAsteroidDestroy;
            if (!_typePool.CheckToContainsInPool(_asterroids[i]))
                _typePool.ReturnToPool(_asterroids[i]);
            _asterroids.Remove(_asterroids[i]);
        }
    }
}
