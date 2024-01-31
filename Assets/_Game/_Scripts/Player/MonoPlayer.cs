﻿using UnityEngine;


[RequireComponent(typeof(MonoPlayerAnimation))]
public class MonoPlayer : MonoBehaviour
{
    private Player _meta;

    private PlayerInputHandler _input;

    [Header("Ground check")]
    [Space(5)]
    [SerializeField]
    private Transform _groundCheck;

    [SerializeField]
    private float _groundCheckRadius;

    [SerializeField]
    private LayerMask _whatIsGround;


    [Header("Player properties")]
    [Space(5)]
    [SerializeField]
    private PlayerType _playerType;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody2D _rb;

    [Header("Movement")]
    [Space(5)]

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _jumpForce;

    private bool _isGrounded;


    private void Awake()
    {
        if (_playerType == PlayerType.First)
            _input = new FirstPlayerInputHandler();
        else
            _input = new SecondPlayerInputHandler();
    }

    private void Update()
    {
        _input.HandleInput();

        if (_input.jump == true) {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        Move();
    }

    private void CheckGround() {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_input.moveDirection * _movementSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    public enum PlayerType { First, Second }
}