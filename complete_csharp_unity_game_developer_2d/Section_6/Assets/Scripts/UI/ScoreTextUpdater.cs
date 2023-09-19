using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _scoreUpdatedEventChannel;
    [SerializeField] private ScoreManagerSO _scoreScriptableObject;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (_scoreUpdatedEventChannel != null)
        {
            _scoreUpdatedEventChannel.Subscribe(HandleScoreUpdatedEvent);
        }
        else
        {
            Debug.LogWarning(this.name + " has no assigned Score Updated Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_scoreUpdatedEventChannel != null)
        {
            _scoreUpdatedEventChannel.Unsubscribe(HandleScoreUpdatedEvent);
        }
    }

    private void Start()
    {
        // Set initial score text
        HandleScoreUpdatedEvent();
    }

    private void HandleScoreUpdatedEvent()
    {
        // Debug.Log("Score Update Event received!");
        _text.text = "<mspace=1em>" + _scoreScriptableObject.Score.ToString().PadLeft(12, '0') + "</mspace>";
    }
}

