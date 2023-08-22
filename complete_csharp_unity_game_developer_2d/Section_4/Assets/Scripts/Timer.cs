using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float durationToAnswerQuestion = 15f;
    [SerializeField] float durationToShowCorrectAnswer = 10f;

    // TODO: Move state management to quiz controller or a separate script.
    private bool _isAnsweringQuestion = false;
    private bool _showNextQuestion = false;
    // private bool _timedout = false;
    // private bool timerCancelled = false;
    private float timeRemaining;

    public bool IsAnsweringQuestion
    {
        get => _isAnsweringQuestion;
        set { _isAnsweringQuestion = value; }
    }

    public float FillFraction { get => CalculateFillPercentage(); }

    public bool ShowNextQuestion
    {
        get => _showNextQuestion;
        set => _showNextQuestion = value;
    }

    // public bool TimedOut {
    //     get => _timedout;
    //     set => _timedout = value;
    // }

    void Update()
    {
        UpdateTimer();
        if (timeRemaining > 0)
        {
            // Debug.Log(FillFraction);
        }
        else
        {
            if (IsAnsweringQuestion)
            {
                timeRemaining = durationToShowCorrectAnswer;
                IsAnsweringQuestion = false;
                // if (!timerCancelled)
                // {
                //     // TimedOut = true;
                //     timerCancelled = false;
                // }
            }
            else
            {
                timeRemaining = durationToAnswerQuestion;
                IsAnsweringQuestion = true;
                ShowNextQuestion = true;
            }
        }

        // Debug.Log(timeRemaining);
    }

    public void CancelTimer()
    {
        timeRemaining = 0;
        // timerCancelled = true;
    }

    private float CalculateFillPercentage()
    {
        if (IsAnsweringQuestion)
            return timeRemaining / durationToAnswerQuestion;
        return timeRemaining / durationToShowCorrectAnswer;
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
    }
}
