using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle = 0,
        Running = 1,
        Jumping = 2,
        Falling = 3,
    };

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetState(int state)
    {
        switch (state)
        {
            case 3:
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isFalling", true);
                break;
            case 2:
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isFalling", false);
                break;
            case 1:
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isFalling", false);
                break;
            default:
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isFalling", true);
                break;
        }
    }
}
