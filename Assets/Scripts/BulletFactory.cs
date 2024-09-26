using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] public static BulletFactory Instance;

    [SerializeField] public int stock = 20;
    [SerializeField] public bool dynamic = true;

    [SerializeField] public ObjPool<Bullet> pool;

    [SerializeField] public Bullet bulletPrefab;

    void Start()
    {
        Instance = this;
        pool = new ObjPool<Bullet>(BulletMaker, Bullet.SwitchOnOff, stock, dynamic);
    }

    public Bullet BulletMaker()
    {
        return Instantiate(bulletPrefab);
    }

    public void ReturnBullet(Bullet b)
    {
        pool.ReturnObj(b);
    }
}
