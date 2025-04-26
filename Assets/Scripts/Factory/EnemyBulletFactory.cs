using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletFactory : MonoBehaviour
{
    [SerializeField] public static EnemyBulletFactory Instance;

    [SerializeField] public int stock = 20;
    [SerializeField] public bool dynamic = true;

    [SerializeField] public ObjPool<EnemyBullet> pool;

    [SerializeField] public EnemyBullet enemyBulletPrefab;

    void Start()
    {
        Instance = this;
        pool = new ObjPool<EnemyBullet>(BulletMaker, EnemyBullet.SwitchOnOff, stock, dynamic);
    }

    public EnemyBullet BulletMaker()
    {
        return Instantiate(enemyBulletPrefab);
    }

    public void ReturnBullet(EnemyBullet b)
    {
        pool.ReturnObj(b);
    }
}