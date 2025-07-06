using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    private int peligro = 0;
    private int totalInstanciados = 0;

    public void CalcularPeligroPorLayer()
    {
        GameObject[] todos = FindObjectsOfType<GameObject>();

        var objetosFiltrados = todos//Iñaki LinQ
            .Where(obj =>
                obj.layer == 6 ||
                obj.layer == 7 ||
                obj.layer == 10 ||
                obj.layer == 11
            )
            .OrderByDescending(obj => obj.layer)
            .ToList();

        totalInstanciados++;

        var ordenados = objetosFiltrados
            .OrderBy(obj => obj.layer == 6 ? 0 : 1) 
            .ThenBy(obj => obj.layer)               
            .ToList();

        peligro = ordenados.Aggregate(0, (a, obj) => //Manu Aggregate
        {
            switch (obj.layer)
            {
                case 6:
                    return a + 2;
                case 10:
                case 11:
                    return a + 1;
                case 7:
                    return a - 1;
                default:
                    return a;
            }
        });

        Debug.Log($"Nivel de peligro Final del Nivel: {peligro} (Total objetos: {totalInstanciados})");
    }
}

