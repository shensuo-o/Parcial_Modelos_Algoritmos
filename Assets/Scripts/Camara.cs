using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y) + offset;
    }
}
