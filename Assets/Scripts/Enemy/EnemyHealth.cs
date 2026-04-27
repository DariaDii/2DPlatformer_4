using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SmoothHealthBarViewer _healthBar;

    public override void Die()
    {
        base.Die();
        DisableComponents();
    }

    private void DisableComponents()
    {
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            component.enabled = false;
        }

        _rigidbody.simulated = false;
        _healthBar.gameObject.SetActive(false);
    }
}