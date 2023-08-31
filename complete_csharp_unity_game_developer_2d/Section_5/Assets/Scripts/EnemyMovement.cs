using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float turningPauseDuration = 2f;

    private bool isMoving = true;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Animator animator;

    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private readonly string platformsTag = "Ground";
    private readonly string isHopping = "isHopping";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FlipSprite();
        MoveForward();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!bc2d.IsTouchingLayers(LayerMask.GetMask(platformsTag)))
        {
            isMoving = false;
            Invoke(nameof(TurnAround), turningPauseDuration);
        }
    }


    /*
        Custom methods
     */

    private void TurnAround()
    {
        facingRight = !facingRight;
        isMoving = true;
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }

    private void MoveForward()
    {
        if (isMoving)
        {
            float moveDirection = facingRight ? 1f : -1f;
            rb2d.velocity = new Vector2(moveSpeed * moveDirection, 0f);
            animator.SetBool(isHopping, true);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            animator.SetBool(isHopping, false);
        }
    }

}
