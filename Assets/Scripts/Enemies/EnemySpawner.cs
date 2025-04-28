using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public float timer;
    public float spawnTime;

    public Transform[] spawnPoints;

    public void StartSpawningEnemies(int cantidad)
    {
        StartCoroutine(SpawnMultipleEnemiesSmoothly(cantidad));
    }

    // Corutina para Time-Slicing en el spawneo de enemigos
    private IEnumerator SpawnMultipleEnemiesSmoothly(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            // Usamos Any() para verificar si hay al menos 10 enemigos activos. Usamos Take() para limitar la cantidad de objectos de la pool en 10
            if (EnemyFactory.Instance.pool.GetObjects(10).Take(10).Any(e => e != null))
            {
                Debug.Log("Ya hay 10 enemigos activos. No se generarán más.");
                yield break;
            }
            var b = new { enemy = EnemyFactory.Instance.pool.GetObject() };

            int x = Random.Range(0, spawnPoints.Length);

            b.enemy.transform.SetPositionAndRotation(spawnPoints[x].position, spawnPoints[x].rotation);

            yield return new WaitForSeconds(spawnTime);

            Debug.Log($"Enemigo #{i + 1} spawneado");
        }
    }

    void Update() // Anónimo
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            var b = new { enemy = EnemyFactory.Instance.pool.GetObject() };

            int x = Random.Range(0, spawnPoints.Length);

            b.enemy.transform.SetPositionAndRotation(spawnPoints[x].position, spawnPoints[x].rotation);

            timer = 0;
        }
    }
}




