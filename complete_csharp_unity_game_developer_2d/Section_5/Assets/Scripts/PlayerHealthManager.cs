using UnityEngine;

/// <summary>
/// This class manages the health of the player character.
/// 
/// Adapted from samyam's tutorial:
/// https://www.youtube.com/watch?v=qUYpQ8ySkLU
/// 
/// As well as from the Unity Open Project:
/// https://github.com/UnityTechnologies/open-project-1/
/// </summary>
public class PlayerHealthManager : HealthManager
{
    [Tooltip("The channel where this component will raise health change events.")]
    [SerializeField] private IntEventChannelSO _healthChangedChannel;
    [SerializeField] private VoidEventChannelSO _resetHealthChannel;
    [SerializeField] private SessionDataSO _sessionData;

    private bool UseSessionData => _sessionData != null;

    protected override void OnEnable()
    {
        if (!UseSessionData)
        {
            base.OnEnable();
        }

        if (_resetHealthChannel != null)
        {
            _resetHealthChannel.OnEventRaised += ResetHealth;
        }
    }

    private void OnDisable()
    {
        if (_resetHealthChannel != null)
        {
            _resetHealthChannel.OnEventRaised -= ResetHealth;
        }
    }

    /// <summary>
    /// Changes the current health by adding the input amount.
    /// </summary>
    /// <param name="amount">How much health to add (a negative value subtracts health).</param>
    public override void ChangeHealth(int amount)
    {
        if (UseSessionData)
        {
            _sessionData.PlayerHealth += amount;
            if (_healthChangedChannel != null)
            {
                _healthChangedChannel.RaiseEvent(_sessionData.PlayerHealth);
            }
            if (_sessionData.PlayerHealth <= 0)
            {
                OnZeroHealth();
            }
        }
        else
        {
            base.ChangeHealth(amount);
            _healthChangedChannel.RaiseEvent(currentHealth);
        }

    }

    public override void OnZeroHealth()
    {
        if (UseSessionData)
            _sessionData.PlayerHealth = maxHealth;
        else
            base.OnZeroHealth();
    }
}
