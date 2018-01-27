using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

internal class AttackController : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _beatsPerAttack;

    private readonly Dictionary<Collider2D, IEnumerator> attackRoutines = new Dictionary<Collider2D, IEnumerator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit enemy = collision.GetComponent<Unit>();
        IEnumerator attackRoutine = AttackingRoutine(enemy);

        attackRoutines[collision] = attackRoutine;
        StartCoroutine(attackRoutine);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IEnumerator attackRoutine = attackRoutines[collision];
        StopCoroutine(attackRoutine);
    }

    private IEnumerator AttackingRoutine(Unit enemy)
    {
        while (true)
        {
            enemy.ApplyDamage(_damage);

            float interval = _beatsPerAttack * BeatManager.Instance.BeatTime;
            yield return new WaitForSeconds(interval);
        }
    }
}
