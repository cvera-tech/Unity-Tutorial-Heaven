using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool facingRight = true;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    // TODO: Extract these magic strings to their own class
    // Perhaps a scriptable object?
    private string platformsTag = "Ground";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
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
            facingRight = !facingRight;
        }
    }


    /*
        Custom methods
     */
    private void FlipSprite()
    {
        transform.localScale = new Vector2(facingRight ? 1f : -1f, 1f);
    }

    private void MoveForward()
    {
        float moveDirection = facingRight ? 1f : -1f;
        rb2d.velocity = new Vector2(moveSpeed * moveDirection, 0f);
    }

}
