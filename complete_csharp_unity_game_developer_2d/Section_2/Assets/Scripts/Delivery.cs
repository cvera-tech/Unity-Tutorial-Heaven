using UnityEngine;

public class Delivery : MonoBehaviour
{
    void OnCollisionEnter2D()
    {
        Debug.Log("Oof.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package"))
        {
            Debug.Log("Package acquired.");
        }
        else if (other.CompareTag("Customer"))
        {
            Debug.Log("Package delivered.");
        }
    }
}
