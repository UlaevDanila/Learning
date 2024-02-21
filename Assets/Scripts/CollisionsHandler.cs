using UnityEngine;

public class CollisionsHandler : MonoBehaviour
{
    private StateMachine _stateMachine;
    public bool IsOnAir { get; private set; }

    private void Awake()
    {
        IsOnAir = true;
        _stateMachine = GetComponent<StateMachine>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Tilemap")
        {
            IsOnAir = false;
            _stateMachine.SetState("isFalling", "isIdle");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Tilemap")
        {
            IsOnAir = true;
        }
    }
}
