using UnityEngine;

public class Delivery : MonoBehaviour
{

    private bool hasPackage = false;
    void OnCollisionEnter2D()
    {
        Debug.Log("Oof.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !hasPackage)
        {
            hasPackage = true;
            Debug.Log("Package acquired.");
        }
        else if (other.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            Debug.Log("Package delivered.");
        }
    }
}
