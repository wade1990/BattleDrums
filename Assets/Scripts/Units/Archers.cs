using System.Collections;
using UnityEngine;

public class Archers : Unit
{
    [SerializeField] private float _beatsPerDamage;

    private IEnumerator attackingRoutine;

    public override void Attack()
    {
        attackingRoutine = AttackRoutine();
        StartCoroutine(attackingRoutine);
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

    public override void StopMoving()
    {
        StopAttacking();
        base.StopMoving();
    }

    private void StopAttacking()
    {
        StopCoroutine(attackingRoutine);
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            base.Attack();
            yield return new WaitForSeconds(_beatsPerDamage);
        }
    }
}