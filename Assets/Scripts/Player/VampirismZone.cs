using System.Collections.Generic;
using UnityEngine;

public class VampirismZone : MonoBehaviour
{
    [SerializeField] private float _damageInterval = 0.1f;
    [SerializeField] private PlayerHealth _playerHealth;

    private List<EnemyHealth> _enemiesInZone = new List<EnemyHealth>();
    private bool _isActive = false;
    private float _damagePerSecond;
    private float _healPerDamage;
    private float _damageTimer;

    private void Update()
    {
        if (_isActive)
        {
            _damageTimer += Time.deltaTime;

            if (_damageTimer >= _damageInterval)
            {
                _damageTimer = 0f;
                DamageNearestEnemy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            if (!_enemiesInZone.Contains(enemy))
            {
                _enemiesInZone.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            if (_enemiesInZone.Contains(enemy))
            {
                _enemiesInZone.Remove(enemy);
            }
        }
    }

    public void StopVampirism()
    {
        _isActive = false;
        _enemiesInZone.Clear();
    }

    public void StartVampirism(float damagePerSecond, float healPerDamage)
    {
        _isActive = true;
        _damagePerSecond = damagePerSecond;
        _healPerDamage = healPerDamage;
    }

    private void DamageNearestEnemy()
    {
        _enemiesInZone.RemoveAll(enemy => enemy.CurrentHealth <= 0);
        EnemyHealth nearestEnemy = GetNearestEnemy();

        if (nearestEnemy != null)
        {
            float damagePerTick = _damagePerSecond * _damageInterval;
            nearestEnemy.TakeDamage(damagePerTick);

            float healAmount = damagePerTick * _healPerDamage;
            _playerHealth.Heal(healAmount);
        }
    }   

    private EnemyHealth GetNearestEnemy()
    {
        EnemyHealth nearestEnemy = null;
        float nearestDistance = float.MaxValue;
        Vector2 currentPosition = transform.position;

        foreach (var enemy in _enemiesInZone)
        {
            if (enemy == null) continue;

            Vector2 directionToEnemy = (Vector2)enemy.transform.position - currentPosition;
            float distance = directionToEnemy.sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }     
        }

        return nearestEnemy;
    }
}