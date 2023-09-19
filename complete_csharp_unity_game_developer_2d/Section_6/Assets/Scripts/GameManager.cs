using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _scoreResetEventChannel;
    [SerializeField] private VoidEventChannelSO _playerDiedEventChannel;
    [SerializeField] private float _playerDeathScoreScreenDelay = 1f;
    [Tooltip("If true, this component sends a score reset event before the first frame update.")]
    [SerializeField] private bool _resetScore = false;

    private void OnEnable()
    {
        if (_playerDiedEventChannel != null)
        {
            _playerDiedEventChannel.Subscribe(HandlePlayerDeath);
        }
        else
        {
            Debug.LogWarning(name + " has no assigned Player Died Event Channel!");
        }
    }

    private void OnDisable()
    {
        if (_playerDiedEventChannel != null)
        {
            _playerDiedEventChannel.Unsubscribe(HandlePlayerDeath);
        } 
    }

    private void Start()
    {
        if (_resetScore)
        {
            _scoreResetEventChannel.RaiseEvent();
        }
    }

    private void HandlePlayerDeath()
    {
        StartCoroutine(LoadScoreScreenWithDelay());
    }

    public void LoadGame()
    {
        if (_scoreResetEventChannel != null)
        {
            _scoreResetEventChannel.RaiseEvent();
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadScoreScreen()
    {
        SceneManager.LoadScene("Score Screen");
    }

    private IEnumerator LoadScoreScreenWithDelay()
    {
        yield return new WaitForSeconds(_playerDeathScoreScreenDelay);
        LoadScoreScreen();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting application.");
        Application.Quit();
    }
}
