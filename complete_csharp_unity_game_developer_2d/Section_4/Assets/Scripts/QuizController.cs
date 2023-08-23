using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public enum GameState
{
    Start,
    AskQuestion,
    AnswerSelected,
    TimedOut,
    ShowAnswer,
    End
}

public class QuizController : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    // NOTE: The shorthand `new()` from C# 9+ does not play nicely with 
    // serialized fields. In this case, the shorthand causes the created List
    // object to implode (AKA become Disposed by the garbage collector) when you
    // exit play mode.
    // [SerializeField] List<QuestionSO> questions = new();
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    private QuestionSO currentQuestion;
    private int currentQuestionIndex;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    private int correctAnswerIndex;
    private int selectedAnswerIndex;

    [Header("Buttons")]
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite correctButtonSprite;
    [SerializeField] Sprite wrongButtonSprite;

    [Header("Results")]
    [SerializeField] string correctText = "Correct!";
    [SerializeField] string wrongText = "Wrong!";
    [SerializeField] string timeoutText = "You ran out of time!";

    [Header("Timer")]
    [SerializeField] Image timerImage;

    private Timer timer;
    private ScoreKeeper scoreKeeper;

    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get => _currentGameState;
        // Avoid assigning values to this property directly.
        // Use the HandleGameStateChange method.
        private set => _currentGameState = value;
    }

    void OnEnable()
    {
    }

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        
        // Start off with the first question.
        CurrentGameState = GameState.Start;
        Debug.Log("[QuizController.Start] CurrentGameState == " + CurrentGameState);
    }

    void Update()
    {
        timerImage.fillAmount = timer.FillFraction;

        // Debug.Log("[QuizController.Update] CurrentGameState == " + CurrentGameState);
        switch (CurrentGameState)
        {
            case GameState.Start:
                // At the start, we switch over immediately to AskQuestion
                // to trigger the retrieval and display of the first question.
                HandleGameStateChange(GameState.AskQuestion);
                break;
            case GameState.End:
                // TODO: Create a score screen!
                break;
            default:
                // Don't do anything in other states!
                // Let the state machine handle updating UI.
                break;
        }
    }

    public void HandleTimeout()
    {
        Debug.Log("[QuizController.HandleTimeout]");
        GameState newGameState;
        switch (CurrentGameState)
        {
            case GameState.AskQuestion:
                newGameState = GameState.TimedOut;
                break;
            case GameState.AnswerSelected:
            case GameState.TimedOut:
                newGameState = GameState.ShowAnswer;
                break;
            case GameState.ShowAnswer:
                newGameState = GameState.AskQuestion;
                break;
            default:
                // TODO: We could throw an error here
                newGameState = CurrentGameState;
                break;
        }
        HandleGameStateChange(newGameState, true);
    }

    /*
     * Event handler for buttons.
     */
    public void OnAnswerSelect(int index)
    {
        Debug.Log("[QuizController.OnAnswerSelect] index = " + index);
        selectedAnswerIndex = index;
        HandleGameStateChange(GameState.AnswerSelected, false);
    }

    private void DisplayNextQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            currentQuestion = questions[currentQuestionIndex];
            SetButtonsInteractable(true);
            SetDefaultButtonSprites();
            questionText.text = currentQuestion.Question;
            correctAnswerIndex = currentQuestion.CorrectAnswerIndex;

            for (int i = 0; i < answerButtons.Length; i++) 
            {
                GameObject answerButton = answerButtons[i];
                TextMeshProUGUI buttonTMP = answerButton.GetComponentInChildren<TextMeshProUGUI>();
                buttonTMP.text = currentQuestion.GetAnswer(i);
            }

            currentQuestionIndex++;
        }
        else
        {
            HandleGameStateChange(GameState.End);
        }
    }

    /**
     * This function performs all the associated actions for state transitions.
     */
    private void HandleGameStateChange(GameState newGameState, bool timeout = false)
    {
        bool resetTimer = false;
        Debug.Log("[QuizController.HandleGameStateChange] newGameState = " + newGameState + " | timeout = " + timeout);
        if (newGameState == GameState.AnswerSelected)
        {
            if (selectedAnswerIndex == correctAnswerIndex)
            {
                Debug.Log("Correct Answer");
                questionText.text = correctText;
                scoreKeeper.CorrectAnswer();
            }
            else
            {
                Debug.Log("Incorrect Answer");
                questionText.text = wrongText;
                GameObject selectedButton = answerButtons[selectedAnswerIndex];
                selectedButton.GetComponent<Image>().sprite = wrongButtonSprite;
                scoreKeeper.IncorrectAnswer();
            }
            GameObject correctButton = answerButtons[correctAnswerIndex];
            correctButton.GetComponent<Image>().sprite = correctButtonSprite;
            SetButtonsInteractable(false);
            resetTimer = true;
        }
        // Question timed out
        else if (newGameState == GameState.TimedOut)
        {
            questionText.text = timeoutText;
            GameObject correctButton = answerButtons[correctAnswerIndex];
            Image correctImage = correctButton.GetComponent<Image>();
            correctImage.sprite = correctButtonSprite;
            scoreKeeper.IncorrectAnswer();

            SetButtonsInteractable(false);
            resetTimer = true;
        }
        // Move on to the next question if possible
        else if (newGameState == GameState.AskQuestion)
        {
            DisplayNextQuestion();
            resetTimer = true;
        }
        else if (newGameState == GameState.End)
        {
            // TODO: Show new screen with score.
        }

        CurrentGameState = newGameState;
        if (resetTimer)
            timer.ResetTimer();
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        Debug.Log("[QuizController.SetButtonsInteractable] isInteractable = " + isInteractable);
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().interactable = isInteractable;
        }
 
    }

    private void SetDefaultButtonSprites()
    {
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().image.sprite = defaultButtonSprite;
        }
    }
}