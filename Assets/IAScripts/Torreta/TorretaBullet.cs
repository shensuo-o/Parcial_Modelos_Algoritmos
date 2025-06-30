using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaBullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float timer;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            TorretaManager.instance.SumarBalasMuertas(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Charecter>().life -= damage;
            TorretaManager.instance.SumarBalasImpactadas(this.gameObject);
            this.gameObject.SetActive(false);
        }
        else
        {
            TorretaManager.instance.SumarBalasMuertas(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
