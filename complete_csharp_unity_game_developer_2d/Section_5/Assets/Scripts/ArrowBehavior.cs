using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float speed = 10f;

    private Rigidbody2D arrowRigidbody;

    private void OnEnable()
    {
        arrowRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        transform.SetParent(otherGameObject.transform);

        // Stop the physics simulation to prevent sticking to walls after hitting an enemy
        arrowRigidbody.simulated = false;

        Destroy(gameObject, lifetime);
    }

    /*
        Custom methods
     */
    
    public void MoveRight(bool moveRight)
    {
        Vector2 arrowVector = new((moveRight ? 1f : -1f) * speed, 0f);
        arrowRigidbody.velocity = arrowVector;
    }
}
