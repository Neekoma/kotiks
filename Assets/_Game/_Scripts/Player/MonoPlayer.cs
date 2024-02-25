using UnityEngine;


[RequireComponent(typeof(MonoPlayerAnimation))]
public class MonoPlayer : MonoBehaviour
{
    private Player _rawPlayer;

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
    private float _ladderSpeed;

    [SerializeField]
    private float _jumpForce;

    private bool _isGrounded;

    private bool _isOnLadder;

    private bool _isInCage; // Block movement if kot in a cage

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

        if (_input.jump == true)
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        CheckGround();

        Move();
    }


    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }


    private void Move()
    {
        if (!_isOnLadder)
            _rb.velocity = new Vector2(_input.moveDirection * _movementSpeed, _rb.velocity.y);

        else
        {
            _rb.MovePosition(transform.position + new Vector3(_input.moveDirection, _input.ladderDirection) * _ladderSpeed * Time.fixedDeltaTime);
            _rb.velocity = Vector2.zero;
        }
    }


    private void Jump()
    {
        if (_isGrounded || _isOnLadder)
        {
            if (_rb.isKinematic == true)
                _rb.isKinematic = false;

            _isOnLadder = false;

            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            _isOnLadder = true;
            _rb.isKinematic = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            _isOnLadder = false;
            _rb.isKinematic = false;
        }
    }

    public bool isInCage => _isInCage;
    public Player rawPlayer => _rawPlayer;

    public enum PlayerType { First, Second }
}