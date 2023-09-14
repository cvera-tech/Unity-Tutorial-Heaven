using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO _scoreChangeEventChannel;
    [SerializeField] private ScoreSO _scoreScriptableObject;

    private void OnEnable()
    {
        if (_scoreChangeEventChannel != null)
        {
            _scoreChangeEventChannel.Subscribe(HandleScoreChangeEvent);
        }
        else
        {
            Debug.LogWarning(this.name + " has no assigned Score Change Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_scoreChangeEventChannel != null)
        {
            _scoreChangeEventChannel.Unsubscribe(HandleScoreChangeEvent);
        }
    }

    private void HandleScoreChangeEvent(int scoreValue)
    {
        _scoreScriptableObject.Score += scoreValue;
        Debug.Log(_scoreScriptableObject.Score);
    }
}
