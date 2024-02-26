using System;
using UnityEngine;

public class TrapTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}
