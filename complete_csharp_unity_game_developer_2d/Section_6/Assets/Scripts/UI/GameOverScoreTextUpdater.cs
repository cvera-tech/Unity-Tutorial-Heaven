using TMPro;
using UnityEngine;

public class GameOverScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private ScoreManagerSO _scoreManagerScriptableObject;
    void Start()
    {

        GetComponent<TextMeshProUGUI>().text = "Score: " + _scoreManagerScriptableObject.Score;
    }
}
