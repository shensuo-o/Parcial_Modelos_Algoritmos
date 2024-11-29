using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttack : IAttack
{
    float _speed;
    float _cooldown;
    float _dashingTime;
    bool _isDashing;
    bool _canDash;
    Rigidbody2D _rb;
    Transform _direction;
    float _dashTimer;
    float _cooldownTimer;

    public DashAttack(float cooldown, Rigidbody2D rb, Transform direction, float speed = 7f,
        float dashingTime = 0.5f, bool canDash = true, bool isDashing = false, float dashTimer = 0f, float cooldownTimer = 0f)
    {
        _cooldown = cooldown;
        _rb = rb;   
        _direction = direction;
        _speed = speed;
        _dashingTime = dashingTime;
        _canDash = canDash;
        _isDashing = isDashing;
        _dashTimer = dashTimer;
        _cooldownTimer = cooldownTimer;
    }

    
    public void Attack()
    {
        if (_isDashing || !_canDash) return;

        StartDash();
    }

    public void Update()
    {
        if(_isDashing)
        {
            _dashTimer += Time.deltaTime;
            if(_dashTimer >= _dashingTime) EndDash();
        }

        if(!_canDash)
        {
            _cooldownTimer += Time.deltaTime;
            if(_cooldownTimer >= _cooldown) ResetCooldown();
        }
    }
    private void StartDash() 
    {
        _canDash = false;
        _isDashing = true;
        Vector2 dashDirection = new Vector2(_direction.right.x, _direction.right.y).normalized;
        _rb.AddForce(dashDirection * _speed, ForceMode2D.Impulse);
        Debug.Log("Hago Dash");
    }

    private void EndDash()
    {
        _isDashing = false;
        _rb.velocity = Vector2.zero;
        Debug.Log("Termino dash");
        _dashTimer = 0f;
        _cooldownTimer = 0f;
    }

    private void ResetCooldown()
    {
        Debug.Log("Reseteo Cooldown");
        _canDash = true;
    }
}
