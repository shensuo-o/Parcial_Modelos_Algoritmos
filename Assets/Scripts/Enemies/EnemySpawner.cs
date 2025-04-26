using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timer;
    public float spawnTime;

    public Transform[] spawnPoints;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            var b = new {enemy = EnemyFactory.Instance.pool.GetObject()};

            int x = Random.Range(0, spawnPoints.Length);

            b.enemy.transform.SetPositionAndRotation(spawnPoints[x].position, spawnPoints[x].rotation);

            timer = 0;
        }
    }
}
