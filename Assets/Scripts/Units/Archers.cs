using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ArrowSpawner))]
public class Archers : Unit
{
    [SerializeField] private float _beatsPerDamage;
    [SerializeField] private float _volleyDuration;

    private ArrowSpawner _arrowSpawner;

    private IEnumerator attackingRoutine;

    protected override void Awake()
    {
        base.Awake();
        _arrowSpawner = GetComponent<ArrowSpawner>();
    }

    public override void Attack()
    {
        attackingRoutine = AttackRoutine();
        _arrowSpawner.StartShooting();

        StartCoroutine(attackingRoutine);
        Invoke("StopAttacking", _volleyDuration);
    }

    public override void MoveBackWards()
    {
        StopAttacking();
        base.MoveBackWards();
    }

    public override void MoveForward()
    {
        StopAttacking();
        base.MoveForward();
    }

    private void StopAttacking()
    {
        if (attackingRoutine == null)
            return;

        _arrowSpawner.StopShooting();
        StopCoroutine(attackingRoutine);
        attackingRoutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            base.Attack();
            yield return new WaitForSeconds(_beatsPerDamage);
        }
    }

    private void OnDestroy()
    {
        if (attackingRoutine != null)
            StopCoroutine(attackingRoutine);
    }
}