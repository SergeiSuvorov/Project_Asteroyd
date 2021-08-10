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

    /// <summary>
    /// Ильзуется для создания ObjectPoolа на основе GameObjecta сделанного с помощью строителя
    /// </summary>
    /// <param name="bulletPrefab"></param>
    protected void CreateObjectPoolWithBuilder()
    {

        var bulletSprite = Resources.Load<Sprite>("Sprite/Bullet");
        var bulletGameOject = new GameObject().SetName("Bullet")
                                           .AddSprite(bulletSprite)
                                           .AddRigidbody2D(1)
                                           .AddCircleCollider2D()
                                           .GetOrAddComponent<Bullet>().gameObject;

        _bulletPool = new ObjectPool.ObjectPool(bulletGameOject);
        // Передаем пул в сервис локатор
        ServiceLocator.SetService(_bulletPool);

        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < 12; i++)
        {
            var bullet = _bulletPool.GetFromPool();
            bullets.Add(bullet);
        }
        for (int i = 0; i < 12; i++)
        {
            _bulletPool.ReturnToPool(bullets[i]);
        }
    }

    /// <summary>
    /// Ильзуется для создания TypePool на основе GameObjecta сделанного с помощью строителя
    /// </summary>
    /// <param name="bulletGameOject"></param>
    protected void CreateTypePoolWhithBuilder()
    {
        var bulletSprite = Resources.Load<Sprite>("Sprite/Bullet");
        var bulletGameOject = new GameObject().SetName("Bullet")
                                           .AddSprite(bulletSprite)
                                           .AddRigidbody2D(1)
                                           .AddCircleCollider2D()
                                           .GetOrAddComponent<BulletView>().gameObject;

        var bulletController = new BulletController(bulletGameOject);
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
    /// Метод передающий Bulet координаты точки спауна пули, использует ObjectPool
    /// </summary>
    /// <param name="_barrel"></param>
    /// <returns></returns>
    protected Bullet ShootFromBarrel(Transform _barrel)
    {
        var bulletGameObject = _bulletPool.GetFromPool();
        bulletGameObject.transform.position = _barrel.position;
        bulletGameObject.transform.rotation = _barrel.rotation;
        var bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.LifeTimeIsEnd += ReturnBulletToPool;
        return bullet;
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
    /// Возвращает пулю в ObjectPool
    /// </summary>
    /// <param name="bullet"></param>
    protected void ReturnBulletToPool(Bullet bullet)
    {
        bullet.LifeTimeIsEnd += null;
        _bulletPool.ReturnToPool(bullet.gameObject);
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
        bullet.BulletLifeIsEnd += null;
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
