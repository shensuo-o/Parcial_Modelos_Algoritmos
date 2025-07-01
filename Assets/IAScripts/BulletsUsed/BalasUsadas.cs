using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalasUsadas : MonoBehaviour
{
    public List<GameObject> balasUsadas;

    public void AgregarBala(GameObject bala)
    {
        balasUsadas.Add(bala);
    }
}
