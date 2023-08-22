using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float durationToAnswerQuestion = 15f;
    [SerializeField] float durationToShowCorrectAnswer = 10f;

    private bool _isAnsweringQuestion = false;
    private bool _showNextQuestion = false;
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

    void Update()
    {
        UpdateTimer();
        if (timeRemaining > 0)
        {
            Debug.Log(FillFraction);
        }
        else
        {
            if (IsAnsweringQuestion)
            {
                timeRemaining = durationToShowCorrectAnswer;
                IsAnsweringQuestion = false;
            }
            else
            {
                timeRemaining = durationToAnswerQuestion;
                IsAnsweringQuestion = true;
                ShowNextQuestion = true;
            }
        }

        Debug.Log(timeRemaining);
    }

    public void CancelTimer()
    {
        timeRemaining = 0;
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
