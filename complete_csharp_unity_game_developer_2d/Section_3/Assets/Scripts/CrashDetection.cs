using UnityEngine;

public class CrashDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("deudly ice my doge");
        }
    }
}
