using UnityEngine;

public class Horsemen : Unit
{
    public override void Attack()
    {
        AttackController.Attack();

        foreach (Animator animator in _animators)
        {
            animator.SetTrigger("Action");
        }
    }
}
