using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : IAttack
{
    Transform _bulletSpawner;
    float _cooldown;
    bool _canShoot;

    public ShootAttack(Transform bulletSpawner, float cooldown, bool canShoot = true)
    {
        _bulletSpawner = bulletSpawner;
        _cooldown = cooldown;
        _canShoot = canShoot;
    }
    
    public void Attack()
    {

            var b = BulletFactory.Instance.pool.GetObject();

            if (!b)
            {
                return;
            }

            b.transform.SetPositionAndRotation(_bulletSpawner.position, _bulletSpawner.rotation);
        
        //StartCoroutine(Shoot());
    }

    public void Update() { }

    /*public IEnumerator Shoot()
    {
        _canShoot = false;
        Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
        yield return new WaitForSeconds(_cooldown);
        _canShoot = true;
    }*/
}
