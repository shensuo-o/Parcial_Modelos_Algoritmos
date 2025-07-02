using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BalasUsadas : MonoBehaviour
{
    public static BalasUsadas instance;
    public List<GameObject> balasUsadas;

    private void Awake()
    {
        instance = this;
    }

    public void AgregarBala(GameObject bala)
    {
        balasUsadas.Add(bala);
    }

    public IEnumerable <(GameObject bullet, bool hit)> BulletHistory(List<GameObject> balas)//Iñaki Generator
    {
        foreach (var bullet in balas)
        {
            var bScript = bullet.GetComponent<Bullet>();
            if (bScript != null)
            {
                yield return (bullet, bScript.impacto);
            }
        }
    }

    public List<(GameObject bullet, float damage, bool hit)> GetImpactBullets(List<GameObject> balas)
    {
        return BulletHistory(balas).Where(b => b.hit)//Iñaki LinQ
                                    .Select(b => (bullet: b.bullet, damage: b.bullet.GetComponent<Bullet>().damage, hit: b.bullet.GetComponent<Bullet>().impacto))
                                    .OrderByDescending(b => b.damage)
                                    .ToList();
    }

    public void LogBullets()
    {
        var bulletData = GetImpactBullets(balasUsadas);

        var average = AverageDamageLast10(balasUsadas);

        foreach (var (bullet, damage, hit) in bulletData)
        {
            Debug.LogWarning($"Bala: " + bullet.name + " | ¿Impactó?:" + hit + " con daño: " + damage);
        }

        Debug.LogWarning("El daño promedio de las ultimas 10 balas es " + average);
    }

    public float AverageDamageLast10(List<GameObject> balas)//Manu LinQ
    {
        return balas.Where(b => b.GetComponent<Bullet>().impacto)
                    .OrderByDescending(b => b.GetComponent<Bullet>().timeFired)
                    .Take(10)
                    .Average(b => b.GetComponent<Bullet>().damage);
    }


}
