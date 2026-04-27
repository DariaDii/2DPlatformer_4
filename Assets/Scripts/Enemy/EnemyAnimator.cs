using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public const string IsWalk = "IsWalk";
    public const string Damage = "Damage";
    public const string Death = "Death";
    public const string Attack = "Attack";
    public const string AttackState = "AttackState";

    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMovement _enemy;
    [SerializeField] private Health _enemyHealth;

    private void OnEnable()
    {
        _enemy.Patrolling += UpdateWalkAnimation;
        _enemyHealth.Death += StartDeathAnimaion;
        _enemyHealth.ReceivingDamage += StartDamage;
        _enemy.Attacking += StartAttack;
    }

    private void OnDisable()
    {
        _enemy.Patrolling -= UpdateWalkAnimation;
        _enemyHealth.Death -= StartDeathAnimaion;
        _enemyHealth.ReceivingDamage -= StartDamage;
        _enemy.Attacking -= StartAttack;
    }

    private void UpdateWalkAnimation(bool isWalking)
    {
        _animator.SetBool(IsWalk, isWalking);
    }

    private void StartDeathAnimaion()
    {
        _animator.SetTrigger(Death);
    }

    private void StartDamage()
    {
        _animator.SetTrigger(Damage);
    }

    private void StartAttack()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(AttackState))
        {
            _animator.SetTrigger(Attack);
        }
    }
}
