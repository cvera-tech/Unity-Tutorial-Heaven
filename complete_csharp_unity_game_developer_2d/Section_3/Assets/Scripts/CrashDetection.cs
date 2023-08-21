using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetection : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] ParticleSystem crashParticleSystem;
    [SerializeField] AudioClip crashAudioClip;

    private AudioSource audioSource;
    private PlayerController playerController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            crashParticleSystem.Play();
            audioSource.PlayOneShot(crashAudioClip);
            playerController.DisableControls();
            Invoke(nameof(ReloadScene), reloadSceneDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
