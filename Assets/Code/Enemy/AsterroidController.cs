using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;


namespace Asteroids
{
    [Serializable]
    public class AsterroidController : EnemyController,ITypePoolObject
    {
        [SerializeField] protected AsterroidView _asterroidView;
        [SerializeField] private GameObject _poolGameObject;
        public Action < AsterroidController > IsDestroy;
        

       
        public AsterroidController(GameObject gameObject, Health health) : base(gameObject, health)
        {
            _poolGameObject = new GameObject();
            _poolGameObject.name = "Asterroid Pool";

            UpdateAsterroidView();
        }
       
        public void  UpdateAsterroidView()
        {
           
            _enemyGameObject = UnityEngine.Object.Instantiate(_enemyGameObject);

            if (_enemyGameObject.GetComponent<AsterroidView>() == null)
                throw new ArgumentException("AsterroidPrefab must be have a AsterroidView");

            _asterroidView = _enemyGameObject.GetComponent<AsterroidView>();
            _asterroidView.GettingDamage += GetDamage;

        }
        public void UpdateHealth(float healthValue)
        {
            _health.ChangeCurrentHealth(healthValue);
        }
        public void CheckField()
        {
            Debug.Log(_asterroidView != null);
            Debug.Log(_enemyGameObject!= null);
            Debug.Log(_health!= null);
        }
        

        public void SetRandomStartPosition(float xMin, float xMax, float yMin, float yMax)
        {
            //CheckField();
            float xEnemyPosition = UnityEngine.Random.Range(xMin, xMax);
            float yEnemyPosition = UnityEngine.Random.Range(yMin, yMax);

            _enemyGameObject.transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
        }

        public void ExecuteBeforReturnToPool()
        {
            _enemyGameObject.transform.parent = _poolGameObject.transform;
            _enemyGameObject.SetActive(false);
           
        }

        public void ExecuteAfterGetToPool()
        {
            _asterroidView.GettingDamage += GetDamage;
            _enemyGameObject.SetActive(true);
            _enemyGameObject.transform.parent = null;
        }

        public void ExecuteAfterDeepCopy()
        {
            UpdateAsterroidView();

            if (_poolGameObject == null)
            {
                _poolGameObject = new GameObject();
                _poolGameObject.name = "Asterroid Pool";
            }
        }

        public override void GetDamage(float damage)
        {
            if (_health.Current <= 0.0f)
            {
                IsDestroy?.Invoke(this);
            }
        }

        public override void SetHealthAid(float healthAid)
        {
            _health.SetHealthAid(healthAid);
        }
    }

   
}

  
