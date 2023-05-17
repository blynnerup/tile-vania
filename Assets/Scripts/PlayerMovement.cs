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
    private float _startGravity;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float climbSpeed = 5;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        _startGravity = _rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();          
        AdjustPlayerFacing();
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        var layerMask = LayerMask.GetMask("Climbing");
        if (!_collider2D.IsTouchingLayers(layerMask))
        {
            _rigidbody2D.gravityScale = _startGravity;
            _animator.SetBool(IsClimbing, false);
            return;
        }
        _rigidbody2D.gravityScale = 0;
        Vector2 playerClimbVelocity = new Vector2(_rigidbody2D.velocity.x, _moveInput.y * climbSpeed);
        _rigidbody2D.velocity = playerClimbVelocity;

        _animator.SetBool(IsClimbing, _moveInput.y != 0);
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
