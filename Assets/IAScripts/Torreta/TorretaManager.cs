using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorretaManager : MonoBehaviour
{
    public static TorretaManager instance;

    public List<GameObject> balasQueImpactaronPlayer = new();
    public List<GameObject> balasQueMurieron = new();

    private List<(GameObject bala, string tipo)> balasImpacto = new(); // Angelo Tupla
    private List<(GameObject bala, string tipo, float tiempoInstancia)> balasMuertasConTiempo = new(); // Iñaki Tupla
    private List<(GameObject bala, string tipo, float tiempoInstancia)> todasLasBalas = new(); // Manu Tupla

    private int totalImpacto = 0;
    private int totalMuertas = 0;
    private int totalDisparadas = 0;

    private void Awake()
    {
        instance = this;
    }

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

    private void BalasImpactadas() // Angelo Tupla
    {
        balasImpacto = balasQueImpactaronPlayer
            .Select(bala => (bala, bala.name.Contains("Super") ? "Super" : "Normal"))
            .ToList();

        foreach (var b in balasImpacto)
        {
            if (!todasLasBalas //Manu Tupla
                .Where(x => x.bala != null) //Manu LINQ
                .OrderBy(x => x.bala.name.Length)
                .Any(x => x.bala == b.bala))
            {
                todasLasBalas.Add((b.bala, b.tipo, b.bala.GetComponent<TorretaBullet>().DeathTime));
            }
        }

        var resultadoImpacto = balasImpacto.Aggregate( //Angelo Aggregate
            (Total: 0, Normales: 0, Supers: 0),
            (acc, b) =>
            {
                acc.Total++;
                if (b.tipo == "Normal") acc.Normales++;
                else if (b.tipo == "Super") acc.Supers++;
                return acc;
            });

        totalImpacto = resultadoImpacto.Total;

        Debug.Log($"Impactaron {resultadoImpacto.Total} balas (Normales: {resultadoImpacto.Normales}, Supers: {resultadoImpacto.Supers})");
    }

    private void BalasMuertas() // Iñaki Tupla
    {
        balasMuertasConTiempo = balasQueMurieron //Iñaki LINQ
                                .Select(bala => (bala, tipo: bala.name.Contains("Super") ? "Super" : "Normal", 
                                                        tiempo: bala.GetComponent<TorretaBullet>().DeathTime))
                                .OrderBy(b => b.tipo)
                                .ThenByDescending(b => b.tiempo)
                                .ToList();

        foreach (var b in balasMuertasConTiempo)
        {
            if (!todasLasBalas.Where(x => x.bala != null) //Iñaki LINQ
                                .OrderBy(x => x.bala.name.Length)
                                .Any(x => x.bala == b.bala))
            {
                todasLasBalas.Add((b.bala, b.tipo, b.tiempoInstancia));
            }
        }

        var resultadoMuertas = balasMuertasConTiempo.Aggregate( //Iñaki Aggregate
            (Total: 0, TiempoAcumulado: 0f),
            (acc, b) =>
            {
                acc.Total++;
                acc.TiempoAcumulado += b.tiempoInstancia;
                return acc;
            });

        totalMuertas = resultadoMuertas.Total;
        float promedioTiempo = resultadoMuertas.Total > 0 ? resultadoMuertas.TiempoAcumulado / resultadoMuertas.Total : 0;

        Debug.Log($"Murieron {totalMuertas} balas. Tiempo promedio antes de morir: {promedioTiempo:F2} segundos");
    }

    private void OrdenarImpactadas() // Aria LinQ
    {
        var ordenadasPorTipo = balasImpacto
            .Where(b => b.tipo == "Normal" || b.tipo == "Super")
            .OrderBy(b => b.tipo)
            .ToList();

        foreach (var b in ordenadasPorTipo)
        {
            Debug.Log($"[Impacto] {b.bala.name} - {b.tipo}");
        }
    }

    private void OrdenarMuertas() // Aria LinQ
    {
        var ordenadasPorTiempo = balasMuertasConTiempo
            .Where(b => b.tipo == "Normal" || b.tipo == "Super")
            .OrderByDescending(b => b.tiempoInstancia)
            .ToList();

        foreach (var b in ordenadasPorTiempo)
        {
            Debug.Log($"[Muerta] {b.bala.name} - {b.tiempoInstancia}");
        }
    }

    private void AgregadoYOrdenadoTotal() // Aria Aggregate
    {
        var resumenDisparadas = todasLasBalas.Aggregate(
            new Dictionary<string, int>(),
            (acc, b) =>
            {
                if (!acc.ContainsKey(b.tipo))
                    acc[b.tipo] = 0;
                acc[b.tipo]++;
                return acc;
            });

        totalDisparadas = resumenDisparadas.Values.Sum();

        Debug.Log($"Total de balas disparadas: {totalDisparadas}");
        foreach (var kv in resumenDisparadas)
        {
            Debug.Log($"Tipo: {kv.Key} - Cantidad: {kv.Value}");
        }

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


