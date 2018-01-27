using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SunController : MonoBehaviour
{
    private Animator _animator;
    private int _beatIndex;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        BeatManager.Instance.Beat.AddListener(OnTheBeat);
    }

    private void OnTheBeat()
    {
        if (_beatIndex % 4 == 0)
        {
            _animator.SetTrigger("GreenPulse");
        }
        else
            _animator.SetTrigger("Pulse");

        _beatIndex++;
    }
}
