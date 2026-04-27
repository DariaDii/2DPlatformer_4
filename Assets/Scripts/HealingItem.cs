using UnityEngine;

public class HealingItem : MonoBehaviour,ICollectible
{
    [field: SerializeField] public float HealingAmount { get; private set; }

    public void Collect(PlayerHealth playerHealth)
    {
        playerHealth.Heal(HealingAmount);
        Destroy(this.gameObject);
    }
}