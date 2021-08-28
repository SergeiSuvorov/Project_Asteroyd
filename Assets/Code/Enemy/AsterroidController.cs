using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Visitor;
using ObjectPool;
using UniRx;
using UnityEngine;


namespace Asteroids
{
    [Serializable]
    public class AsterroidController : EnemyController,ITypePoolObject, IDestroy, IActivate
    {
        [SerializeField] protected AsterroidView _asterroidView;
        [SerializeField] protected AsterroidModel _asterroidModel;
        [SerializeField] private GameObject _poolGameObject;
        [SerializeField] protected int _gameObjectCloneCount;
        public event Action <EnemyController> IsDestroy;
        
        public AsterroidController(GameObject gameObject, AsterroidData asterroidData) : base(gameObject)
        {
            _asterroidModel = new AsterroidModel(asterroidData);
            _poolGameObject = new GameObject();
            _poolGameObject.name = "Asterroid Pool";
            UpdateAsterroidView();
        }

        public void onCollision(GameObject collision)
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log(_asterroidModel.Damage);
            if (collision.activeSelf && collision.GetComponent<IHealth>() != null)
                collision.gameObject.GetComponent<IHealth>().GetDamage(_asterroidModel.Damage);
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
            _asterroidModel.Health.ChangeCurrentHealth(healthValue);
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
            _asterroidView.InCollision -= onCollision;
            _enemyGameObject.transform.parent = _poolGameObject.transform;
            _enemyGameObject.SetActive(false);
        }

        public void ExecuteAfterGetToPool()
        {
            _asterroidView.GettingDamage += GetDamage;
            _asterroidView.InCollision += onCollision;
            _enemyGameObject.SetActive(true);
            _enemyGameObject.transform.parent = null;
            UpdateHealth(_asterroidModel.Health.Max);
        }

        public void ExecuteAfterDeepCopy()
        {
            //Debug.Log("health " + _asterroidModel.Health.Max);
            UpdateAsterroidView();

            if (_poolGameObject == null)
            {
                _poolGameObject = new GameObject();
                _poolGameObject.name = "Asterroid Pool";
            }
        }

        public override void GetDamage(float damage)
        {
            Debug.Log("Damage " + damage);
            Debug.Log("Before " + _asterroidModel.Health.Current);
            _asterroidModel.Health.Damage(damage);
            Debug.Log("After " + _asterroidModel.Health.Current);
            if (_asterroidModel.Health.Current <= 0.0f)
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
            _asterroidModel.Health.SetHealthAid(healthAid);
        }

        public void Activate(IActivateAtScene value)
        {
            value.ActivateAtScene(this);
        }
    }

   
}

  
