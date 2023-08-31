using UnityEngine;

public class ScenePersist : MonoBehaviour
{

    [SerializeField] private VoidEventChannelSO _destroyScenePersistChannel;

    private void OnEnable()
    {
        if (_destroyScenePersistChannel != null)
        {
            _destroyScenePersistChannel.OnEventRaised += DestroyScenePersist;
        }
        else
        {
            Debug.LogWarning("ScenePersist was not assigned a Load Next Level Channel. "
                + "Please assign a channel to properly update the destroy the game object.");
        }
    }

    private void OnDisable()
    {
        if (_destroyScenePersistChannel != null)
        {
            _destroyScenePersistChannel.OnEventRaised -= DestroyScenePersist;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<ScenePersist>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void DestroyScenePersist()
    {
        Destroy(gameObject);
    }
}
