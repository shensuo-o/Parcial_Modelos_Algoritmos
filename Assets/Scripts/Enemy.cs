using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Charecter
{
    public Transform target;
    //public float rangeView;
    float _distance;

    public int randomStrategy;
    public Transform spawner;
    public float shootCooldown;
    public float dashCooldown;

    IAttack _myCurrentStrategy;
    IAttack _myDashAttack;
    IAttack _myShootAttack;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //speed = 4f;
        life = FlyWeigthPointer.Enemy.maxLife;

        randomStrategy = Random.Range(1, 3);

        _myDashAttack = new DashAttack(dashCooldown, rb, spawner);
        _myShootAttack = new ShootAttack(spawner, shootCooldown);
        if (randomStrategy == 1) _myCurrentStrategy = _myDashAttack;
        else if (randomStrategy == 2) _myCurrentStrategy = _myShootAttack;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
;    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if(randomStrategy == 1)  _myCurrentStrategy.Update();
        
        if (_distance < FlyWeigthPointer.Enemy.rangeView)
        {
            Vector3 lookAtDirection = (target.position - transform.position).normalized;
            LookTarget(lookAtDirection);
            Movement(lookAtDirection);
            if (_myCurrentStrategy != null)
                _myCurrentStrategy.Attack();
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(FlyWeigthPointer.Enemy.enemyDMG);
        }
    }
}
