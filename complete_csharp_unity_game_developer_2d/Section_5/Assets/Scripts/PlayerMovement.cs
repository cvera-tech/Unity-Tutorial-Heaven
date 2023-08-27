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
    private CircleCollider2D feetCollider;

    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    private float defaultGravityScale;
    private bool isAlive;

    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private readonly string isClimbing = "isClimbing";
    private readonly string isRunning = "isRunning";
    private readonly string ladderLayer = "Ladder";
    private readonly string groundLayer = "Ground";
    private readonly string enemyTag = "Enemy";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<CircleCollider2D>();

        defaultGravityScale = rb2d.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive)
            return;

        if (value.isPressed && IsTouchingLayer(cc2d, groundLayer) && IsTouchingLayer(feetCollider, groundLayer))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive)
            return;

        moveInput = value.Get<Vector2>();
        if (HasHorizontalMoveInput())
            facingRight = moveInput.x > Mathf.Epsilon;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
            isAlive = false;
    }

    /*
        Custom methods
    */

    private void ClimbLadder()
    {
        if (IsTouchingLayer(cc2d, ladderLayer))
        {
            rb2d.gravityScale = 0;
            if (HasVerticalMoveInput())
                rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            else
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            
            animator.SetBool(isClimbing, HasVerticalMoveInput());
        }
        else
        {
            rb2d.gravityScale = defaultGravityScale;
            // This fixes the bug where leaving the ladder while holding down a
            // climb input causes the animator to be stuck in the climbing state
            animator.SetBool(isClimbing, false);
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }

    private bool HasHorizontalMoveInput() => Mathf.Abs(moveInput.x) > Mathf.Epsilon;

    private bool HasVerticalMoveInput() => Mathf.Abs(moveInput.y) > Mathf.Epsilon;

    private bool IsTouchingLayer(Collider2D collider, string layerMaskString) => collider.IsTouchingLayers(LayerMask.GetMask(layerMaskString));

    private void Run()
    {
        Vector2 runVelocity = new(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = runVelocity;
        
        animator.SetBool(isRunning, HasHorizontalMoveInput());
    }

    

}
