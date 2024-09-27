using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] public static EnemyFactory Instance;

    [SerializeField] public int Stock = 10;
    [SerializeField] public bool dynamic = true;

    [SerializeField] public ObjPool<Enemy> pool;

    [SerializeField] public Enemy enemyPrefab;

    void Start()
    {
        Instance = this;
        pool = new ObjPool<Enemy>(EnemyMaker, Enemy.SwitchOnOff, Stock, dynamic);
    }

    public Enemy EnemyMaker()
    {
        return Instantiate(enemyPrefab);
    }

    public void ReturnEnemy(Enemy b)
    {
        pool.ReturnObj(b);
    }
}
