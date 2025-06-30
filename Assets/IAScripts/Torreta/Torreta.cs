using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public bool canShoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject superBullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int id;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        int count = 0;

        while (canShoot)
        {
            count++;
            GameObject proyectile = BulletGenerator().Take(count).Last();
            Instantiate(proyectile, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerable <GameObject> BulletGenerator()
    {
        int count = 0;
        while (true)
        {
            count++;
            if (count % 3 == 0)
            {
                yield return superBullet;
            }
            else
            {
                yield return bullet;
            }
        }
    }
}
