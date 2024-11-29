using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float timer;
    public Rigidbody2D rb;

    void Update()
    {
        transform.position += transform.right * FlyWeigthPointer.Bullet.speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= FlyWeigthPointer.Bullet.lifeTime)
        {
            Debug.Log("Volvi al pool");
            timer = 0f;
            EnemyBulletFactory.Instance.ReturnBullet(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(FlyWeigthPointer.Bullet.bulletDMG);
        }

        EnemyBulletFactory.Instance.ReturnBullet(this);
    }

    private void Reset()
    {
        timer = 0f;
    }

    public static void SwitchOnOff(EnemyBullet b, bool active = true)
    {
        if (active) b.Reset();
        b.gameObject.SetActive(active);
    }
}
