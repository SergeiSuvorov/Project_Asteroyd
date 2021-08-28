using Asteroids;
using Asteroids.Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.ServiceLocator;

public abstract class BallisticWeapon 
{
    protected float _force;
    protected ObjectPool.ObjectPool _bulletPool;
    protected TypePool<BulletController> _typePool;

    /// <summary>
    /// Ильзуется для создания ObjectPoolа на основе префаба
    /// </summary>
    /// <param name="bulletPrefab"></param>
    protected void CreateBulletPool(GameObject bulletPrefab)
    {
        _bulletPool = new ObjectPool.ObjectPool(bulletPrefab);

        List<GameObject> bullets = new List<GameObject>();

        for (int i=0; i<30; i++)
        {
            var bullet = _bulletPool.GetFromPool();
            bullets.Add(bullet);
        }
        for (int i = 0; i < 30; i++)
        {
            _bulletPool.ReturnToPool(bullets[i]);
        }
    }

    protected void CreateBulletTypePoolWhithBuilder(AmmoData ammoData)
    {
        var bulletController = new BulletController(ammoData);
        _typePool = new TypePool<BulletController>(bulletController);
        _typePool.ReturnToPool(bulletController);

        // Передаем пул в сервис локатор
        ServiceLocator.SetService(_typePool);

        List<BulletController> bullets = new List<BulletController>();
        for (int i = 0; i < 12; i++)
        {
            var bullet = _typePool.GetFromPool();
            bullets.Add(bullet);
        }
        for (int i = 0; i < 12; i++)
        {
            _typePool.ReturnToPool(bullets[i]);
        }
    }

    /// <summary>
    /// Метод вызывающий BuletController из пула и передающий команду на выстрел, использует TypePool
    /// </summary>
    protected void ShootFromBarrel(Transform barrel, Vector2 direction)
    {
        var bulletController = CreateBulletFromTypePool();
        bulletController.Shooting(barrel,direction,_force);
    }

    /// <summary>
    /// Возвращает пулю в TypePool
    /// </summary> 
    protected void ReturnBulletToPool(BulletController bullet)
    {
        bullet.BulletLifeIsEnd += null;
        _typePool.ReturnToPool(bullet);
    }

    /// <summary>
    /// Событие происходящее по уничтожению пули
    /// </summary>
    private void onBulletDestroyPool(BulletController bullet)
    {
        bullet.BulletLifeIsEnd -= onBulletDestroyPool;
        _typePool.ReturnToPool(bullet);
    }

    /// <summary>
    /// Метод вызывающий BuletController из пула 
    /// </summary>
    public BulletController CreateBulletFromTypePool()
    {
        BulletController bulletController = _typePool.GetFromPool();
        bulletController.BulletLifeIsEnd += onBulletDestroyPool;
        return bulletController;
    }
}
