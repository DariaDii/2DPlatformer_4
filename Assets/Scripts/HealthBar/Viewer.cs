using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Start()
    {
        ChangeHealthIndicator(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnEnable()
    {
        _health.ValueChange += ChangeHealthIndicator;
    }

    private void OnDisable()
    {
        _health.ValueChange -= ChangeHealthIndicator;
    }

    protected virtual void ChangeHealthIndicator(float currentValue, float maxValue) { }
}
