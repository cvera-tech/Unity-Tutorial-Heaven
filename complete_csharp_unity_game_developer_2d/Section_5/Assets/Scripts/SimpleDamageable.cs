using UnityEngine;

/// <summary>
/// This is a basic damageable component that simply destroys the game object with an optional delay.
/// </summary>
public class SimpleDamageable : AbstractDamageable
{
    [SerializeField] private float destroyDelay = 0f;

    protected override void OnZeroHP() => Destroy(gameObject, destroyDelay);
}
