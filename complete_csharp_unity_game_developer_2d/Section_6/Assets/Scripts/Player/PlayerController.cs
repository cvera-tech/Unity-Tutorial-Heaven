using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;

    private Vector2 moveInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;


    private void Start()
    {
        InitializeBounds();
    }
    private void Update()
    {
        Vector2 calculatedPosition = moveSpeed * Time.deltaTime * moveInput;
        Vector2 newPosition = new()
        {
            // Note: These are in world space units, not pixels!
            x = Mathf.Clamp(transform.position.x + calculatedPosition.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + calculatedPosition.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop)
        };
        transform.position = newPosition;
    }
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
    private void InitializeBounds()
    {
        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Debug.Log("Min = " + minBounds.ToString());
        Debug.Log("Max = " + maxBounds.ToString());
    }
}
