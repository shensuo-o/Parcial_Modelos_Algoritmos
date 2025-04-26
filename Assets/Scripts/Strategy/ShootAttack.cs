using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : IAttack
{
    Transform _bulletSpawner;
    float _cooldown;
    bool _canShoot;
    float _cooldownTimer;

    public ShootAttack(Transform bulletSpawner, float cooldown, bool canShoot = true)
    {
        _bulletSpawner = bulletSpawner;
        _cooldown = cooldown;
        _canShoot = canShoot;
    }
    
    public void Attack()
    {
        if (_canShoot)
        {
            var b = new { bullet = EnemyBulletFactory.Instance.pool.GetObject() };
            if (!b.bullet)
            {
                return;
            }

            b.bullet.transform.SetPositionAndRotation(_bulletSpawner.position, _bulletSpawner.rotation);
            _canShoot = false;
        }
    }

    public void Update() 
    {
        if (!_canShoot)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _cooldown) ResetCooldown();
        }
    }

    private void ResetCooldown()
    {
        Debug.Log("Reseteo Cooldown");
        _cooldownTimer = 0f;
        _canShoot = true;
    }
}
