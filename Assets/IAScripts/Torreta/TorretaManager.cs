using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorretaManager : MonoBehaviour
{
    public static TorretaManager instance;

    public List<GameObject> balasQueImpactaronPlayer = new();
    public List<GameObject> balasQueMurieron = new();

    private List<(GameObject bala, string tipo)> balasImpacto = new();//Angelo Tupla
    private List<(GameObject bala, string tipo, float tiempoInstancia)> balasMuertasConTiempo = new(); //Iñaki Tupla
    private List<(GameObject bala, string tipo, float tiempoInstancia)> todasLasBalas = new(); //Manu Tupla

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

    private void BalasImpactadas()//Angelo Tupla
    {
        balasImpacto = balasQueImpactaronPlayer
                        .Select(bala => (bala, bala.name
                        .Contains("Super") ? "Super" : "Normal"))
                        .ToList();

        foreach (var b in balasImpacto)
        {
            if (!todasLasBalas.Any(x => x.bala == b.bala))
                todasLasBalas.Add((b.bala, b.tipo, b.bala.GetComponent<TorretaBullet>().DeathTime));
        }

        totalImpacto = balasImpacto.Aggregate(0, (acum, bala) => acum + 1);//Angelo Aggregate
    }

    private void BalasMuertas()//Iñaki Tupla
    {
        balasMuertasConTiempo = balasQueMurieron.Select(bala => (bala, bala.name.Contains("Super") ? "Super" : "Normal", bala.GetComponent<TorretaBullet>().DeathTime)).ToList();

        foreach (var b in balasMuertasConTiempo)
        {
            if (!todasLasBalas.Any(x => x.bala == b.bala))
                todasLasBalas.Add((b.bala, b.tipo, b.tiempoInstancia));

            totalMuertas = balasMuertasConTiempo.Aggregate(0, (acum, bala) => acum + 1);//Manu Aggregate
        }
    }

    void OrdenarImpactadas()
    {
        var ordenadasPorTipo = balasImpacto//Aria LinQ
            .Where(b => b.tipo == "Normal" || b.tipo == "Super")
            .OrderBy(b => b.tipo)
            .ToList();

        foreach (var b in ordenadasPorTipo)
        {
            Debug.Log($"[Impacto] {b.bala.name} - {b.tipo}");
        }

    }

    void OrdenarMuertas()//Aria LinQ
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
        totalDisparadas = todasLasBalas.Aggregate(0, (acum, b) => acum + 1);//Aria Agregate

        Debug.Log($"Total de balas disparadas: {totalDisparadas}");

        var ordenadasPorTiempo = todasLasBalas//Aria LinQ
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


