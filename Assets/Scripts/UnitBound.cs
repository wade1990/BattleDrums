using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UnitBound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.transform.GetComponent<Unit>();
        if (unit != null)
            unit.StopMoving();
    }
}
