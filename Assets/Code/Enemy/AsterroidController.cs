using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UniRx;
using UnityEngine;


namespace Asteroids
{
    [Serializable]
    public class AsterroidController : EnemyController,ITypePoolObject
    {
        [SerializeField] protected AsterroidView _asterroidView;
        [SerializeField] private GameObject _poolGameObject;
        [SerializeField] protected int _gameObjectCloneCount;
        [SerializeField] protected float _healthMax;
        public Action < AsterroidController > IsDestroy;
        
        public AsterroidController(GameObject gameObject, Health health) : base(gameObject, health)
        {
            _healthMax =_health.Max;
            _poolGameObject = new GameObject();
            _poolGameObject.name = "Asterroid Pool";

            UpdateAsterroidView();
        }
       
        public void  UpdateAsterroidView()
        {
           
            _enemyGameObject = UnityEngine.Object.Instantiate(_enemyGameObject);
            _enemyGameObject.name = "Asterroir" + UnityEngine.Random.Range(1,10);
            _gameObjectCloneCount++;
            if (_enemyGameObject.GetComponent<AsterroidView>() == null)
                throw new ArgumentException("AsterroidPrefab must be have a AsterroidView");

            _asterroidView = _enemyGameObject.GetComponent<AsterroidView>();

        }
        public void UpdateHealth(float healthValue)
        {
            _health.ChangeCurrentHealth(healthValue);
        }

        public void SetRandomStartPosition(float xMin, float xMax, float yMin, float yMax)
        {
            float xEnemyPosition = UnityEngine.Random.Range(xMin, xMax);
            float yEnemyPosition = UnityEngine.Random.Range(yMin, yMax);

            _enemyGameObject.transform.position = new Vector3(xEnemyPosition, yEnemyPosition);
        }

        public void ExecuteBeforReturnToPool()
        {
            _asterroidView.GettingDamage -= GetDamage;
            _enemyGameObject.transform.parent = _poolGameObject.transform;
            _enemyGameObject.SetActive(false);
           
        }

        public void ExecuteAfterGetToPool()
        {
            _asterroidView.GettingDamage += GetDamage;
            _enemyGameObject.SetActive(true);
            _enemyGameObject.transform.parent = null;
            UpdateHealth(_healthMax);
            Debug.Log("Здоровья в объекте" + _enemyGameObject.name + " " + _health.Current);
        }

        public void ExecuteAfterDeepCopy()
        {
            Debug.Log("clone "+_gameObjectCloneCount);
            Debug.Log("health "+ _healthMax);
            UpdateAsterroidView();

            if (_poolGameObject == null)
            {
                _poolGameObject = new GameObject();
                _poolGameObject.name = "Asterroid Pool";
            }
        }

        public override void GetDamage(float damage)
        {
            Debug.Log("Попали в объект" + _enemyGameObject.name + " " + _health.Current);
            _health.Damage(damage);
            Debug.Log("Результат попадания в " + _enemyGameObject.name + " " + _health.Current);
            if (_health.Current <= 0.0f)
            {
                IsDestroy?.Invoke(this);
                SendMessageAboutDestroy();
            }
        }

        private void SendMessageAboutDestroy()
        {
            MessageBroker.Default
            .Publish(MessageBase.Create(
                        _asterroidView, // sender MonoBehaviour
                         ServiceShareData.MSG_DESTROY, // message id
                        "was Destroed!" // data System.Ojbect
                        ));
        }
        public override void SetHealthAid(float healthAid)
        {
            _health.SetHealthAid(healthAid);
        }
    }

   
}

  
