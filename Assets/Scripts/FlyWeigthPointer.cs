using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class FlyWeigthPointer
{
    public static readonly FlyWeigth Enemy = new FlyWeigth
    {
        speed = 4,
        maxLife = 100,
        rangeView = 10,
        enemyDMG = 5,

    };

    public static readonly FlyWeigth Bullet = new FlyWeigth
    {
        speed = 8,
        bulletDMG = 10,
        lifeTime = 5,

    };
}
