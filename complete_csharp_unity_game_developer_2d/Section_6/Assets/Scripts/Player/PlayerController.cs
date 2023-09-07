using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _paddingLeft;
    [SerializeField] private float _paddingRight;
    [SerializeField] private float _paddingTop;
    [SerializeField] private float _paddingBottom;


    private Vector2 _moveInput;
    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private Shooter _shooter;

    private void Start()
    {
        InitializeBounds();
        _shooter = GetComponent<Shooter>();
    }
    private void Update()
    {
        Vector2 calculatedPosition = _moveSpeed * Time.deltaTime * _moveInput;
        Vector2 newPosition = new()
        {
            // Note: These are in world space units, not pixels!
            x = Mathf.Clamp(transform.position.x + calculatedPosition.x, _minBounds.x + _paddingLeft, _maxBounds.x - _paddingRight),
            y = Mathf.Clamp(transform.position.y + calculatedPosition.y, _minBounds.y + _paddingBottom, _maxBounds.y - _paddingTop)
        };
        transform.position = newPosition;
    }
    private void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }

    private void OnFire(InputValue inputValue)
    {
        if (_shooter != null)
        {
            // Debug.Log("Firing! " + inputValue.isPressed);
            _shooter.IsFiring = inputValue.isPressed;
        }
    }

    private void InitializeBounds()
    {
        _minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // Debug.Log("Min = " + minBounds.ToString());
        // Debug.Log("Max = " + maxBounds.ToString());
    }

}
