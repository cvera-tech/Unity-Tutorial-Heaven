using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] ParticleEventChannelSO _particleEventChannel;
    [Tooltip("If set, this component will send raise events on this channel when damage is dealt.")]
    [SerializeField] private VoidEventChannelSO _cameraShakeEventChannel;

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
            Destroy(gameObject);
        }
    }
}
