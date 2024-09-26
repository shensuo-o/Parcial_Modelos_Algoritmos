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

    };
}
