using System.Collections;
using UnityEngine;
using UnityEngine.Events;

internal class ArrowController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _targetReached;

    public void StartMoving(Vector2 targetPosition)
    {
        StartCoroutine(MoveRoutine(targetPosition));
    }

    public IEnumerator MoveRoutine(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float distance = Vector2.Distance(startPosition, targetPosition);

        float progressIncrement = _speed / distance;
        float progress = 0;

        while ((Vector2)transform.position != targetPosition)
        {
            Vector2 newPosition = Vector3.Slerp(startPosition, targetPosition, progress);
            transform.right = newPosition - (Vector2)transform.position;

            transform.position = newPosition;

            progress += progressIncrement;
            yield return null;
        }

        Destroy(gameObject);
        _targetReached.Invoke();
    }
}

