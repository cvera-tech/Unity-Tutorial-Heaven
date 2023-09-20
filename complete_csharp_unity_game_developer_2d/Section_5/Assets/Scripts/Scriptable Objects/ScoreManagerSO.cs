using UnityEngine;

[CreateAssetMenu(menuName = "Score Manager")]
public class ScoreManagerSO : ScriptableObject
{
    [SerializeField] private SessionDataSO _sessionData;

    [Tooltip("This channel if for subscribing to score change events.")]
    [SerializeField] IntEventChannelSO _changeScoreChannel;

    [Tooltip("This channel is for notifying subscribers that the score has updated.")]
    [SerializeField] VoidEventChannelSO _scoreUpdatedChannel;

    [Tooltip("This channel is for subscribing to requests to reset the score.")]
    [SerializeField] VoidEventChannelSO _resetScoreChannel;

    private void OnEnable()
    {
        if (_changeScoreChannel != null)
        {
            _changeScoreChannel.OnEventRaised += ChangeScore;
        }
        else
        {
            Debug.LogWarning("ScoreManager was not assigned a Change Score Channel. "
                + "Please assign a channel to properly update the score in the Session Data.");
        }
        if (_resetScoreChannel != null)
        {
            _resetScoreChannel.OnEventRaised += ResetScore;
        }
        else
        {
            Debug.LogWarning("ScoreManager was not assigned a Reset Score Channel. "
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
        if (_changeScoreChannel != null)
        {
            _changeScoreChannel.OnEventRaised -= ChangeScore;
        }
    }

    private void ChangeScore(int amount)
    {
        Debug.Log("[ScoreManagerSO] Score Change Event received!");
        _sessionData.Score += amount;

        if (_scoreUpdatedChannel != null)
        {
            _scoreUpdatedChannel.RaiseEvent();
            Debug.Log("[ScoreManagerSO] Score Updated Event raised!");
        }
    }

    private void ResetScore()
    {
        _sessionData.Score = 0;
    }
}
