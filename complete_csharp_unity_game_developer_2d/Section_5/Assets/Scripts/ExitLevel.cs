
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour, Interactable
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private float delay = 1f;
    public void Interact()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
