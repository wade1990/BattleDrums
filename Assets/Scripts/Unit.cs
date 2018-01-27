using UnityEngine;

[RequireComponent(typeof(HealthController))]
internal class Unit : MonoBehaviour
{
    public Vector2 ForwardDirection;
    public float Speed;

    private HealthController _healthController;
    private AttackController _attackController;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _attackController = GetComponentInChildren<AttackController>();
    }

    private void Update()
    {
        gameObject.transform.position += _moveDirection * Speed;
    }

    public void MoveForward()
    {
        _moveDirection = ForwardDirection;
    }

    public void MoveBackWards()
    {
        _moveDirection = -ForwardDirection;
    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }

    public virtual void Attack()
    {
        _attackController.Attack();
    }

    public void ApplyDamage(float damage)
    {
        _healthController.Health -= damage;
    }
}