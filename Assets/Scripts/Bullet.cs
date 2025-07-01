using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;
    public Rigidbody2D rb;
    public bool impacto;
    public float damage;

    private void Start()
    {
        damage = Random.Range(10, 21);
    }

    void Update()
    {
        transform.position += transform.right * FlyWeigthPointer.Bullet.speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= FlyWeigthPointer.Bullet.lifeTime)
        {
            timer = 0f;
            BulletFactory.Instance.ReturnBullet(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        BulletFactory.Instance.ReturnBullet(this);
    }

    private void Reset()
    {
        timer = 0f;
    }

    public static void SwitchOnOff(Bullet b, bool active = true)
    {
        if (active) b.Reset();
        b.gameObject.SetActive(active);
    }
}