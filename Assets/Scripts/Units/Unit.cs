using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Unit : MonoBehaviour
{
    public Vector2 ForwardDirection;
    public float Speed;

    private HealthController _healthController;
    protected AttackController AttackController;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        AttackController = GetComponentInChildren<AttackController>();
    }

    private void Update()
    {
        gameObject.transform.position += _moveDirection * Speed;
    }

    public virtual void MoveForward()
    {
        _moveDirection = ForwardDirection;
    }

    public virtual void MoveBackWards()
    {
        _moveDirection = -ForwardDirection;
    }

    public virtual void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }

    public virtual void Attack()
    {
        StopMoving();
        AttackController.Attack();
    }

    public virtual void ApplyDamage(float damage)
    {
        _healthController.Health -= damage;
    }
}