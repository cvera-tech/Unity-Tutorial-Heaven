using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _attackSpeed = 2f;
    [SerializeField] private int _projectileDamage = 10;
    [SerializeField] private float _projectileLifetime = 3f;
    [SerializeField] private float _projectileSpeed = 5f;

    [Header("AI")]
    [SerializeField] private bool _useAI = false;
    [SerializeField] private float _attackSpeedVariance = 0f;

    [Header("Audio")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField, Range(0f, 1f)] private float _audioVolume;

    private bool _isFiring;

    public bool IsFiring { get => _isFiring; set => _isFiring = value; }
    // public float Cooldown { get => _attackSpeed; set => _attackSpeed = value; }
    // public float CooldownVariance { get => _attackSpeedVariance; set => _attackSpeedVariance = value; }
    // public int ProjectileDamage { get => _projectileDamage; set => _projectileDamage = value; }
    // public float ProjectileLifetime { get => _projectileLifetime; set => _projectileLifetime = value; }
    // public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }

    private float AttackCooldown
    {
        get
        {
            float randomAttackSpeed = Mathf.Max(
                Random.Range(_attackSpeed - _attackSpeedVariance, _attackSpeed + _attackSpeedVariance),
                Mathf.Epsilon);
            return 1f / randomAttackSpeed;
        }
    }

    private Coroutine _firingCoroutine;

    public void Start()
    {
        if (_useAI)
        {
            _isFiring = true;
        }
    }

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
            projectileInstance.GetComponent<DamageDealer>().Damage = _projectileDamage;
            if (projectileInstance.TryGetComponent(out Rigidbody2D projectileRigidbody))
            {
                projectileRigidbody.velocity = transform.up * _projectileSpeed;
            }
            SendAudioEvent();
            Destroy(projectileInstance, _projectileLifetime);
            yield return new WaitForSeconds(AttackCooldown);
        }
        while (_isFiring);
    }

    private void SendAudioEvent()
    {
        if (_audioEventChannel != null && _audioClip != null)
        {
            _audioEventChannel.RaiseEvent(_audioClip, _audioVolume);
        }
    }
}
