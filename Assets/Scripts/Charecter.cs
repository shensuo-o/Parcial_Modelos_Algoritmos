using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Charecter : MonoBehaviour
{
    public float maxlife = 100f;
    public float life;

    public Rigidbody2D rb;

    private Queue<float> lifeChanges = new Queue<float>();

    private void Start()
    {
        life = maxlife;
    }

    public void TakeDamage(float amount)
    {
        life -= amount;
        life = Mathf.Max(life, 0);
        lifeChanges.Enqueue(life);

        if (life <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void LookTarget(Vector3 lookAtDirection)
    {
        float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public IEnumerable<float> GetLifeChanges() // Generator usador para que tener al tanto la vida del jugador
    {
        //Where para filtrar solo los cambios donde la vida es menor o igual al 20% de la vida máxima
        foreach (var lifeChange in lifeChanges.Where(lifeValue => lifeValue <= maxlife * 0.20f))
        {
            yield return lifeChange;
        }
    }
}

