using UnityEngine;

internal class AttackController : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit enemy = collision.GetComponent<Unit>();
        enemy.ApplyDamage(_damage);
    }
}
