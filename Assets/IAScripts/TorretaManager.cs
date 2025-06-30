using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorretaManager : MonoBehaviour
{
    public List<GameObject> balasQueImpactaronPlayer = new();
    public List<GameObject> balasQueMurieron = new();

    private List<(GameObject bala, string tipo)> balasImpacto = new();
    private List<(GameObject bala, string tipo, float tiempoInstancia)> balasMuertasConTiempo = new();

    private int totalImpacto = 0;
    private int totalMuertas = 0;

    void Update()
    {
        BalasImpactadas();
        BalasMuertas();

        OrdenarImpactadas();
        OrdenarMuertas();
    }

    void BalasImpactadas()
    {
        balasImpacto = balasQueImpactaronPlayer.Select(bala =>
        (
            bala,
            bala.name.Contains("Especial") ? "Especial" : "Normal"
        )).ToList();

        totalImpacto = balasImpacto.Aggregate(0, (acum, bala) => acum + 1);
    }

    void BalasMuertas()
    {
        balasMuertasConTiempo = balasQueMurieron.Select(bala =>
        (
            bala,
            bala.name.Contains("Especial") ? "Especial" : "Normal",
            Time.time
        )).ToList();

        totalMuertas = balasMuertasConTiempo.Aggregate(0, (acum, bala) => acum + 1);
    }

    void OrdenarImpactadas()
    {
        var ordenadasPorTipo = balasImpacto
            .Where(b => b.tipo == "Normal" || b.tipo == "Especial")
            .OrderBy(b => b.tipo)                                   
            .ToList();                                              

        foreach (var b in ordenadasPorTipo)
        Debug.Log($"[Impacto] {b.bala.name} - {b.tipo}");
    }

    void OrdenarMuertas()
    {
        var ordenadasPorTiempo = balasMuertasConTiempo
            .Skip(0)                                  
            .OrderByDescending(b => b.tiempoInstancia)
            .ToList();                                 

        foreach (var b in ordenadasPorTiempo)
        Debug.Log($"[Muerta] {b.bala.name} - {b.tiempoInstancia}");
    }
}


