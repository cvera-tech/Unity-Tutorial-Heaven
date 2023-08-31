using UnityEngine;

public class CoinPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _scoreValue;
    [SerializeField] private IntEventChannelSO _scoreChangeChannel;

    // TODO: Convert this to an event-based system
    public void Interact()
    {
        Debug.Log("[CoinPickup] Interact! Score Change Channel = " + (_scoreChangeChannel != null).ToString());
        Vector3 coinPosition = gameObject.transform.position;
        AudioSource.PlayClipAtPoint(_audioClip, coinPosition);
        // Vector3 audioPosition = new Vector3(coinPosition.x, coinPosition.y, Camera.main.transform.position.z);
        // AudioSource.PlayClipAtPoint(_audioClip, audioPosition);
        if (_scoreChangeChannel != null)
        {
            _scoreChangeChannel.RaiseEvent(_scoreValue);
            Debug.Log("[CoinPickup] Score Change Event raised!");
        }
        Destroy(gameObject);
    }
}
