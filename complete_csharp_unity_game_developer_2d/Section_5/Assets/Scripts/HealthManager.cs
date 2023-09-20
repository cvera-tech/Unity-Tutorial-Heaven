using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;

    protected virtual void OnEnable() => currentHealth = maxHealth;

    public virtual void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
            OnZeroHealth();
    }

    public virtual void OnZeroHealth() => Destroy(gameObject);

    public void ResetHealth() => currentHealth = maxHealth;

}