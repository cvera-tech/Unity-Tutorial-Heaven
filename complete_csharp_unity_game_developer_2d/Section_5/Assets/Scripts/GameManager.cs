using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthManager playerHealthManager;
    [SerializeField] private GameLevels gameLevels;


    [Tooltip("This is how long it takes to reset the game after the player loses all lives.")]
    [SerializeField] private float resetGameDelay = 2f;

    [Tooltip("This is how long it takes to restart the current level after the player loses a life.")]
    [SerializeField] private float restartLevelDelay = 2f;

    private void OnEnable() => playerHealthManager.healthChangeEvent.AddListener(HandlePlayerHealthChange);
    private void OnDisable() => playerHealthManager.healthChangeEvent.RemoveListener(HandlePlayerHealthChange);

    private void HandlePlayerHealthChange(int health)
    {
        Debug.Log("Player died!");
        if (health <= 0)
        {
            playerHealthManager.ResetHealth();
            StartCoroutine(LoadScene(gameLevels.Levels[0], resetGameDelay));
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            // int nextSceneNameIndex = Array.FindIndex(gameLevels.Levels, value => value == currentSceneName);
            // string nextSceneName = gameLevels.Levels[nextSceneNameIndex];
            StartCoroutine(LoadScene(currentSceneName, restartLevelDelay));
        }
    }

    private IEnumerator LoadScene(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }
}
