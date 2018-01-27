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
        _animator.speed = 60f / BeatManager.Instance.BeatTime * 4;
    }


}
