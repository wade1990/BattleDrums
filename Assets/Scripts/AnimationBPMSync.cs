using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBPMSync : MonoBehaviour
{
    [SerializeField] private float _baseBPM = 60f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void Start()
    {
        BeatManager beatManager = BeatManager.Instance;
        _animator.speed = beatManager.BPM / _baseBPM;
        beatManager.Beat.AddListener(StartPulse);
    }

    private void StartPulse()
    {
        BeatManager.Instance.Beat.RemoveListener(StartPulse);
        _animator.enabled = true;
    }
}
