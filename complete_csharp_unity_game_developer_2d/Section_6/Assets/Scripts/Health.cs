using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    [Header("Audio")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField, Range(0f, 1f)] private float _audioVolume;

    public void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Add(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }

    public void Subtract(int amount)
    {
        Add(-amount);
        if (_currentHealth == 0)
        {
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
