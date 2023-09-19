using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Particle")]
public class ParticleEventChannelSO : ScriptableObject
{
    private UnityAction<Collider2D> OnEventRaised;

    public void RaiseEvent(Collider2D collider)
    {
        OnEventRaised?.Invoke(collider);
    }

    public void Subscribe(UnityAction<Collider2D> eventHandler)
    {
        OnEventRaised += eventHandler;
    }

    public void Unsubscribe(UnityAction<Collider2D> eventHandler)
    {
        OnEventRaised -= eventHandler;
    }
}