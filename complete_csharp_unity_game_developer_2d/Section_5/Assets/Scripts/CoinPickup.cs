using UnityEngine;

public class CoinPickup : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
