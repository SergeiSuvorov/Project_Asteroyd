﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletController :IExecute, ITypePoolObject
{
    [SerializeField] private Rigidbody2D _bulletRigidbody;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private GameObject _bulletGameObject;
    [SerializeField] private GameObject _poolGameObject;
    [SerializeField] private BulletModel _bulletModel;
    private Vector2 _shootDirection;
    private float _shootStartForce;
    private float _lifeTime = 3f; // время жизни пули - за это время она успевает вылететь за прeделы экрана

    public Action<BulletController> BulletLifeIsEnd;

    public bool IsActive { get; private set; }

    public BulletController(GameObject bulletGameObject)
    {
        _bulletGameObject = bulletGameObject;
        _poolGameObject = new GameObject();
        _poolGameObject.name = "Bullet Pool";

        UpdateBullet();
    }

    public BulletController(AmmoData ammoData)
    {
        _bulletGameObject = UnityEngine.Object.Instantiate(ammoData.AmmoGO);
        _poolGameObject = new GameObject();
        _poolGameObject.name = "Bullet Pool";
        _bulletModel = new BulletModel(ammoData);

        UpdateBullet();
    }

    public void Shooting(Transform barrel, Vector2 direction, float shootStartForce)
    {
        _bulletGameObject.transform.position = barrel.position;
        _bulletGameObject.transform.rotation = barrel.rotation;
        _shootDirection = direction;
        _shootStartForce = shootStartForce;
        _lifeTime = 3;
    }

  
   public void onCollision(GameObject collision)
   {
        Debug.Log(collision.gameObject.name);
        
            if (collision.activeSelf && collision.GetComponent<IHealth>() != null)
                collision.gameObject.GetComponent<IHealth>().GetDamage(_bulletModel.Damage);

        DeactiveBullet();
   }
   public void DeactiveBullet()
   {
       
        _lifeTime = 0;
        _bulletView.InCollision -= onCollision;
        //_bulletView.InCollision -= DeactiveBullet;
        BulletLifeIsEnd?.Invoke(this);
   }

    public void Execute()
    {
        _lifeTime -= Time.deltaTime;
        _bulletRigidbody.velocity = _shootDirection * _shootStartForce;

        if (_lifeTime < 0)
        {
            Debug.Log(_bulletGameObject.name);
            DeactiveBullet();
        }
        
    }

    private void UpdateBullet()
    {
        if (_bulletGameObject.GetComponent<BulletView>() == null)
            throw new ArgumentException("BulletPrefab must be have a BulletView");

        _bulletRigidbody = _bulletGameObject.GetComponent<Rigidbody2D>();
        _bulletView = _bulletGameObject.GetComponent<BulletView>();
    }
    public void ExecuteBeforReturnToPool()
    {
        _lifeTime = 0;
        _bulletRigidbody.velocity = Vector2.zero;
        _shootStartForce = 0;
        
        _bulletGameObject.transform.parent = _poolGameObject.transform;
        //Debug.Log(_bulletGameObject.transform.parent.name);
        _bulletView.gameObject.SetActive(false);
        IsActive = false;
    }

    public void ExecuteAfterGetToPool()
    {
        _bulletGameObject.transform.parent = null;
        _bulletView.gameObject.SetActive(true);
        _bulletView.InCollision += onCollision;
        //_bulletView.InCollision -= DeactiveBullet;
        IsActive = true;
        Debug.Log(_bulletModel.Damage);
    }

    public void ExecuteAfterDeepCopy()
    {
        Debug.Log(_bulletModel.Damage);
        _bulletGameObject = UnityEngine.Object.Instantiate(_bulletGameObject);
        UpdateBullet();

        _bulletView.InCollision -= onCollision;
        //_bulletView.InCollision -= DeactiveBullet;

        if (_poolGameObject==null)
        {
            _poolGameObject = new GameObject();
            _poolGameObject.name = "Bullet Pool";
        }
    }


}
