using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    [SerializeField] string correctText = "Correct!";
    [SerializeField] string wrongText = "Wrong!";

    private int correctAnswerIndex;

    void Start()
    {
        DisplayQuestion();
    }

    public void OnAnswerSelect(int index)
    {
        GameObject selectedButton = answerButtons[index];
        Image buttonImage = selectedButton.GetComponent<Image>();
        
        if (index == correctAnswerIndex)
        {
            questionText.text = correctText;
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = wrongText;
            buttonImage.sprite = wrongAnswerSprite;
            GameObject correctButton = answerButtons[correctAnswerIndex];
            Image correctImage = correctButton.GetComponent<Image>();
            correctImage.sprite = correctAnswerSprite;
        }
        SetButtonsInteractable(false);
    }

    private void DisplayQuestion()
    {
        questionText.text = question.Question;
        correctAnswerIndex = question.CorrectAnswerIndex;

        for (int i = 0; i < answerButtons.Length; i++) 
        {
            GameObject answerButton = answerButtons[i];
            TextMeshProUGUI buttonTMP = answerButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonTMP.text = question.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        SetButtonsInteractable(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
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
}