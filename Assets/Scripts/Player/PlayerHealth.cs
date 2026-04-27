public class PlayerHealth : Health
{
    public void Heal(float amount)
    {
        HealthAmount += amount;
        if (HealthAmount > MaxHealth)
            HealthAmount = MaxHealth;

        InvokeValueChange();
    }
}