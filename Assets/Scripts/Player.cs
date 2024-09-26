using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    float _axisH;
    float _axisV;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform SpawnBullet;

    private void Awake()
    {
        speed = 7f;
        life = maxlife;
    }
    void Update()
    {
        _axisH = Input.GetAxisRaw("Horizontal");
        _axisV = Input.GetAxisRaw("Vertical");

        LookMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Fire(SpawnBullet);
        }
        Death();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.MovePosition(transform.position + new Vector3(_axisH, _axisV, 0).normalized
        * speed * Time.fixedDeltaTime);

    }

    void LookMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 lookAtDirection = (mouseWorldPosition - transform.position).normalized;
        //Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookAtDirection);
        float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle);
        SpawnBullet.rotation = Quaternion.Euler(0,0, angle);
    }
}
