using System.Collections.Generic;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<string, bool> _states;
    
    private Animator _animator;

    private void Awake()
    {
        _states = new Dictionary<string, bool>()
        {
            { "isIdle", true },
            { "isRunning", false },
            { "isFalling", false },
            { "isJumping", false }
        };

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (KeyValuePair<string, bool> concreteState in _states)
        {
            _animator.SetBool(concreteState.Key, concreteState.Value);
        }
    }

    public void SetState(string currentState, string newState)
    {
        _states[currentState] = false;
        _states[newState] = true;
    }
}
