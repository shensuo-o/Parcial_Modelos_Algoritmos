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

    public IEnumerable<(GameObject enemy, int type)> EnemigosAsesinaods()
    {
        return enemies.Where(x => x.GetComponent<Enemy>().Tag == 1 || x.GetComponent<Enemy>().Tag == 2)
                       .OrderBy(x => x.GetComponent<Enemy>().Tag)
                       .Select(x => (enemy: x, type: x.GetComponent<Enemy>().Tag))
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
