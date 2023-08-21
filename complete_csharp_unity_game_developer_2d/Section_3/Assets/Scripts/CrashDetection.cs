using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetection : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] ParticleSystem crashParticleSystem;
    [SerializeField] AudioClip crashAudioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            crashParticleSystem.Play();
            audioSource.PlayOneShot(crashAudioClip);
            Invoke(nameof(ReloadScene), reloadSceneDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
