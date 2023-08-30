using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage = 1;


    [Tooltip("These are the tags that this GameObject can deal damage to.")]
    [SerializeField] private string[] tagsToDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (!Array.Exists(tagsToDamage, value => otherGameObject.CompareTag(value)))
            return;

        if (otherGameObject.TryGetComponent(out HealthManager healthManager))
            healthManager.ChangeHealth(-damage);
    }
}
