using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    // [SerializeField] GameObject startPositionObject;
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] ParticleSystem finishLineParticleSystem;
    [SerializeField] AudioClip finishLineAudioClip;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // GameObject player = other.gameObject;
            // startPositionObject.transform.GetPositionAndRotation(out Vector3 startPosition, out Quaternion startRotation);
            // player.transform.SetPositionAndRotation(startPosition, startRotation);
            // player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // player.GetComponent<Rigidbody2D>().angularVelocity = 0;
            finishLineParticleSystem.Play();
            audioSource.PlayOneShot(finishLineAudioClip);

            Invoke(nameof(ReloadScene), reloadSceneDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
