using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float timer;
    public float bulletDamage;
    static public float pjDMG = 5;
    public Rigidbody2D rb;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Debug.Log("Volvi al pool");
            timer = 0f;
            BulletFactory.Instance.ReturnBullet(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(pjDMG);
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
