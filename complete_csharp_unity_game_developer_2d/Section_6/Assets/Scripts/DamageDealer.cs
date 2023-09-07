using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage { get => _damage; set => _damage = value; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
        {
            Debug.Log("HIT");
            health.Subtract(Damage);
            Destroy(gameObject);
        }
    }
}
