using TMPro;
using UnityEngine;

public class LivesTextUpdater : MonoBehaviour
{
    [SerializeField] private SessionDataSO _sessionData;
    [SerializeField] private IntEventChannelSO _healthChangedChannel;

    private TextMeshProUGUI _livesText;

    private void OnEnable()
    {
        if (_healthChangedChannel != null)
        {
            _healthChangedChannel.OnEventRaised += HandleHealthChange;
        }
        else
        {
            Debug.LogWarning("LivesTextUpdater was not assigned a Score Updated Channel. "
                + "Please assign a channel to properly display the lives.");
        }
    }

    private void OnDisable()
    {
        if (_healthChangedChannel != null)
        {
            _healthChangedChannel.OnEventRaised -= HandleHealthChange;
        }
    }

    private void Start()
    {
        _livesText = GetComponent<TextMeshProUGUI>();
        _livesText.text = _sessionData.PlayerHealth.ToString();
    }

    private void HandleHealthChange(int newHealthAmount)
    {
        _livesText.text = newHealthAmount.ToString();
    }
}
