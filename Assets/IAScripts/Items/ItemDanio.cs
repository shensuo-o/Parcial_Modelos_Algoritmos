using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDanio : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            PlayerBonus.damageExtra += 5;
            Debug.Log($"Bonus de daño ahora: +{PlayerBonus.damageExtra}");
            Destroy(gameObject);
        }
    }
}

