using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float durationToAnswerQuestion = 15f;
    [SerializeField] float durationToShowCorrectAnswer = 10f;

    private float timeRemaining;

    public float FillFraction { get => CalculateFillPercentage(); }

    private QuizController quizController;

    private bool running = false;


    private void Update()
    {
        // Debug.Log("[Timer.Update] Running = " + running);
        if (running)
        {
            UpdateTimer();
            if (timeRemaining <= 0)
            {
                // Stop the timer to prevent multiple calls to HandleTimeout.
                StopTimer();
                quizController.HandleTimeout();
            }
        }
    }

    public void Initialize(QuizController quizController)
    {
        this.quizController = quizController;
    }
    
    public void ResetTimer()
    {
        if (quizController.CurrentGameState == GameState.AskQuestion)
            timeRemaining = durationToAnswerQuestion;
        else
            timeRemaining = durationToShowCorrectAnswer;
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    private float CalculateFillPercentage()
    {
        GameState currentGameState = quizController.CurrentGameState;
        if (currentGameState == GameState.AskQuestion)
            return timeRemaining / durationToAnswerQuestion;
        return timeRemaining / durationToShowCorrectAnswer;
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
    }
}
