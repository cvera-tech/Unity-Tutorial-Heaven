using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float durationToAnswerQuestion = 15f;
    [SerializeField] float durationToShowCorrectAnswer = 10f;

    private bool _isAnsweringQuestion = false;
    private float timeRemaining;

    public bool IsAnsweringQuestion
    {
        get => _isAnsweringQuestion;
        set { _isAnsweringQuestion = value; }
    }

    void Update()
    {
        UpdateTimer();
        if (timeRemaining <= 0)
        {
            if (_isAnsweringQuestion)
            {
                timeRemaining = durationToShowCorrectAnswer;
                _isAnsweringQuestion = false;
            }
            else
            {
                timeRemaining = durationToAnswerQuestion;
                _isAnsweringQuestion = true;
            }
        }

        Debug.Log(timeRemaining);
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        
    }
}
