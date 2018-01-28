using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthController))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnityEvent StartedMoving;
    public Vector2 ForwardDirection;
    public float Speed;
    public float MovementDuration = 1.0f;

    public event Action<Unit> Dying;

    protected Animator[] _animators;
    private HealthController _healthController;
    protected AttackController AttackController;

    private Vector3 _moveDirection;

    private bool _isMoving;

    public bool Alive { get { return _healthController.Alive; } }

    protected virtual void Awake()
    {
        _animators = GetComponentsInChildren<Animator>();
        AttackController = GetComponentInChildren<AttackController>();
        _healthController = GetComponent<HealthController>();

        _healthController.Died += x =>
        {
            if (Dying != null)
                Dying.Invoke(this);

            enabled = false;
        };
    }

    protected virtual void Start()
    {
        StopMoving();
    }

    private void Update()
    {
        if (_isMoving)
            gameObject.transform.position += _moveDirection * Speed;
    }

    public virtual void MoveForward()
    {
        _moveDirection = ForwardDirection;
        StartMoving();
    }

    public virtual void MoveBackWards()
    {
        _moveDirection = -ForwardDirection;
        StartMoving();
    }

    protected void StartMoving()
    {
        StartedMoving.Invoke();
        _isMoving = true;

        Invoke("StopMoving", MovementDuration);

        foreach (Animator animator in _animators)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdeling", false);
        }
    }

    public virtual void StopMoving()
    {
        _isMoving = false;

        foreach (Animator animator in _animators)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdeling", true);
        }
    }

    public virtual void Attack()
    {
        StopMoving();
        AttackController.Attack();

        foreach (Animator animator in _animators)
        {
            animator.SetTrigger("Action");
        }
    }

    public virtual void ApplyDamage(float damage)
    {
        foreach (Animator animator in _animators)
            animator.SetTrigger("Hurt");

        _healthController.Health -= damage;
    }
}