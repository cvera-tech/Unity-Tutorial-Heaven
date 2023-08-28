using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int hitpoints = 1;
    [SerializeField] private float destroyDelay = 0f;

    public void ReceiveDamage(int amount)
    {
        hitpoints -= amount;
        Debug.Log("Hit! Hitpoints = " + hitpoints);
        if (hitpoints <= 0)
            Destroy(gameObject, destroyDelay);
    }
}
