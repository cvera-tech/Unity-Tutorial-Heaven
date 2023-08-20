using UnityEngine;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter2D() {
        Debug.Log("Oof.");
    }
}
