using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public float timer;
    public Rigidbody2D rb;

    private static List<Bullet> activeBullets = new List<Bullet>();

    //Time-slicing para poder controlar el movimientod y vida de la bullet//Iñaki
    private IEnumerator Start()
    {
        activeBullets.Add(this);
        GetBulletsOrderedByTimer();

        while (timer < FlyWeigthPointer.Bullet.lifeTime)
        {
            transform.position += transform.right * FlyWeigthPointer.Bullet.speed * Time.deltaTime;
            timer += Time.deltaTime;

            yield return null;
        }

        Debug.Log("Volvi al pool");
        activeBullets.Remove(this);
        BulletFactory.Instance.ReturnBullet(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(FlyWeigthPointer.Bullet.bulletDMG);
        }

        activeBullets.Remove(this);
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

    // OrderByDEscending para ordenar las Bullet
    public static List<Bullet> GetBulletsOrderedByTimer()
    {
        return activeBullets.Where(x => x.timer <= 4).OrderByDescending(b => b.timer).ToList();//Iñaki
    }
}

