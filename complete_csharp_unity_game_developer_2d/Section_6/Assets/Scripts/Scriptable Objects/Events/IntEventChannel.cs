using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Int")]
public class IntEventChannelSO : ScriptableObject
{
    private UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }

    public void Subscribe(UnityAction<int> eventHandler)
    {
        OnEventRaised += eventHandler;
    }

    public void Unsubscribe(UnityAction<int> eventHandler)
    {
        OnEventRaised -= eventHandler;
    }
}

