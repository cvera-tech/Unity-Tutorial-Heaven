using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] ParticleEventChannelSO _particleEventChannel;
    [Tooltip("If set, this component will send raise events on this channel when damage is dealt.")]
    [SerializeField] private VoidEventChannelSO _cameraShakeEventChannel;
    
    // TODO: Create an audio properties scriptable object to hold these fields
    [Header("Audio")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField, Range(0f, 1f)] private float _audioVolume;

    public int Damage { get => _damage; set => _damage = value; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
        {
            health.Subtract(Damage);
            if (_particleEventChannel != null)
            {
                _particleEventChannel.RaiseEvent(collider);
            }
            if (_cameraShakeEventChannel != null)
            {
                _cameraShakeEventChannel.RaiseEvent();
            }
            SendAudioEvent();
            Destroy(gameObject);
        }
    }

    private void SendAudioEvent()
    {
        if (_audioEventChannel != null && _audioClip != null)
        {
            _audioEventChannel.RaiseEvent(_audioClip, _audioVolume);
        }
    }
}
