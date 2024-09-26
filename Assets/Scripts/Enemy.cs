using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Charecter
{
    public Transform target;
    public float rangeView;
    float _distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 4f;
        life = maxlife;

    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance < rangeView)
        {
            Vector3 lookAtDirection = (target.position - transform.position).normalized;
            LookTarget(lookAtDirection);
            Movement(lookAtDirection);
        }
        Death();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeView);
        Gizmos.color = Color.red;
    }
}
