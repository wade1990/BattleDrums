using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void DestroySelf()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();

        //todo make this work
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Disappear");
        }
        Destroy(gameObject);
    }
}
