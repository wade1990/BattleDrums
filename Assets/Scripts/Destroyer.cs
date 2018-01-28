using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void StartDieAnimation()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();

        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Hurt");
            animator.SetTrigger("Dead");
            animator.SetTrigger("Disappear");
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
