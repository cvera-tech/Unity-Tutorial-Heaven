using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _shouldUseScriptableObject = false;
    [SerializeField] private HealthSO _healthScriptableObject;
    [SerializeField] private VoidEventChannelSO _healthUpdatedEventChannel;


    [Header("Audio")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField, Range(0f, 1f)] private float _audioVolume;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    private ScoreValue _scoreValue;

    private void Start()
    {
        if (_shouldUseScriptableObject)
        {
            if (_healthScriptableObject == null)
            {
                Debug.LogWarning(this.name + " has no assigned HealthSO!");
            }
            else
            {
                _healthScriptableObject.Value = _healthScriptableObject.MaxValue;
            }
            
            if (_healthUpdatedEventChannel == null)
            {
                Debug.LogWarning(this.name + " has no assigned Health Updated Event Channel!");
            }
        }
        _currentHealth = _maxHealth;


        // TODO: Decouple this?
        TryGetComponent(out _scoreValue);
    }

    public void Add(int amount)
    {
        if (_shouldUseScriptableObject)
        {
            _healthScriptableObject.Value = Mathf.Clamp(
                _healthScriptableObject.Value + amount,
                0,
                _healthScriptableObject.MaxValue
            );

            SendHealthUpdatedEvent();
        }
        else
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        }
    }

    public void Subtract(int amount)
    {
        Add(-amount);
        int health = _shouldUseScriptableObject ? _healthScriptableObject.Value : _currentHealth;
        if (health == 0)
        {
            SendAudioEvent();
            if (_scoreValue != null)
            {
                _scoreValue.SendScoreChangeEvent();
            }
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

    private void SendHealthUpdatedEvent()
    {
        if (_healthUpdatedEventChannel != null)
        {
            _healthUpdatedEventChannel.RaiseEvent();
        }
    }
}
