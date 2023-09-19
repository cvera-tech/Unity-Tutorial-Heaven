using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    [SerializeField] private ParticleEventChannelSO _particleEventChannel;
    [SerializeField] private ParticleSystem _hitEffect;


    private void OnEnable()
    {
        if (_particleEventChannel != null)
        {
            _particleEventChannel.Subscribe(HandleParticleEvent);
        }
        else
        {
            Debug.LogWarning(name + " has no assigned Particle Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_particleEventChannel != null)
        {
            _particleEventChannel.Unsubscribe(HandleParticleEvent);
        }
    }

    private void HandleParticleEvent(Collider2D collider)
    {
        // Debug.Log("Hey, I'm particling over here!");
        ParticleSystem instance = Instantiate(
            _hitEffect, 
            collider.transform.position, 
            Quaternion.identity
        );
        
        instance.GetComponent<Renderer>().sortingLayerName = 
            collider.GetComponentInChildren<SpriteRenderer>().sortingLayerName;

        Destroy(
            instance.gameObject,
            instance.main.duration + instance.main.startLifetime.constantMax
        );
    }
}
