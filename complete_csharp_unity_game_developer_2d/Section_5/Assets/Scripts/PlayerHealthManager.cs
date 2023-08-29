using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class manages the health of the player character.
/// 
/// Adapted from samyam's tutorial:
/// https://www.youtube.com/watch?v=qUYpQ8ySkLU
/// </summary>
[CreateAssetMenu(menuName = "Health Manager")]
public class PlayerHealthManager : ScriptableObject
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int health;

    public UnityEvent<int> healthChangeEvent;

    private void OnEnable()
    {
        health = maxHealth;
        healthChangeEvent ??= new();
    }

    /// <summary>
    /// Changes the current health by adding the input amount.
    /// </summary>
    /// <param name="amount">How much health to add (a negative value subtracts health).</param>
    public void ChangeHealth(int amount)
    {
        health += amount;
        healthChangeEvent.Invoke(health);
    }

    /// <summary>
    /// Sets health to maxHealth. This method does NOT invoke a health change event.
    /// </summary>
    public void ResetHealth() => health = maxHealth;
}
