using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator animator;
    private CapsuleCollider2D cc2d;
    private CircleCollider2D feetCollider;
    private PlayerHealthManager _playerHealthManager;

    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField, Min(0)] private float knockbackHorizontalMultiplier = 5f;
    [SerializeField, Min(0)] private float knockbackVerticalSpeed = 5f;

    // TODO: Should these be in a different component attached to the bow game object?
    [Header("Weapon")]
    [SerializeField] private Transform bow;
    [SerializeField] private GameObject arrow;

    [Header("Event Channel Listeners")]
    [Tooltip("This channel raises events containing collisions that damage the player.")]
    [SerializeField] private Collision2DEventChannelSO _damageSourcePositionChannel;

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
    private readonly string enemyTag = "Enemy";
    private readonly string hazardTag = "Hazard";


    private void OnEnable()
    {
        if (_damageSourcePositionChannel != null)
            _damageSourcePositionChannel.OnEventRaised += HandleDamagingCollision;
        else
            Debug.LogWarning("PlayerMovement was not assigned a Enemy Collided Channel."
                + "Please assign a channel to enable proper player death physics.");
    }
    private void OnDisable()
    {
        if (_damageSourcePositionChannel != null)
            _damageSourcePositionChannel.OnEventRaised -= HandleDamagingCollision;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        _playerHealthManager = GetComponent<PlayerHealthManager>();

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

    private void HandleDamagingCollision(Collision2D collision, GameObject source)
    {
        if (source.CompareTag(enemyTag))
        {
            Vector3 collisionDirection = (source.GetComponent<Rigidbody2D>().transform.position - rb2d.transform.position).normalized;
            Die(collisionDirection);
        }
        else if (source.CompareTag(hazardTag))
        {
            // We need to do it this way because the hazards tilemap's transform is at (0, 0, 0)
            Vector2 collisionDirection = (collision.GetContact(0).point - new Vector2(rb2d.transform.position.x, rb2d.transform.position.y)).normalized;
            Die(collisionDirection);
        }
    }

    private bool HasHorizontalMoveInput() => Mathf.Abs(moveInput.x) > Mathf.Epsilon;

    private bool HasVerticalMoveInput() => Mathf.Abs(moveInput.y) > Mathf.Epsilon;

    private bool IsTouchingLayer(Collider2D collider, string layerMaskString) => collider.IsTouchingLayers(LayerMask.GetMask(layerMaskString));

    private void Run()
    {
        // This implementation prevents physics collisions from altering the rigidbody's velocity as long as the controls are enabled.
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
    }

    // TODO: Should this be in a different class? See the "Weapon" class members above.
    public void ShootArrow()
    {
        GameObject newArrow = Instantiate(arrow, bow.position, bow.rotation);
        FlipSprite(newArrow.transform);
        newArrow.GetComponent<ArrowBehavior>().MoveRight(facingRight);
    }
}
