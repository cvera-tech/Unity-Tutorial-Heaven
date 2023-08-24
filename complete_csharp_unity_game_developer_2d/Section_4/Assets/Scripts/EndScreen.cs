using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    
    public void Initialize(ScoreKeeper scoreKeeper)
    {
        this.scoreKeeper = scoreKeeper;
    }

    public void ShowFinalScore()
    {
        scoreText.text = "You got " + scoreKeeper.ScoreString;
    }
}
