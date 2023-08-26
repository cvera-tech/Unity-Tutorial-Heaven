using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator animator;

    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private bool facingRight = true;
    
    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private readonly string isRunning = "isRunning";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (HasHorizontalMoveInput())
            facingRight = moveInput.x > Mathf.Epsilon;
    }


    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }
    
    private void Run()
    {
        Vector2 runVelocity = new(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = runVelocity;
        
        animator.SetBool(isRunning, HasHorizontalMoveInput());
    }

    private bool HasHorizontalMoveInput() => Mathf.Abs(moveInput.x) > Mathf.Epsilon;
}
