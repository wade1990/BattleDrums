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
        _animator.enabled = false;
    }

    private void Start()
    {
        BeatManager.Instance.Beat.AddListener(StartPulse);
    }

    private void StartPulse()
    {
        BeatManager.Instance.Beat.RemoveListener(StartPulse);
        _animator.enabled = true;
    }
}
