using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private bool _isFiring;
    [SerializeField] private float _fireDelay = 0.5f;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _projectileLifetime = 3f;
    [SerializeField] private GameObject _projectilePrefab;

    public bool IsFiring { get => _isFiring; set => _isFiring = value; }

    private Coroutine _firingCoroutine;

    // Update is called once per frame
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (_isFiring && _firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!_isFiring && _firingCoroutine != null)
        {
            StopCoroutine(_firingCoroutine);
            _firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        do
        {
            transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
            GameObject projectileInstance = Instantiate(_projectilePrefab, position, rotation);
            if (projectileInstance.TryGetComponent(out Rigidbody2D projectileRigidbody))
            {
                projectileRigidbody.velocity = transform.up * _projectileSpeed;
            }
            Destroy(projectileInstance, _projectileLifetime);
            yield return new WaitForSeconds(_fireDelay);
        }
        while (_isFiring);
    }
}
