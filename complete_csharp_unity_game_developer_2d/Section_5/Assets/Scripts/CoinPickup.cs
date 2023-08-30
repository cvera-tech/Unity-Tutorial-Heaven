using UnityEngine;

public class CoinPickup : MonoBehaviour, Interactable
{
    [SerializeField] private AudioClip _audioClip;

    // TODO: Convert this to an event-based system
    public void Interact()
    {
        Vector3 coinPosition = gameObject.transform.position;
        AudioSource.PlayClipAtPoint(_audioClip, coinPosition);
        // Vector3 audioPosition = new Vector3(coinPosition.x, coinPosition.y, Camera.main.transform.position.z);
        // AudioSource.PlayClipAtPoint(_audioClip, audioPosition);
        Destroy(gameObject);
    }
}
