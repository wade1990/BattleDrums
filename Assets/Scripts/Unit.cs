using System;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
internal abstract class Unit : MonoBehaviour
{
    private HealthController _healthController;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    public void MoveForward()
    {
        throw new NotImplementedException();
    }

    public void MoveBackWards()
    {
        throw new NotImplementedException();
    }

    public void StopMoving()
    {
        throw new NotImplementedException();
    }

    public abstract void PerformAction();

    public void ApplyDamage(float damage)
    {
        _healthController.Health -= damage;
    }
}