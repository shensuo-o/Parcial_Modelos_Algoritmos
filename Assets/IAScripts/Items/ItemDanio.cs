using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDanio : MonoBehaviour
{
    [SerializeField] private float dmgE;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("toque al player");
        if (other.GetComponent<Player>())
        {
            PlayerBonus.damageExtra +=dmgE;
            Debug.Log($"Bonus de daño ahora: +{PlayerBonus.damageExtra}");
            Destroy(this.gameObject);
        }
    }
}

