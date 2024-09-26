using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    public float speed = 7f;
    public float maxlife = 100f;
    public float life;
    public GameObject bulletPrefab;

    public void Fire(Transform SpawnBullet)
    {
        if (SpawnBullet != null)
        {
            GameObject.Instantiate(bulletPrefab, SpawnBullet.position, SpawnBullet.rotation);
        }
    }

    public void TakeDamage(float amount)
    {
        life -= amount;

        if (life <= 0)
        {
            life = 0;
        }
    }

    public void Death()
    {
        if(life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
