using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private EnemyAttack _attack;

    private Transform _currentPoint;
    private float _distanceToTargetPoint = 0.15f;
    private bool _isMoving = false;
    private bool _playerInZone = false;

    public event Action<bool> Patrolling;
    public event Action Attacking;

    private void Start()
    {
        _currentPoint=_startPoint;
    }

    public void UpdateEnemy(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            _playerInZone = true; 
            Vector3 toPlayer = playerTransform.position - transform.position;
            float distance = toPlayer.sqrMagnitude;

            if (distance < _attackRange * _attackRange)
            {
                Attacking?.Invoke();
                _attack.gameObject.SetActive(true);
            }
            else
            {
                _attack.gameObject.SetActive(false);
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, _speed * Time.deltaTime);
                _currentPoint = playerTransform;
                TurnToCurrentPoint();
            }
        }
        else
        {
            if (_playerInZone)
            {
                _playerInZone = false;
                _currentPoint = _startPoint;
            }

            Patroll();
        }
    }

    private void Patroll()
    {
        float distance = (transform.position - _currentPoint.position).sqrMagnitude;
        bool wasMoving = _isMoving;
        _isMoving = true;

        if (distance > _distanceToTargetPoint * _distanceToTargetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);
        }
        else
        {
            if (_currentPoint == _startPoint)
                _currentPoint = _endPoint;
            else
                _currentPoint = _startPoint;

            _isMoving = false;
        }

        if (wasMoving != _isMoving)
        {
            Patrolling?.Invoke(_isMoving);
        }

        TurnToCurrentPoint();
    }

    private void TurnToCurrentPoint()
    {
        _spriteRenderer.flipX = _currentPoint.position.x > transform.position.x;
    }
}
