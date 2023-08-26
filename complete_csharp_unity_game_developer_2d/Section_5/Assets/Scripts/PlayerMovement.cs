using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;

    [SerializeField] private float runSpeed = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    private void Run()
    {
        Vector2 runVelocity = new(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = runVelocity;
    }
}
