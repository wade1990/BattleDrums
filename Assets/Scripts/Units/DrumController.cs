using UnityEngine;

public class DrumController : MonoBehaviour
{
    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        InputHandler inputHandler = GetComponentInParent<InputHandler>();

        inputHandler.BeatMade += beat => animator.SetTrigger("Beat");
    }
}
