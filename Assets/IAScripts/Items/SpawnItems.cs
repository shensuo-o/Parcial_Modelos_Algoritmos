using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnItems : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private int maxItemsASpawnear = 20;

    [Header("Prefabs de ítems disponibles")]
    public List<GameObject> itemsDisponibles = new();

    [Header("Puntos de spawn")]
    public Transform[] spawnPoints;

    private List<GameObject> itemsAInstanciar = new();

    private void Start()
    {
        int cantidad = Random.Range(10, maxItemsASpawnear + 1);
        itemsAInstanciar = GenerarItems(cantidad).ToList(); //LINQ Grupo 3

        StartCoroutine(SpawnItemsConTiempo());
    }

    private IEnumerable<GameObject> GenerarItems(int total)//Manu generator
    {
        if (itemsDisponibles.Count == 0) yield break;

        //LINQ Grupo 1
        var filtrados = itemsDisponibles.Where(item => item != null).ToList();//LINQ Grupo 1

        for (int i = 0; i < total; i++)
        {
            int randomIndex = Random.Range(0, filtrados.Count);
            yield return filtrados[randomIndex];
        }
    }



    private IEnumerator SpawnItemsConTiempo()//Manu Time-Slicing
    {
        foreach (var itemPrefab in itemsAInstanciar)
        {
            Transform spawnPoint = spawnPoints.OrderByDescending(p => Random.value).First(); //LINQ Grupo 2

            Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(10f);
        }
    }
}

