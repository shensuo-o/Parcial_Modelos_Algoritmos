using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _axisH;
    float _axisV;

    public Rigidbody2D rb;
    public float speed = 7f;

    public Transform SpawnBullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _axisH = Input.GetAxisRaw("Horizontal");
        _axisV = Input.GetAxisRaw("Vertical");

        LookMouse();
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
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookAtDirection);
        transform.rotation = rotation;
    }
}
