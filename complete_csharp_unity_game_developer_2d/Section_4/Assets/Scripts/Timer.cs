using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float durationToAnswerQuestion = 15f;
    [SerializeField] float durationToShowCorrectAnswer = 10f;

    private float timeRemaining;

    public float FillFraction { get => CalculateFillPercentage(); }

    private QuizController quizController;

    void Start()
    {
        // There should only be one quiz controller
        quizController = FindObjectOfType<QuizController>();
    }

    void Update()
    {
        UpdateTimer();
        if (timeRemaining <= 0)
            quizController.HandleTimeout();
    }
    
    public void ResetTimer()
    {
        Debug.Log("[Timer.ResetTimer] CurrentGameState = " + quizController.CurrentGameState);
        if (quizController.CurrentGameState == GameState.AskQuestion)
            timeRemaining = durationToAnswerQuestion;
        else
            timeRemaining = durationToShowCorrectAnswer;
        Debug.Log("[Timer.ResetTimer] timeRemaining = " + timeRemaining);
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
