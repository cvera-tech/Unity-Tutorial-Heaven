using UnityEngine;

[CreateAssetMenu(menuName = "Score Manager")]
public class ScoreManagerSO : ScriptableObject
{
    [SerializeField] private SessionDataSO _sessionData;

    [Tooltip("This channel if for subscribing to score change events.")]
    [SerializeField] IntEventChannelSO _scoreChangeChannel;

    [Tooltip("This channel is for notifying subscribers that the score has updated.")]
    [SerializeField] VoidEventChannelSO _scoreUpdatedChannel;

    private void OnEnable()
    {
        if (_scoreChangeChannel != null)
        {
            _scoreChangeChannel.OnEventRaised += HandleScoreChange;
        }
        else
        {
            Debug.LogWarning("ScoreManager was not assigned a Score Change Channel. "
                + "Please assign a channel to properly update the score in the Session Data.");
        }
        if (_scoreUpdatedChannel == null)
        {
            Debug.LogWarning("ScoreManager was not assigned a Score Updated Channel. "
                + "Please assign a channel to properly notify subscibers of score updates.");
        }
    }

    private void OnDisable()
    {
        if (_scoreChangeChannel != null)
        {
            _scoreChangeChannel.OnEventRaised -= HandleScoreChange;
        }
    }

    private void HandleScoreChange(int amount)
    {
        Debug.Log("[ScoreManagerSO] Score Change Event received!");
        _sessionData.Score += amount;

        if (_scoreUpdatedChannel != null)
        {
            _scoreUpdatedChannel.RaiseEvent();
            Debug.Log("[ScoreManagerSO] Score Updated Event raised!");
        }
    }
}
