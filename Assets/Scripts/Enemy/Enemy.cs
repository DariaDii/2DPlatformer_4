using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyDetection _detection;
    [SerializeField] private EnemyAttack _attack;

    private void Update()
    {
        _movement.UpdateEnemy(_detection.PlayerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Sword>(out Sword playerAttack))
        {
            _health.TakeDamage(playerAttack.DamageAmount);
        }
    }
}