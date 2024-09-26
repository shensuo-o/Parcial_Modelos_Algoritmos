using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Charecter : MonoBehaviour
{
    public float speed;
    public float maxlife = 100f;
    public float life;

    public Rigidbody2D rb;

    public void Movement(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
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

    public void LookTarget(Vector3 lookAtDirection)
    {
        
        float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
