using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _scoreUpdatedEventChannel;
    [SerializeField] private ScoreSO _scoreScriptableObject;

    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();

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

    private void HandleScoreUpdatedEvent()
    {
        // Debug.Log("Score Update Event received!");
        _text.text = "<mspace=1em>" + _scoreScriptableObject.Score.ToString().PadLeft(12, '0') + "</mspace>";
    }
}

