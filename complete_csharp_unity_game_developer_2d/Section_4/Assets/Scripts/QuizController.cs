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
    End
}

public class QuizController : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] Canvas quizCanvas;
    [SerializeField] Canvas endCanvas;
    private EndScreen endScreen;


    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new();
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

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get => _currentGameState;
        // Avoid assigning values to this property directly.
        // Use the HandleGameStateChange method.
        private set => _currentGameState = value;
    }

    void Start()
    {
        InitializeSystems();
        
        CurrentGameState = GameState.Start;
    }

    void Update()
    {
        timerImage.fillAmount = timer.FillFraction;
        scoreText.text = "Score: " + scoreKeeper.ScoreString;

        // Debug.Log("[QuizController.Update] CurrentGameState == " + CurrentGameState);
        switch (CurrentGameState)
        {
            case GameState.Start:
                Debug.Log("[QuizController.Update] CurrentGameState == " + CurrentGameState);
                // At the start, we switch over immediately to AskQuestion
                // to trigger the retrieval and display of the first question.
                // If we were to add a start screen, modify this block!
                StartGame();
                HandleGameStateChange(GameState.AskQuestion);
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

        GameState? newGameState = null;
        switch (CurrentGameState)
        {
            case GameState.AskQuestion:
                newGameState = GameState.TimedOut;
                break;
            case GameState.AnswerSelected:
            case GameState.TimedOut:
                newGameState = GameState.AskQuestion;
                break;
            default:
                // TODO: We could throw an error here
                Debug.LogError("[QuizController.HandleTimeout] Invalid Game State");
                break;
        }
        HandleGameStateChange(newGameState ?? CurrentGameState, true);
    }

    /*
     * Event handler for buttons.
     */
    public void OnAnswerSelect(int index)
    {
        // Debug.Log("[QuizController.OnAnswerSelect] index = " + index);
        selectedAnswerIndex = index;
        HandleGameStateChange(GameState.AnswerSelected, false);
    }

    public void OnRestartGameSelect()
    {
        Debug.Log("[QuizController.OnRestartGameSelect]");
        CurrentGameState = GameState.Start;
    }

    /**
     * Retrieves and renders the next question. Returns true if there are
     * questions remaining. False otherwise.
     */
    private bool DisplayNextQuestion()
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
            return true;
        }
        else
        {
            HandleGameStateChange(GameState.End);
            return false;
        }
    }

    private void InitializeSystems()
    {
        
        timer = FindObjectOfType<Timer>();
        timer.Initialize(this);
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreen = FindObjectOfType<EndScreen>();
        endScreen.Initialize(scoreKeeper);
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
                questionText.text = correctText;
                scoreKeeper.CorrectAnswer();
            }
            else
            {
                questionText.text = wrongText;
                GameObject selectedButton = answerButtons[selectedAnswerIndex];
                selectedButton.GetComponent<Image>().sprite = wrongButtonSprite;
                scoreKeeper.IncorrectAnswer();
            }
            GameObject correctButton = answerButtons[correctAnswerIndex];
            correctButton.GetComponent<Image>().sprite = correctButtonSprite;
            SetButtonsInteractable(false);
            progressBar.value++;
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
            progressBar.value++;
            resetTimer = true;
        }
        // Move on to the next question if possible
        else if (newGameState == GameState.AskQuestion)
        {
            resetTimer = DisplayNextQuestion();
        }
        else if (newGameState == GameState.End)
        {
            timer.StopTimer();
            ShowEndScreen();
        }

        CurrentGameState = newGameState;
        if (resetTimer)
        {
            Debug.Log("[QuizController.HandleGameStateChange] Reset timer");
            timer.ResetTimer();
            timer.StartTimer();
        }
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        // Debug.Log("[QuizController.SetButtonsInteractable] isInteractable = " + isInteractable);
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

    private void ShowEndScreen()
    {
        endCanvas.gameObject.SetActive(true);
        quizCanvas.gameObject.SetActive(false);
        endScreen.ShowFinalScore();
    }

    /**
     *  Initialize all the necessary variables and activate the correct canvas.
     */
    private void StartGame()
    {
        currentQuestionIndex = 0;
        progressBar.value = 0;
        scoreKeeper.Reset();
        quizCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }
}