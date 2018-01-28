using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _attackedStopsMoving = true;

    private readonly Dictionary<Collider2D, Unit> _enemiesInRange = new Dictionary<Collider2D, Unit>();

    public event Action<Unit> TriggerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit enemy = collision.GetComponent<Unit>();
        if (enemy == null)
            return;

        _enemiesInRange[collision] = enemy;
        enemy.Dying += x => Remove(collision);

        if (TriggerEntered != null)
            TriggerEntered.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Remove(collision);
    }

    private void Remove(Collider2D collision)
    {
        _enemiesInRange.Remove(collision);
    }

    public void Attack()
    {
        foreach (Collider2D collision in _enemiesInRange.Keys)
        {
            if (collision == null)
                continue;

            Unit enemy = _enemiesInRange[collision];

            enemy.ApplyDamage(_damage);

            if (_attackedStopsMoving)
                enemy.StopMoving();
        }
    }
}
