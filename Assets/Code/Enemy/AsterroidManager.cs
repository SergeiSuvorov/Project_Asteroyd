using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Prototype;
using System;
using Asteroids.Visitor;

public class AsterroidManager : EnemyManager, IExecute
{
    private GameObject _asterroidPrototype;
    private List<AsterroidController> _asterroidControllersPool = new List<AsterroidController>();
    private TypePool<AsterroidController> _typePool;
    private List<EnemyController> _asterroids =new List<EnemyController>();
    private AsterroidController _asterroidControllerPrototype;
    private int _scoreForAsterroidDestroy = 200;// очки за уничтожение астеройда 
    private int _scoreForAsterroidOverlight = 50;// очки за пролет астеройда 
    private bool IsGameActive;
    private ListenerDestroyEnemy listenerDestroyEnemy = new ListenerDestroyEnemy();

    public bool IsActive { get; private set; }
    
    public Action<int> AddPoint;

    public  AsterroidManager ()
    {
        EnemyCount = 10;// используется для проверки 
        CreateAsterroidPrototype();

     
        for (int i = 0; i < 5; i++)
        {
            AsterroidController asterroidController = CreateAsterroidFromTypePool();
        }

        ReturnAllToPool();

        IsActive = true;
        IsGameActive = true;
    }

    private void CreateAsterroidPrototype()
    {
        var asterroidDataPrototype = Resources.Load<AsterroidData>("Entities/Enemy/Asterroid");
        _asterroidPrototype = asterroidDataPrototype.EnemyGameObject;
        _asterroidControllerPrototype = new AsterroidController(_asterroidPrototype, asterroidDataPrototype);
        _typePool = new TypePool<AsterroidController>(_asterroidControllerPrototype);
        _asterroidControllerPrototype.IsDestroy += onAsteroidDestroy;
        //_typePool.ReturnToPool(_asterroidControllerPrototype as AsterroidController);
        _asterroids.Add(_asterroidControllerPrototype);
        listenerDestroyEnemy.Add(_asterroidControllerPrototype);
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

        if (_asterroids.Count < 5)
        {
            var asterroidCreateCount = 0;
            while (_asterroids.Count < 5 && EnemyCount>0)
            {
                var asterroidController = CreateAsterroidFromTypePool();
                asterroidController.SetRandomStartPosition(-8, +8, 10 + asterroidCreateCount, 25 + asterroidCreateCount);// числа в параметрах ограничивают выход астеройдов за пределы области отображаемой экраном
                asterroidCreateCount++;
                EnemyCount--;
            }
        }
    }

    void onAsteroidDestroy(EnemyController asterroid)
    {
        (asterroid as AsterroidController).IsDestroy -= onAsteroidDestroy;
        listenerDestroyEnemy.Remove(asterroid as AsterroidController);
        _asterroids.Remove(asterroid);

        if(_asterroids.Contains(asterroid))
            Debug.Log("Еще в списке");

        if (IsGameActive && !_typePool.CheckToContainsInPool(asterroid as AsterroidController))
            AddPoint?.Invoke(_scoreForAsterroidDestroy);
        else Debug.Log("Уже в пуле");
        _typePool.ReturnToPool(asterroid as AsterroidController);
    }

    void onAsteroidOverlight(EnemyController asterroid)
    {
        (asterroid as AsterroidController).IsDestroy -= onAsteroidDestroy;
        listenerDestroyEnemy.Remove(asterroid as AsterroidController);
        _asterroids.Remove(asterroid);

        if (IsGameActive && !_typePool.CheckToContainsInPool((asterroid as AsterroidController)))
            AddPoint?.Invoke(_scoreForAsterroidOverlight);

        _typePool.ReturnToPool((asterroid as AsterroidController));
    }

    public AsterroidController CreateAsterroidFromTypePool()
    {
        
        AsterroidController asterroidController = _typePool.GetFromPool();

        asterroidController.IsDestroy += onAsteroidDestroy;
        if (_asterroids.Contains(asterroidController))
            Debug.Log("Уже в списке");
        Debug.Log(asterroidController.EnemyGameObject.name + " создан");

        _asterroids.Add(asterroidController);
        listenerDestroyEnemy.Add(asterroidController);
        asterroidController.Activate(new ActivateEnemyAtScene());
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
            (_asterroids[i] as AsterroidController).IsDestroy -= onAsteroidDestroy;
            if (!_typePool.CheckToContainsInPool(_asterroids[i] as AsterroidController))
                _typePool.ReturnToPool(_asterroids[i] as AsterroidController);
            listenerDestroyEnemy.Remove(_asterroids[i] as AsterroidController);
            _asterroids.Remove(_asterroids[i]);
        }
    }
}
