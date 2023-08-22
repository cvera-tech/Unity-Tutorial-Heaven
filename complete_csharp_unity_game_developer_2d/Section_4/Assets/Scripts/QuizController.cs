using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    [SerializeField] string correctText = "Correct!";
    [SerializeField] string wrongText = "Wrong!";

    private int correctAnswerIndex;

    void Start()
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
    }
}