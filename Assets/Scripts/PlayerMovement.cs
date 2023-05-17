using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _isJumping;
    private CapsuleCollider2D _collider2D;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float jumpSpeed = 5;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();          
        AdjustPlayerFacing();
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = playerVelocity;
        if (_moveInput.x != 0)
        {
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
    }

    private void AdjustPlayerFacing()
    {
        var localScale = transform.localScale;
        localScale = _moveInput.x switch
        {
            // transform.localScale = _moveInput.x < 0 ? new Vector2(-1, 1) : new Vector3(1, 1);
            < 0 => new Vector2(-1, 1),
            > 0 => new Vector2(1, 1),
            0 => localScale,
            _ => localScale
        };
        transform.localScale = localScale;
    }

    private void OnJump(InputValue value)
    {
        var layerMask = LayerMask.GetMask("Ground");
        if (!value.isPressed || !_collider2D.IsTouchingLayers(layerMask)) return;
        _rigidbody2D.velocity += new Vector2(0, jumpSpeed);
        // _rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        Debug.Log(value);
    }

}
