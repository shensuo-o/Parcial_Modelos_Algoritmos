using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Charecter
{
    float _axisH;
    float _axisV;

    public BulletCount bulletCount;

    [SerializeField] float speed;

    [SerializeField] Transform SpawnBullet;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] BoxCollider2D boxCollider;
    public Torreta turret;

    private bool CanShoot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 7f;
        life = maxlife;
        CanShoot = true;
    }
    void Update()
    {
        _axisH = Input.GetAxisRaw("Horizontal");
        _axisV = Input.GetAxisRaw("Vertical");

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 lookAtDirection = (mouseWorldPosition - transform.position).normalized;
        LookTarget(lookAtDirection);
        SpawnBullet.rotation = transform.rotation;

        if (Input.GetMouseButtonDown(0) && CanShoot)
        {
            StartCoroutine(BulletCD());
        }

        if (life <= 0)
        {
            life = 1;

            StartCoroutine(MostrarStats());

            boxCollider.enabled = false;
            turret.canShoot = false;
            GameManager.instance.playerAlive = false;
            //GameManager.instance.ChangeScene("Lose");
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_axisH, _axisV, 0).normalized;
        Movement(direction);
    }

    private IEnumerator BulletCD() //Time-Slicing Iñaki
    {
        CanShoot = false;

        var b = new { bullet = BulletFactory.Instance.pool.GetObject() }; //Anonimo Iñaki

        if (!b.bullet)
        {
            yield break;
        }

        b.bullet.transform.SetPositionAndRotation(SpawnBullet.position, SpawnBullet.rotation);

        yield return new WaitForSeconds(0.1f);

        CanShoot = true;
    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator MostrarStats()//Aria Time-Slicing
    {
        Debug.Log("Mostrando estadísticas...");

        TorretaManager.instance.MostrarStats();
        yield return new WaitForSeconds(0.3f);

        EnemiesKilled.Instance.ShowEnemiesKilled();
        yield return new WaitForSeconds(0.3f);

        BalasUsadas.instance.LogBullets();
        yield return new WaitForSeconds(0.3f);

        bulletCount.CalcularPeligroPorLayer();
        yield return new WaitForSeconds(0.3f);

        Debug.Log("Estadísticas finalizadas");
    }

    public void AumentarVida(int cantidad)
    {
        life += cantidad;
        Debug.Log($"Vida aumentada a {life}");
    }

    public void AumentarVelocidad(float cantidad)
    {
        speed += cantidad;
        Debug.Log($"Velocidad aumentada a {speed}");
    }


}
