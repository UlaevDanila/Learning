using Movable;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _jumpVelocity;

    [SerializeField] private float _movementVelocity;

    private Animator _animator;
    
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;

    private bool _isOnAir;

    private float _inputDirection;

    private float _previousYPosition;

    private void Awake()
    {
        _isOnAir = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_inputDirection < 0)
            _spriteRenderer.flipX = true;
        if(_inputDirection > 0 && _spriteRenderer.flipX)
            _spriteRenderer.flipX = false;

        if (_isOnAir && (transform.position.y > _previousYPosition))
        {
            _animator.SetBool("isJumping", true);
            _animator.SetBool("isRunning", false);
        }
        else if(_isOnAir && (transform.position.y < _previousYPosition))
        {
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", true);
        }
    }

    private void LateUpdate()
    {
        _previousYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        float velocityMultiplication = _movementVelocity*Time.fixedDeltaTime;
        _rigidbody2D.velocity = new Vector2(_inputDirection* velocityMultiplication, _rigidbody2D.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Tilemap")
        {
            _isOnAir = false;
            _animator.SetBool("isFalling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Tilemap")
        {
            _isOnAir = true;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<float>();
        _animator.SetBool("isRunning", true);
    }

    public void ExitMoveRight(InputAction.CallbackContext context)
    {
        if(context.canceled)
            _animator.SetBool("isRunning", false);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isOnAir)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * _jumpVelocity);
        }
    }
}
