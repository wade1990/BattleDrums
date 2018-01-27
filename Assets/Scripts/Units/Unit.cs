using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Unit : MonoBehaviour
{
    public Vector2 ForwardDirection;
    public float Speed;

    private HealthController _healthController;
    protected AttackController AttackController;

    private Vector3 _moveDirection;

    private bool _isMoving;

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
        StartMoving();
    }

    public virtual void MoveBackWards()
    {
        _moveDirection = -ForwardDirection;
        StartMoving();
    }
    
    protected void StartMoving()
    {
        _isMoving = true;
    }
    
    public virtual void StopMoving()
    {
        _isMoving = false;
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