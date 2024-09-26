using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Charecter
{
    float _axisH;
    float _axisV;

    [SerializeField] float speed;

    [SerializeField] Transform SpawnBullet;
    [SerializeField] GameObject bulletPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 7f;
        life = maxlife;
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

        if (Input.GetMouseButtonDown(0))
        {
            Shooting.Fire(SpawnBullet, bulletPrefab);
        }
        Death();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_axisH, _axisV, 0).normalized;
        Movement(direction);
    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    /*void LookMouse()
    {

        float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle);
        SpawnBullet.rotation = Quaternion.Euler(0,0, angle);
    }*/
}
