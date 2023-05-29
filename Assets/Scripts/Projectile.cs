using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    
    private Rigidbody2D _rigidbody2D;
    private PlayerMovement _playerMovement;
    private float xSpeed; 

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        xSpeed = _playerMovement.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(xSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
