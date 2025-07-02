using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCuracion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("toque al player");
        if (other.gameObject.layer == 8)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AumentarVida(10);
                Destroy(this.gameObject);
            }
        }
    }
}
