
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour, IInteractable
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private float delay = 1f;

    // TODO: Is this the right place to send out this event?
    // I think a more general "Level Manager" might be proper, but that would require sending two events:
    // one to load the new scene and another to destroy the scene persist.
    [SerializeField] private VoidEventChannelSO _destroyScenePersistChannel;
    
    public void Interact()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(delay);
        if (_destroyScenePersistChannel != null)
        {
            _destroyScenePersistChannel.RaiseEvent();
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
