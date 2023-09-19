using UnityEngine;
using UnityEngine.UI;

public class HealthUIUpdater : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _healthUpdatedEventChannel;
    [SerializeField] private HealthSO _healthScriptableObject;

    private Slider _slider;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();

        if (_healthUpdatedEventChannel != null)
        {
            _healthUpdatedEventChannel.Subscribe(HandleHealthUpdatedEvent);
        }
        else
        {
            Debug.LogWarning(this.name + " has no assigned Health Updated Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_healthUpdatedEventChannel != null)
        {
            _healthUpdatedEventChannel.Unsubscribe(HandleHealthUpdatedEvent);
        }
    }

    private void Start()
    {
        // Get initial state
        HandleHealthUpdatedEvent();
    }

    private void HandleHealthUpdatedEvent()
    {
        // Debug.Log("Health Update Event received!");
        _slider.value = (float)_healthScriptableObject.Value / _healthScriptableObject.MaxValue * 100;
        // Debug.Log(_slider.value);
    }
}
