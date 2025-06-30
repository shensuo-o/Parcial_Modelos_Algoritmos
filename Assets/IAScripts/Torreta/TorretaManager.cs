using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorretaManager : MonoBehaviour
{
    public static TorretaManager instance;

    public List<GameObject> balasQueImpactaronPlayer = new();
    public List<GameObject> balasQueMurieron = new();

    private List<(GameObject bala, string tipo)> balasImpacto = new();
    private List<(GameObject bala, string tipo, float tiempoInstancia)> balasMuertasConTiempo = new();
    private List<(GameObject bala, string tipo, float tiempoInstancia)> todasLasBalas = new();

    private int totalImpacto = 0;
    private int totalMuertas = 0;
    private int totalDisparadas = 0;

    private void Awake()
    {
        instance = this;
    }

    /* void Update()
     {
         BalasImpactadas();
         BalasMuertas();

         OrdenarImpactadas();
         OrdenarMuertas();

         AgregadoYOrdenadoTotal();
     }*/

    public void SumarBalasImpactadas(GameObject bullet)
    {
        balasQueImpactaronPlayer.Add(bullet);
    }

    public void SumarBalasMuertas(GameObject bullet)
    {
        balasQueMurieron.Add(bullet);
    }

    public void MostrarStats()
    {
        BalasImpactadas();
        BalasMuertas();
        OrdenarImpactadas();
        OrdenarMuertas();
        AgregadoYOrdenadoTotal();
    }

    private void BalasImpactadas()
    {
        balasImpacto = balasQueImpactaronPlayer
                        .Select(bala => (bala, bala.name
                        .Contains("Especial") ? "Especial" : "Normal"))
                        .ToList();

        totalImpacto++;

        foreach (var b in balasImpacto)
        {
            if (!todasLasBalas.Any(x => x.bala == b.bala))
                todasLasBalas.Add((b.bala, b.tipo, Time.time));
        }
    }

    private void BalasMuertas()
    {
        balasMuertasConTiempo = balasQueMurieron.Select(bala => (bala, bala.name.Contains("Especial") ? "Especial" : "Normal", Time.time)).ToList();

        totalMuertas++;

        foreach (var b in balasMuertasConTiempo)
        {
            if (!todasLasBalas.Any(x => x.bala == b.bala))
                todasLasBalas.Add((b.bala, b.tipo, b.tiempoInstancia));
        }
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

    void AgregadoYOrdenadoTotal()
    {
        totalDisparadas = todasLasBalas.Aggregate(0, (acum, b) => acum + 1);

        Debug.Log($"Total de balas disparadas: {totalDisparadas}");

        var ordenadasPorTiempo = todasLasBalas
            .TakeWhile(b => b.tiempoInstancia >= 0)
            .OrderByDescending(b => b.tiempoInstancia)
            .ToArray();                               

        Debug.Log("Balas ordenadas por tiempo de instancia (más recientes primero):");
        foreach (var b in ordenadasPorTiempo)
        {
            Debug.Log($"{b.bala.name} - Tipo: {b.tipo} - Tiempo: {b.tiempoInstancia}");
        }
    }
}


