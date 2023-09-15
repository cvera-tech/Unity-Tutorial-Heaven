using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float _magnitude;
    [SerializeField] private float _duration;
    [SerializeField] private VoidEventChannelSO _cameraShakeEventChannel;

    private Vector3 _initialPosition;

    private void OnEnable()
    {
        if (_cameraShakeEventChannel != null)
        {
            _cameraShakeEventChannel.Subscribe(HandleCameraShakeEvent);
        }
        else
        {
            Debug.LogWarning(this.name + " has no assigned Camera Shake Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_cameraShakeEventChannel != null)
        {
            _cameraShakeEventChannel.Unsubscribe(HandleCameraShakeEvent);
        }
    }

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void HandleCameraShakeEvent()
    {
        // Debug.Log("Camera shake event received!");
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = transform.position + (Vector3)Random.insideUnitCircle * _magnitude;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _initialPosition;
    }
}
