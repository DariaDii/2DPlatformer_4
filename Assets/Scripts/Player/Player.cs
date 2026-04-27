using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private Attack _attack;
    [SerializeField] private Vampirism _vampirism;

    private void Update()
    {
        if (_inputReader.GetIsAttack())
        {
            _attack.Attacking();
        }
        if (_inputReader.GetIsVampirism())
        {
            _vampirism.StartAbility();
        }
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _rotator.Rotate(_inputReader.Direction);            
        }

        _mover.Move(_inputReader.Direction);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
            _inputReader.ResetJumpFlag();
        }            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out _))
        {
            _wallet.AddCoin();
        }

        if (collision.gameObject.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            collectible.Collect(_health);
        }

        if (collision.gameObject.TryGetComponent<EnemyAttack>(out EnemyAttack enemyAttack))
        {
            _health.TakeDamage(enemyAttack.DamageAmount);
        }
    }
}