using System.Collections.Generic;
using UnityEngine;

internal class AttackController : MonoBehaviour
{
    [SerializeField] private float _damage;

    private Dictionary<Collider2D, Unit> _enemiesInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit enemy = collision.GetComponent<Unit>();
        if (enemy == null)
            return;
        
        _enemiesInRange[collision] = enemy;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _enemiesInRange.Remove(collision);
    }

    public void Attack()
    {
        foreach (Unit enemy in _enemiesInRange.Values)
            enemy.ApplyDamage(_damage);
    }
}
