using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
