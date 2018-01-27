using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private bool isMoving;

    private void Update()
    {
        if (isMoving)
            StartMoving();
    }

    private void StartMoving()
    {
        gameObject.transform.position += moveDirection;
    }

    private void StopMoving()
    {
        
    }
}
