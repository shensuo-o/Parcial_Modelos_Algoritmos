using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;
    public Rigidbody2D rb;
    public bool impacto;
    public float damage;
    public float timeFired;

    private void Start()
    {
        float baseDamage = Random.Range(10, 21);
        damage = baseDamage + PlayerBonus.damageExtra;
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
            collision.gameObject.GetComponent<IAEnemy>().TakeDamage(damage);
            impacto = true;
        }
        else
        {
            impacto = false;
        }

        timeFired = Time.time;
        BalasUsadas.instance.AgregarBala(this.gameObject);
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