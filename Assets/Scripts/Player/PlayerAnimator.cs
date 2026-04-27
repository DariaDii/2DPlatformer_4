using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public const string IsRun = "IsRun";
    public const string Attack = "Attack";
    public const string Damage = "Damage";

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;

    private const float moveThreshold = 0.01f;    

    private void OnEnable()
    {
        _inputReader.Moving += UpdateRunAnimation;
        _inputReader.Attacking += StartAttack;
        _health.ReceivingDamage += StartDamage;
    }

    private void OnDisable()
    {
        _inputReader.Moving -= UpdateRunAnimation;
        _inputReader.Attacking -= StartAttack;
        _health.ReceivingDamage -= StartDamage;
    }

    private void UpdateRunAnimation(float direction)
    {
        bool isRunning = Mathf.Abs(direction) > moveThreshold;
        _animator.SetBool(IsRun,isRunning);
    } 
    private void StartAttack()
    {
        _animator.SetTrigger(Attack);
    }

    private void StartDamage()
    {
        _animator.SetTrigger(Damage);
    }
}