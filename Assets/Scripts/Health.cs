using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }

    public event Action Death;
    public event Action ReceivingDamage;
    public event Action<float, float> ValueChange;

    protected float HealthAmount { get; set; }

    public float CurrentHealth => HealthAmount;

    private void Awake()
    {
        HealthAmount = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        ReceivingDamage?.Invoke();
        HealthAmount -= damage;
        if (HealthAmount < 0)
            HealthAmount = 0;
        InvokeValueChange();

        if (HealthAmount == 0)
            Die();
    }

    public virtual void Die()
    {
        Death?.Invoke();
    }

    protected void InvokeValueChange()
    {
        ValueChange?.Invoke(HealthAmount, MaxHealth);
    }
}