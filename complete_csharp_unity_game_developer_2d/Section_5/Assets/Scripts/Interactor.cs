using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact();
        }
    }
}
