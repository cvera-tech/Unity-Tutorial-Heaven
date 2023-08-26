using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;

    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private bool facingRight = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
            facingRight = moveInput.x > Mathf.Epsilon;
        Debug.Log("facingRight: " + facingRight);
    }

    private void Run()
    {
        Vector2 runVelocity = new(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = runVelocity;
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }
}
