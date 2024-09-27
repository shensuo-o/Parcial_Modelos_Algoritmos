using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Charecter
{
    public Transform target;
    //public float rangeView;
    float _distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //speed = 4f;
        life = FlyWeigthPointer.Enemy.maxLife;

    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
;    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance < FlyWeigthPointer.Enemy.rangeView)
        {
            Vector3 lookAtDirection = (target.position - transform.position).normalized;
            LookTarget(lookAtDirection);
            Movement(lookAtDirection);
        }
        if (life <= 0)
        {
            EnemyFactory.Instance.ReturnEnemy(this);
        }

    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * FlyWeigthPointer.Enemy.speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, FlyWeigthPointer.Enemy.rangeView);
        Gizmos.color = Color.red;
    }
    private void Reset()
    {
        life = FlyWeigthPointer.Enemy.maxLife;
    }

    public static void SwitchOnOff(Enemy e, bool active = true)
    {
        if (active) e.Reset();
        e.gameObject.SetActive(active);
    }
}
