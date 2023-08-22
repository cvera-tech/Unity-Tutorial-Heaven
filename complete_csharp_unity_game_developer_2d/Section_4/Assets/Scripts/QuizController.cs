using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

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

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    private int correctAnswerIndex;
    private bool hasAnsweredEarly;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Results")]
    [SerializeField] string correctText = "Correct!";
    [SerializeField] string wrongText = "Wrong!";
    [SerializeField] string timeoutText = "You ran out of time!";

    [Header("Timer")]
    [SerializeField] Image timerImage;
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.FillFraction;
        if (timer.ShowNextQuestion)
        {
            GetNextQuestion();
            timer.IsAnsweringQuestion = true;
            timer.ShowNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.IsAnsweringQuestion)
        {
            ShowCorrectAnswer(-1);
            SetButtonsInteractable(false);
        }
    }

    public void OnAnswerSelect(int index)
    {
        Debug.Log(index);
        hasAnsweredEarly = true;
        ShowCorrectAnswer(index);
        SetButtonsInteractable(false);
        timer.CancelTimer();
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.Question;
        correctAnswerIndex = currentQuestion.CorrectAnswerIndex;

        for (int i = 0; i < answerButtons.Length; i++) 
        {
            GameObject answerButton = answerButtons[i];
            TextMeshProUGUI buttonTMP = answerButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonTMP.text = currentQuestion.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            currentQuestion = questions[0];
            questions.RemoveAt(0);
            hasAnsweredEarly = false;
            SetButtonsInteractable(true);
            SetDefaultButtonSprites();
            DisplayQuestion();
        }
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().interactable = isInteractable;
        }
 
    }

    private void SetDefaultButtonSprites()
    {
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().image.sprite = defaultAnswerSprite;
        }
    }

    private void ShowCorrectAnswer(int selectedIndex)
    {
        if (selectedIndex == correctAnswerIndex)
        {
            questionText.text = correctText;
            GameObject selectedButton = answerButtons[selectedIndex];
            selectedButton.GetComponent<Image>().sprite = correctAnswerSprite;
        }
        else
        {
            if (hasAnsweredEarly)
            {
                questionText.text = wrongText;
                GameObject selectedButton = answerButtons[selectedIndex];
                selectedButton.GetComponent<Image>().sprite = wrongAnswerSprite;
            }
            else
            {
                questionText.text = timeoutText;
            }
            GameObject correctButton = answerButtons[correctAnswerIndex];
            Image correctImage = correctButton.GetComponent<Image>();
            correctImage.sprite = correctAnswerSprite;
        }
    }
}