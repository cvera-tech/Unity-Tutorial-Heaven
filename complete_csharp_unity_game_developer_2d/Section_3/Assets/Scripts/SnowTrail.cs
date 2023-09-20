using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrailParticleSystem;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            snowTrailParticleSystem.Play();
        }
    }

    void OnCollisionExit2D()
    {
        snowTrailParticleSystem.Stop();
    }
}
