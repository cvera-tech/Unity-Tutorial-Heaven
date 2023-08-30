using UnityEngine;

[CreateAssetMenu(menuName = "Session Data")]
public class SessionDataSO : ScriptableObject
{
    // TODO: _maxPlayerHealth doesn't fit here... maybe move it and _playerHealth to a separate ScriptableObject?
    [SerializeField] private int _playerHealth;
    [SerializeField] private int _maxPlayerHealth;

    public int PlayerHealth {
        get => _playerHealth;
        set => _playerHealth = value;
    }

    private void OnEnable()
    {
        // This ensures that when we launch a new game, we set the correct player health
        // instead of whatever was saved from the last session.
        _playerHealth = _maxPlayerHealth;
    }
}
