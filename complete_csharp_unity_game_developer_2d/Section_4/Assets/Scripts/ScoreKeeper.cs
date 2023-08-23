using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    public int CorrectAnswers { get; private set; }
    public int QuestionsSeen { get; private set; }

    void Start()
    {
        CorrectAnswers = 0;
        QuestionsSeen = 0;
    }

    void Update()
    {
        if (QuestionsSeen > 0)
        {
            scoreText.text = "Score: " + CorrectAnswers + " / " + QuestionsSeen
                + " (" + CalculatePercent() + "%)";
        }
    }

    public void CorrectAnswer()
    {
        CorrectAnswers++;
        QuestionsSeen++;
    }

    public void IncorrectAnswer()
    {
        QuestionsSeen++;
    }

    private int CalculatePercent()
    {
        return (int)(CorrectAnswers / ((float)QuestionsSeen) * 100);
    }

}
