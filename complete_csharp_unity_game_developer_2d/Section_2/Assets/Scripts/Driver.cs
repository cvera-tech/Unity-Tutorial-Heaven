using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float steerSpeed = 150;
    [SerializeField] float moveSpeed = 12;

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}
