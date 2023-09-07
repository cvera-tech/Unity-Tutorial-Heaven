using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Particle Event Channel")]
public class ParticleEventChannelSO : ScriptableObject
{
    public UnityAction<Collider2D> OnEventRaised;
    public void RaiseEvent(Collider2D collider) => OnEventRaised?.Invoke(collider);
}