using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Void")]
public class VoidEventChannelSO : ScriptableObject
{
    private UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

    public void Subscribe(UnityAction eventHandler)
    {
        OnEventRaised += eventHandler;
    }

    public void Unsubscribe(UnityAction eventHandler)
    {
        OnEventRaised -= eventHandler;
    }
}
