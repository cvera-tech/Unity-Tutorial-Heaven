using UnityEngine;
using UnityEngine.Events;

public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    public void RaiseEvent(AudioClip audioClip) => OnEventRaised?.Invoke(audioClip);
}
