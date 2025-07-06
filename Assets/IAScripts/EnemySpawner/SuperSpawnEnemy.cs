using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperSpawnEnemy : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public int cantidadEnemigos = 10;
    public float delayEntreSpawns = 0.2f;

    [Header("Puntos de Spawn")]
    public Transform[] puntosDeSpawn = new Transform[4];

    private List<GameObject> enemigosGenerados = new();

    private (int vida, int daño, float velocidad)[] statsFiltrados;

    void Start()
    {
        statsFiltrados = IAEnemy.GenerarStats(cantidadEnemigos * 2)//Angelo LINQ
            .Where(s => s.velocidad > 3f)
            .OrderBy(s => s.vida)
            .Take(cantidadEnemigos)
            .ToArray();

        StartCoroutine(SpawnEnemigosCoroutine());
    }

    private IEnumerable<(int index, int tagAleatorio)> GenerarEnemigos(int cantidad)//Angelo Generator
    {
        for (int i = 0; i < cantidad; i++)
        {
            int tagAleatorio = Random.Range(1, 3);
            yield return (i, tagAleatorio);
        }
    }

    private IEnumerator SpawnEnemigosCoroutine()//Angelo Time Slicing
    {
        var enemigosDatos = GenerarEnemigos(cantidadEnemigos)
            .Where(e => e.tagAleatorio == 1 || e.tagAleatorio == 2)
            .OrderBy(e => e.tagAleatorio)
            .ToList();

        var enemigosConSpawn = enemigosDatos.Select(e =>
        {
            int spawnIndex = Random.Range(0, puntosDeSpawn.Length);
            return new
            {
                index = e.index,
                tag = e.tagAleatorio,
                spawnIndex = spawnIndex
            };
        }).OrderByDescending(e => e.spawnIndex)
          .ToArray();

        foreach (var e in enemigosConSpawn)
        {
            Transform punto = puntosDeSpawn[e.spawnIndex];

            GameObject nuevoEnemigo = Instantiate(enemyPrefab, punto.position, Quaternion.identity);
            enemigosGenerados.Add(nuevoEnemigo);

            IAEnemy enemyIA = nuevoEnemigo.GetComponent<IAEnemy>();
            if (enemyIA != null)
            {
                enemyIA.Tag = e.tag;

                if (e.index < statsFiltrados.Length)
                {
                    var stat = statsFiltrados[e.index];
                    enemyIA.SetStats(stat.vida, stat.daño, stat.velocidad);
                }
            }

            Debug.Log($"Enemy {e.index} tipo {e.tag} spawneado en punto {e.spawnIndex}");

            yield return new WaitForSeconds(delayEntreSpawns);
        }

        Debug.Log("Todos los enemigos fueron spawneados.");
    }
}