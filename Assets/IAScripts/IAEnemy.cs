using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IAEnemy : Charecter
{
    public static IEnumerable<(int vida, int daño, float velocidad)> GenerarStats(int cantidad)//Aria Generator
    {
        for (int i = 0; i < cantidad; i++)
        {
            int vida = Random.Range(80, 150);
            int daño = Random.Range(3, 10);
            float velocidad = Random.Range(2f, 5f);
            yield return (vida, daño, velocidad);
        }//Aria Tupla
    }

    private float velocidad;
    private int daño;
    public int Tag;
    public Transform target;
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

        var stat = GenerarStats(1).First();
        life = stat.vida;
        daño = stat.daño;
        velocidad = stat.velocidad;

        randomStrategy = Random.Range(1, 3);
        Tag = randomStrategy;

        _myDashAttack = new DashAttack(dashCooldown, rb, spawner);
        _myShootAttack = new ShootAttack(spawner, shootCooldown);
        if (randomStrategy == 1) _myCurrentStrategy = _myDashAttack;
        else if (randomStrategy == 2) _myCurrentStrategy = _myShootAttack;
    }


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ;
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (randomStrategy == 1) _myCurrentStrategy.Update();
        if (randomStrategy == 2) _myCurrentStrategy.Update();

        if (_distance < FlyWeigthPointer.Enemy.rangeView)
        {
            Vector3 lookAtDirection = (target.position - transform.position).normalized;
            LookTarget(lookAtDirection);
            Movement(lookAtDirection);
            if (_myCurrentStrategy != null)
            {
                Debug.Log("enemigo dispara");
                _myCurrentStrategy.Attack();
            }
        }
        if (life <= 0)
        {
            EnemiesKilled.Instance.AddKilledEnemy(this.gameObject);
            this.gameObject.SetActive(false);
        }

    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * velocidad * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, FlyWeigthPointer.Enemy.rangeView);
        Gizmos.color = Color.red;
    }
    private void Reset()
    {
        life = GenerarStats(1).First().vida;
    }

    public static void SwitchOnOff(IAEnemy e, bool active = true)
    {
        if (active) e.Reset();
        e.gameObject.SetActive(active);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(daño);
        }
    }
}
