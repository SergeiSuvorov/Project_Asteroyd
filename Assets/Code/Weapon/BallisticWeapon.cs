using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallisticWeapon 
{
    protected float _force;
    protected ObjectPool.ObjectPool _bulletPool;

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

    protected Bullet ShootFromBarrel(Transform _barrel)
    {
        var bulletGameObject = _bulletPool.GetFromPool();
        bulletGameObject.transform.position = _barrel.position;
        bulletGameObject.transform.rotation = _barrel.rotation;
        var bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.LifeTimeIsEnd += ReturnBulletToPool;
        return bullet;
    }

    protected void ReturnBulletToPool(Bullet bullet)
    {
        bullet.LifeTimeIsEnd += null;
        _bulletPool.ReturnToPool(bullet.gameObject);
    }
}
