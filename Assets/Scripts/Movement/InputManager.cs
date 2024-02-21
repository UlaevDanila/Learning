using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class InputManager : MonoBehaviour
    {
        public float InputDirection { get; private set; }
        public bool JumpButtonPressed { get; private set; }

        private StateMachine _stateMachine;

        private CollisionsHandler _collisionsHandler;

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            _collisionsHandler = GetComponent<CollisionsHandler>();
        }
        public void MoveRight(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<float>();
            _stateMachine.SetState("isIdle", "isRunning");
        }

        public void ExitMoveRight(InputAction.CallbackContext context)
        {
            if(context.canceled)
                _stateMachine.SetState("isRunning", "isIdle");
        }
    
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed && !_collisionsHandler.IsOnAir)
            {
                JumpButtonPressed = true;
            }
        }

        public void JumpExit(InputAction.CallbackContext context)
        {
            if (context.canceled)
                JumpButtonPressed = false;
        }
    }
}