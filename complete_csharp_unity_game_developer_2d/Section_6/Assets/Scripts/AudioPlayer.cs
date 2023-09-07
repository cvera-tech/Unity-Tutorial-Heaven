using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    private void OnEnable()
    {
        if (_audioEventChannel != null)
        {
            _audioEventChannel.Subscribe(HandleAudioEvent);
        }
    }

    private void OnDisable()
    {
        if (_audioEventChannel != null)
        {
            _audioEventChannel.Unsubscribe(HandleAudioEvent);
        }
    }

    private void HandleAudioEvent(AudioClip audioClip, float volume)
    {
        AudioSource.PlayClipAtPoint(
            audioClip,
            Camera.main.transform.position,
            volume
        );
    }
}
