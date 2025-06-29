using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        int count = 0;

        while (true)
        {
            count++;
            string msg = BulletGenerator().Take(count).Last();
            Debug.LogWarning( msg + ", la torreta");
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerable <string> BulletGenerator()
    {
        int count = 0;
        while (true)
        {
            count++;
            if (count % 3 == 0)
            {
                yield return "aca dispara una super balatro";
            }
            else
            {
                yield return "aca dispara una bala normal";
            }
        }
    }
}
