using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Tooltip("This is how much damage this object deals on a collision.")]
    [SerializeField] private int damage = 1;

    // TODO: See if there is a less fiddly way to get tags than using strings.
    [Tooltip("These are the tags that this GameObject can deal damage to.")]
    [SerializeField] private string[] tagsToDamage;

    [Tooltip("This is the channel to raise collision events if needed.")]
    [SerializeField] private Collision2DEventChannelSO _collisionChannel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (!Array.Exists(tagsToDamage, value => otherGameObject.CompareTag(value)))
            return;

        if (_collisionChannel != null)
            _collisionChannel.RaiseEvent(collision, gameObject);

        if (otherGameObject.TryGetComponent(out HealthManager healthManager))
            healthManager.ChangeHealth(-damage);
    }
}
