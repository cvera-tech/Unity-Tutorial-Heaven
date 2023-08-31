using UnityEngine;

[CreateAssetMenu(menuName = "Session Data")]
public class SessionDataSO : ScriptableObject
{
    // TODO: _maxPlayerHealth doesn't fit here... maybe move it and _playerHealth to a separate ScriptableObject?
    [SerializeField] private int _playerHealth;
    [SerializeField] private int _maxPlayerHealth;
    [SerializeField] private int _score;

    public int PlayerHealth {
        get => _playerHealth;
        set => _playerHealth = value;
    }

    public int Score {
        get => _score;
        set => _score = value;
    }

    private void OnEnable()
    {
        // This ensures that when we launch a new game, we set the correct player health
        // instead of whatever was saved from the last session.
        _playerHealth = _maxPlayerHealth;
        Score = 0;
    }
}
