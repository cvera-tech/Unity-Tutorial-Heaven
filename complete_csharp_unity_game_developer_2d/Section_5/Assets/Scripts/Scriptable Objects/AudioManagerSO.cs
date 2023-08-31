using UnityEngine;

public class AudioManagerSO : MonoBehaviour
{
    [Tooltip("The Audio Manager plays audio clips within events raised by in this channel.")]
    [SerializeField] private AudioEventChannelSO _audioClipChannel;

    private void OnEnable()
    {
        if (_audioClipChannel != null)
            _audioClipChannel.OnEventRaised += PlayClip;
        else
            Debug.LogWarning("Audio Manager was not assigned an Audio Clip Channel. "
                + "Please assign a channel to enable audio playback.");
    }

    private void OnDisable()
    {
        if (_audioClipChannel != null)
            _audioClipChannel.OnEventRaised -= PlayClip;
    }

    private void PlayClip(AudioClip audioClip)
    {
    }
}
