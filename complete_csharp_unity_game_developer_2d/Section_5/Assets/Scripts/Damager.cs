using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    [Tooltip("These are the layers that this GameObject can deal damage to.")]
    [SerializeField] private LayerMask layersToDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        
        // Check if the colliding object is within the layer mask
        if ((1 << otherGameObject.layer & layersToDamage) == 0)
            return;
        
        if (otherGameObject.TryGetComponent<Damageable>(out var otherDamageable))
            otherDamageable.ReceiveDamage(damage);
    }
}
