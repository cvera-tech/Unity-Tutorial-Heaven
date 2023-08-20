using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            SceneManager.LoadScene("Level1");
            Debug.Log("deudly ice my doge");
        }
    }
}
