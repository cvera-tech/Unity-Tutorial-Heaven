using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Audio")]
public class AudioEventChannelSO : ScriptableObject
{
    private UnityAction<AudioClip, float> _onEventRaised;

    public void RaiseEvent(AudioClip audioClip, float volume)
    {
        _onEventRaised?.Invoke(audioClip, volume);
    }

    public void Subscribe(UnityAction<AudioClip, float> eventHandler)
    {
        _onEventRaised += eventHandler;
    }

    public void Unsubscribe(UnityAction<AudioClip, float> eventHandler)
    {
        _onEventRaised -= eventHandler;
    }
}
