using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    // [SerializeField] GameObject startPositionObject;
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] ParticleSystem finishLineParticleSystem;

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
            Invoke(nameof(ReloadScene), reloadSceneDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
