using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
    [SerializeField] private float _jumpVelocity;

    [SerializeField] private float _movementVelocity;

    private StateMachine _stateMachine;
    
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;

    private CollisionsHandler _collisionsHandler;

    private InputManager _inputManager;

    private float _previousYPosition;

    private void Awake()
    {
        _collisionsHandler = GetComponent<CollisionsHandler>();
        _inputManager = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponent<StateMachine>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_inputManager.InputDirection < 0)
            _spriteRenderer.flipX = true;
        if(_inputManager.InputDirection > 0 && _spriteRenderer.flipX)
            _spriteRenderer.flipX = false;

        if (_collisionsHandler.IsOnAir && (transform.position.y > _previousYPosition))
        {
            _stateMachine.SetState("isRunning", "isJumping");
        }
        else if(_collisionsHandler.IsOnAir && (transform.position.y < _previousYPosition))
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
        float verticalVelocity = _inputManager.JumpButtonPressed && !_collisionsHandler.IsOnAir ? _jumpVelocity : _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = new Vector2(_inputManager.InputDirection * velocityMultiplication, verticalVelocity);
        
    }
    
    }
}
