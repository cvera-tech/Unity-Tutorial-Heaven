using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO _scoreChangeEventChannel;
    [SerializeField] private VoidEventChannelSO _scoreResetEventChannel;
    [SerializeField] private VoidEventChannelSO _scoreUpdatedEventChannel;
    [SerializeField] private ScoreManagerSO _scoreScriptableObject;

    private void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
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
        // _scoreScriptableObject.Score += scoreValue;
        SendScoreUpdatedEvent();
        // Debug.Log(_scoreScriptableObject.Score);
    }

    private void HandleScoreResetEvent()
    {
        // _scoreScriptableObject.Score = 0;
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
