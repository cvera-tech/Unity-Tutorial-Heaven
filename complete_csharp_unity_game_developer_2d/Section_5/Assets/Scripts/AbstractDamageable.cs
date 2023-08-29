using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractDamageable : MonoBehaviour
{
    [SerializeField] private int hitpoints;
    [SerializeField] private int maxHitpoints = 1;

    private void OnEnable() => hitpoints = maxHitpoints;

    public void ReceiveDamage(int amount)
    {
        hitpoints -= amount;
        if (hitpoints <= 0)
            OnZeroHP();
    }

    protected abstract void OnZeroHP();
}
