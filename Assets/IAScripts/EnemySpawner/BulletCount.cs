using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    public static BulletCount instance;

    private int peligro = 0;

     public void CalcularPeligroPorLayer()
    {
        GameObject[] todos = FindObjectsOfType<GameObject>();

        var objetosFiltrados = todos.Where(obj =>
            obj.layer == 6 ||
            obj.layer == 7 ||
            obj.layer == 10 ||
            obj.layer == 11
        ).ToList();

        int totalInstanciados = objetosFiltrados.Aggregate(0, (acum, obj) => acum + 1);

        var ordenados = objetosFiltrados
            .OrderBy(obj => obj.layer == 6 ? 0 : 1) 
            .ThenBy(obj => obj.layer)               
            .ToList();

        peligro = 0;

        foreach (var obj in ordenados)
        {
            switch (obj.layer)
            {
                case 6:
                    peligro += 2;
                    break;

                case 10:
                case 11:
                    peligro += 1;
                    break;

                case 7:
                    peligro -= 1;
                    break;
            }
        }

        Debug.Log("Nivel de peligro Final del Nivel: {peligro} (Total objetos: {totalInstanciados})");
    }
}

