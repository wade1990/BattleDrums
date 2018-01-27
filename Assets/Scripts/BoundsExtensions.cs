using UnityEngine;

internal static class BoundsExtensions
{
    public static Vector2 GetRandomPoint(this Bounds bounds)
    {
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        return new Vector2(x, y);
    }
}
