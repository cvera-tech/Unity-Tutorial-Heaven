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
    [SerializeField, Min(0)] private float knockbackHorizontalMultiplier = 5f;
    [SerializeField, Min(0)] private float knockbackVerticalSpeed = 5f;


    [Header("Health")]
    [SerializeField] private PlayerHealthManager playerHealthManager;


    [Header("Weapon")]
    [SerializeField] private Transform bow;
    [SerializeField] private GameObject arrow;


    private float defaultGravityScale;
    private bool isAlive;

    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private readonly string isClimbing = "isClimbing";
    private readonly string isRunning = "isRunning";
    private readonly string isDead = "isDead";
    private readonly string ladderLayer = "Ladder";
    private readonly string groundLayer = "Ground";
    private readonly string enemyLayer = "Enemy";
    private readonly string hazardLayer = "Hazard";


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
        if (!isAlive)
            return;
        Run();
        FlipSprite(transform);
        ClimbLadder();
    }

    private void OnFire(InputValue value)
    {
        if (!isAlive)
            return;

        if (value.isPressed)
        {
            animator.Play("Fire");
        }
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

    // TODO: Remove and replace with new Damage system
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsTouchingLayer(cc2d, enemyLayer))
        {
            Vector3 collisionDirection = (collision.gameObject.GetComponent<Rigidbody2D>().transform.position - rb2d.transform.position).normalized;
            Die(collisionDirection);
        }
        else if (IsTouchingLayer(cc2d, hazardLayer))
        {
            // We need to do it this way because the hazards tilemap's transform is at (0, 0, 0)
            Vector2 collisionDirection = (collision.contacts[0].point - new Vector2(rb2d.transform.position.x, rb2d.transform.position.y)).normalized;
            Die(collisionDirection);
        }
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

    private void FlipSprite(Transform transformToFlip)
    {
        transformToFlip.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
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

    private void Die(Vector2 knockbackDirection)
    {
        isAlive = false;
        // This ensures the sprite is facing upwards
        if (facingRight)
            rb2d.SetRotation(90f);
        else
            rb2d.SetRotation(-90f);
        rb2d.velocity = -knockbackDirection * knockbackHorizontalMultiplier + new Vector2(0f, knockbackVerticalSpeed);
        animator.SetTrigger(isDead);
        
        // Resets friction to default (0.4)
        rb2d.sharedMaterial = null;

        // Removes collision with enemies
        rb2d.excludeLayers = LayerMask.GetMask(new string[] { enemyLayer, hazardLayer });

        playerHealthManager.ChangeHealth(-1);
    }

    public void ShootArrow()
    {
        GameObject newArrow = Instantiate(arrow, bow.position, bow.rotation);
        FlipSprite(newArrow.transform);
        newArrow.GetComponent<ArrowBehavior>().MoveRight(facingRight);
    }
}
