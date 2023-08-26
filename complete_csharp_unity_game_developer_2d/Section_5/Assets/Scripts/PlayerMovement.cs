using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator animator;
    private CapsuleCollider2D cc2d;

    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    private float defaultGravityScale;


    // private bool isTouchingLadder = false;
    
    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private readonly string isRunning = "isRunning";
    private readonly string ladderLayer = "Ladder";
    private readonly string groundLayer = "Ground";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();

        defaultGravityScale = rb2d.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && IsTouchingLayer(groundLayer))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (HasHorizontalMoveInput())
            facingRight = moveInput.x > Mathf.Epsilon;
    }

    /*
        Custom methods
    */

    private void ClimbLadder()
    {
        if (IsTouchingLayer(ladderLayer))
        {
            rb2d.gravityScale = 0;
            if (Mathf.Abs(moveInput.y) > Mathf.Epsilon)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            }
        }
        else
            rb2d.gravityScale = defaultGravityScale;

        
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }

    private bool HasHorizontalMoveInput() => Mathf.Abs(moveInput.x) > Mathf.Epsilon;

    private bool IsTouchingLayer(string layerMaskString) => cc2d.IsTouchingLayers(LayerMask.GetMask(layerMaskString));

    private void Run()
    {
        Vector2 runVelocity = new(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = runVelocity;
        
        animator.SetBool(isRunning, HasHorizontalMoveInput());
    }

    

}
