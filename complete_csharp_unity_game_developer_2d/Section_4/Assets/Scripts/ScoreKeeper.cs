using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int CorrectAnswers { get; private set; }
    public int QuestionsSeen { get; private set; }

    public string ScoreString
    {
        get
        {
            int percent = Mathf.RoundToInt(CorrectAnswers / ((float)QuestionsSeen) * 100);
            if (QuestionsSeen == 0)
            {
                return "--";
            }
            return CorrectAnswers + " / " + QuestionsSeen + " (" + percent + "%)";
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

    public void Reset()
    {
        CorrectAnswers = 0;
        QuestionsSeen = 0;
    }

}
