
using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private SessionDataSO _sessionData;
    [SerializeField] private VoidEventChannelSO _scoreUpdatedChannel;

    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        if (_scoreUpdatedChannel != null)
        {
            _scoreUpdatedChannel.OnEventRaised += HandleScoreUpdated;
        }
        else
        {
            Debug.LogWarning("ScoreTextUpdater was not assigned a Score Updated Channel. "
                + "Please assign a channel to properly display the score.");
        }
    }
    
    private void OnDisable()
    {
        if (_scoreUpdatedChannel != null)
        {
            _scoreUpdatedChannel.OnEventRaised -= HandleScoreUpdated;
        }
    }

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = _sessionData.Score.ToString();
    }

    private void HandleScoreUpdated()
    {
        Debug.Log("Updating Score Text!");
        _text.text = _sessionData.Score.ToString();
    }
}
