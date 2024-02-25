using UnityEngine;


namespace Vald
{
    [RequireComponent(typeof(MonoPlayerAnimation))]
    public class MonoPlayer : MonoBehaviour
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
        private PlayerInputHandler _input;

        [Header("Ground and top check")]
        [Space(5)]
        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private Transform _topCheck;

        [SerializeField]
        private float _checkOverlapRadius;

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

        // TODO: MAKE THIS UNSERIALIZED

        [SerializeField]
        private bool _isGrounded;

        [SerializeField]
        private bool _isFreeTop;

        [SerializeField]
        private bool _isNearLadder;

        [SerializeField]
        private bool _isOnLadder;

        // END TODO

        private Collider2D groundOverlap => Physics2D.OverlapCircle(_groundCheck.position, _checkOverlapRadius, _whatIsGround);
        private Collider2D topOverlap => Physics2D.OverlapCircle(_topCheck.position, _checkOverlapRadius, _whatIsGround);


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

            CheckTop();

            Move();
        }


        private void CheckGround()
        {
            _isGrounded = groundOverlap;
        }

        private void CheckTop()
        {
            _isFreeTop = !topOverlap;
        }

        private void Move()
        {
            if (!_isOnLadder) // Default moving and mount ladder
            {
                if (_isNearLadder && _input.ladderDirection > 0 && _rb.velocity.y == 0)
                    SwitchLadderControl(true);
                else
                    _rb.velocity = new Vector2(_input.moveDirection * _movementSpeed, _rb.velocity.y);
            }

            else // Ladder moving
            {
                if (_isOnLadder && _isGrounded && _input.ladderDirection < 0) // Unmount ladder if grounded
                {
                    SwitchLadderControl(false);
                }
                else
                {
                    if ((_input.ladderDirection > 0 && _isFreeTop) || _input.ladderDirection < 0)
                        _rb.MovePosition(transform.position + new Vector3(_input.moveDirection, _input.ladderDirection) * _ladderSpeed * Time.fixedDeltaTime);
                }
            }
        }


        private void Jump()
        {
            if ((_isGrounded || _isOnLadder) && _isFreeTop)
            {
                if (_rb.isKinematic == true)
                    _rb.isKinematic = false;

                _isOnLadder = false;

                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }
        }

        private void SwitchLadderControl(bool state)
        {
            _isOnLadder = state;
            _rb.isKinematic = state;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ladder")
            {
                _isNearLadder = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ladder")
            {
                _isNearLadder = false;

                if (_isOnLadder == true)
                    SwitchLadderControl(false);
            }
        }

        public enum PlayerType { First, Second }
    }
    public bool isInCage => _isInCage;
    public Player rawPlayer => _rawPlayer;

    public enum PlayerType { First, Second }
}