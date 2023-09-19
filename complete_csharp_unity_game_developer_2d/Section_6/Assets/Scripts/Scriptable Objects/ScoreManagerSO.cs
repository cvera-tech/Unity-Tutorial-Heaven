using UnityEngine;

[CreateAssetMenu(menuName = "Score Manager")]
public class ScoreManagerSO : ScriptableObject
{
    [SerializeField] private IntEventChannelSO _scoreChangeEventChannel;
    [SerializeField] private VoidEventChannelSO _scoreResetEventChannel;
    [SerializeField] private VoidEventChannelSO _scoreUpdatedEventChannel;

    private int _score;

    public int Score { get => _score; private set => _score = value; }

    private void OnEnable()
    {
        if (_scoreChangeEventChannel != null)
        {
            _scoreChangeEventChannel.Subscribe(HandleScoreChangeEvent);
        }
        else
        {
            Debug.LogWarning(name + " has no assigned Score Change Event Channel!");
        }
        
        if (_scoreResetEventChannel != null)
        {
            _scoreResetEventChannel.Subscribe(HandleScoreResetEvent);
        }
        else
        {
            Debug.LogWarning(name + " has no assigned Score Reset Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_scoreChangeEventChannel != null)
        {
            _scoreChangeEventChannel.Unsubscribe(HandleScoreChangeEvent);
        }
        if (_scoreResetEventChannel != null)
        {
            _scoreResetEventChannel.Unsubscribe(HandleScoreResetEvent);
        }
    }

    private void HandleScoreChangeEvent(int scoreValue)
    {
        Score += scoreValue;
        SendScoreUpdatedEvent();
        // Debug.Log(_scoreScriptableObject.Score);
    }

    private void HandleScoreResetEvent()
    {
        Score = 0;
        SendScoreUpdatedEvent();
    }

    private void SendScoreUpdatedEvent()
    {
        if (_scoreUpdatedEventChannel != null)
        {
            _scoreUpdatedEventChannel.RaiseEvent();
        }
    }
}
