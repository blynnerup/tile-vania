using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float moveSpeed = 1;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        Vector2 runningVector = new Vector2( _rigidbody2D.position.x + _moveInput.x * (moveSpeed * Time.fixedDeltaTime), _rigidbody2D.position.y);
        _rigidbody2D.MovePosition(runningVector);
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
}
