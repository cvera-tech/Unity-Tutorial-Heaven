using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TODO: Reduce the number of events being raised.
    // Reset player health, destroy scene persist, and reset score can all be condensed to one channel.
    [SerializeField] private IntEventChannelSO _playerHealthChangedChannel;
    [SerializeField] private VoidEventChannelSO _resetPlayerHealthChannel;
    [SerializeField] private VoidEventChannelSO _destroyScenePersistChannel;
    [SerializeField] private VoidEventChannelSO _resetScoreChannel;

    [SerializeField] private GameLevels gameLevels;


    [Tooltip("This is how long it takes to reset the game after the player loses all lives.")]
    [SerializeField] private float resetGameDelay = 2f;

    [Tooltip("This is how long it takes to restart the current level after the player loses a life.")]
    [SerializeField] private float restartLevelDelay = 2f;

    private void OnEnable() 
    {
        if (_playerHealthChangedChannel != null)
        {
            _playerHealthChangedChannel.OnEventRaised += HandlePlayerHealthChange;
        }
    }

    private void OnDisable()
    {
        if (_playerHealthChangedChannel != null)
        {
            _playerHealthChangedChannel.OnEventRaised -= HandlePlayerHealthChange;
        }
    }

    private void HandlePlayerHealthChange(int health)
    {
        if (health <= 0)
        {
            // Raise an event to reset the player health.
            if (_resetPlayerHealthChannel != null)
            {
                _resetPlayerHealthChannel.RaiseEvent();
            }
            if (_resetScoreChannel != null)
            {
                _resetScoreChannel.RaiseEvent();
            }
            StartCoroutine(LoadScene(gameLevels.Levels[0], resetGameDelay, true));
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadScene(currentSceneName, restartLevelDelay, false));
        }
    }

    private IEnumerator LoadScene(string sceneName, float delay, bool destroyScenePersist)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (destroyScenePersist)
        {
            _destroyScenePersistChannel.RaiseEvent();
        }
        SceneManager.LoadScene(sceneName);
    }
}
