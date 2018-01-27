using System.Collections;
using UnityEngine;
using UnityEngine.Events;

internal class ArrowController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private UnityEvent _targetReached;

    [SerializeField] private float _timeBeforeFading;
    [SerializeField] private float _fadingDuration;


    public void StartMoving(Vector2 targetPosition)
    {
        StartCoroutine(MoveRoutine(targetPosition));
    }

    public IEnumerator MoveRoutine(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float distance = Vector2.Distance(startPosition, targetPosition) / 2;

        float duration = distance * 2 / _speed;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float f = Mathf.PI * elapsedTime  / duration - Mathf.PI / 2f;

            float dx = Mathf.Sin(f) * distance + distance;
            float dy = Mathf.Cos(f) * distance;

            Vector2 newPosition = startPosition + new Vector2(dx, dy);
            transform.right = newPosition - (Vector2)transform.position;
            transform.position = newPosition;
            
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        _targetReached.Invoke();
        yield return FadeAway();
    }

    private IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(_timeBeforeFading);
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

        Color color = sprite.color;
        float startAlpha = color.a;


        float elapsedTime = 0;
        while (elapsedTime < _fadingDuration)
        {
            float point = Mathf.InverseLerp(0, _fadingDuration, elapsedTime);
            float alpha = Mathf.Lerp(startAlpha, 0, point);

            color.a = alpha;
            sprite.color = color;

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        Destroy(gameObject);
    }
}

