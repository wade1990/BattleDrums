using System;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationBPMSync : MonoBehaviour
{
    [SerializeField] private StartingBeat startingBeat;
    [SerializeField] private bool _adjustSpeed = true;

    private enum StartingBeat
    {
        Beat,
        HalfTime,
        QuarterTime
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void Start()
    {
        if (_adjustSpeed)
        {
            BeatManager beatManager = BeatManager.Instance;
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips.First();
            float speed = beatManager.BPM / (60f / clip.length);
            _animator.speed = speed;
        }
        
        GetStartingBeat().AddListener(StartAnimation);
    }

    private UnityEvent GetStartingBeat()
    {
        switch (startingBeat)
        {
            case StartingBeat.Beat:
                return BeatManager.Instance.Beat;
            case StartingBeat.HalfTime:
                return BeatManager.Instance.HalfTimeBeat;
            case StartingBeat.QuarterTime:
                return BeatManager.Instance.QuarterTimeBeat;
        }

        throw new ArgumentOutOfRangeException();
    }

    private void StartAnimation()
    {
        GetStartingBeat().RemoveListener(StartAnimation);
        _animator.enabled = true;
    }
}
