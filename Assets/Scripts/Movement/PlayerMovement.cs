using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IMovable
    {
    [SerializeField] private float _jumpVelocity;

    [SerializeField] private float _movementVelocity;

    private StateMachine _stateMachine;
    
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;

    private bool _isOnAir;

    private float _inputDirection;

    private float _previousYPosition;

    private void Awake()
    {
        _isOnAir = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponent<StateMachine>();
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
            _stateMachine.SetState("isRunning", "isJumping");
        }
        else if(_isOnAir && (transform.position.y < _previousYPosition))
        {
            _stateMachine.SetState("isJumping", "isFalling");
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
            _stateMachine.SetState("isFalling", "isIdle");
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
        _stateMachine.SetState("isIdle", "isRunning");
    }

    public void ExitMoveRight(InputAction.CallbackContext context)
    {
        if(context.canceled)
            _stateMachine.SetState("isRunning", "isIdle");
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isOnAir)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * _jumpVelocity);
        }
    }
    }
}
