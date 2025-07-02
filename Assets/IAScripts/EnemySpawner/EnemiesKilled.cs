using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesKilled : MonoBehaviour
{
    public static EnemiesKilled Instance;
    public List<GameObject> enemies;

    private void Awake()
    {
        Instance = this;
    }

    public void AddKilledEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public IEnumerable<(GameObject enemy, int type)> EnemigosAsesinaods()//Iñaki Generator
    {
        return enemies.Where(x => x.GetComponent<IAEnemy>().Tag == 1 || x.GetComponent<IAEnemy>().Tag == 2)//Iñaki LinQ
                       .OrderBy(x => x.GetComponent<IAEnemy>().Tag)
                       .Select(x => (enemy: x, type: x.GetComponent<IAEnemy>().Tag))
                       .ToList();
    }

    public void ShowEnemiesKilled()
    {
        Debug.LogWarning("Lista de enemigos asesinados: ");

        var killedEnemies = EnemigosAsesinaods();

        foreach(var (enemy, type) in killedEnemies)
        {
            string enemyName = enemy.name;
            Debug.LogWarning($"[Derrotado] Enemy: " + enemyName + " del Tipo: " + type);
        }
    }
}
