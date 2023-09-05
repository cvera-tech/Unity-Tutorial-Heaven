using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
        {
            health.Subtract(_damage);
            Destroy(gameObject);
        }
    }
}
