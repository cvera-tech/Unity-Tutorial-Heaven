using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;

    private Vector2 moveInput;

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * (Vector3)moveInput;
    }
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
