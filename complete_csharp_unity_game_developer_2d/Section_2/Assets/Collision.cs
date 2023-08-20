using UnityEngine;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter2D() {
        Debug.Log("Oof.");
    }

    void OnTriggerEnter2D() {
        Debug.Log("Rough ride!");
    }
}
