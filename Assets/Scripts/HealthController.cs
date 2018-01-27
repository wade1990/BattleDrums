using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls the health of a unit.
/// </summary>
internal class HealthController : MonoBehaviour
{
    /// <summary>
    /// The current value of the health.
    /// </summary>
    [SerializeField] private float _health;

    /// <summary>
    /// Wether currently alive or not.
    /// </summary>
    [SerializeField] private bool alive;

    /// <summary>
    /// Event invoked when this unit dies.
    /// </summary>
    [SerializeField] private UnityEvent _died;

    /// <summary>
    /// The current value of the health.
    /// </summary>
    public float Health
    {
        get { return _health; }
        set
        {
            if (!alive)
                return;

            _health = value;
            if (_health <= 0)
                Die();
        }
    }
    
    /// <summary>
    /// Wether currently alive or not.
    /// </summary>
    public bool Alive { get { return alive; } }

    /// <summary>
    /// Event invoked when this unit dies.
    /// </summary>
    public event Action<HealthController> Died;

    /// <summary>
    /// Makes the unit die.
    /// </summary>
    public void Die()
    {
        alive = false;

        _died.Invoke();
        if(Died != null)
            Died.Invoke(this);
    }
}