using UnityEngine;


[RequireComponent(typeof(MonoPlayerAnimation))]
public class MonoPlayer : MonoBehaviour
{
    private Player _meta;

    private PlayerInputHandler _input;
    private float _movementDirection;

    [SerializeField]
    private PlayerType _playerType;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _jumpForce;

    private void Awake()
    {
        if (_playerType == PlayerType.First)
            _input = new FirstPlayerInputHandler();
        else
            _input = new SecondPlayerInputHandler();
    }

    private void Update()
    {
        _movementDirection = _input.HandleInput();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movementDirection * _movementSpeed, _rb.velocity.y);
    }

    public enum PlayerType { First, Second }
}