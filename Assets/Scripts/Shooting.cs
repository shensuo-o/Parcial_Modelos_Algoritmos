using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shooting     
{
    public static void Fire(Transform SpawnBullet, GameObject bulletPreFab)
    {
            if (SpawnBullet != null)
            {
                GameObject.Instantiate(bulletPreFab, SpawnBullet.position, SpawnBullet.rotation);
            }
    }
}
