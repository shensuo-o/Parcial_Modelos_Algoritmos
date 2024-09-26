using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float bulletDamage;
    static public float pjDMG = 5;
    public Rigidbody2D rb;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        Destroy(gameObject, lifeTime);
    }

}
