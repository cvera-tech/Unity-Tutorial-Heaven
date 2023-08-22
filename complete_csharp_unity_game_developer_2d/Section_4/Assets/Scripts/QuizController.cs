using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;

    public void Start()
    {
        questionText.text = question.Question;

        for (int i = 0; i < answerButtons.Length; i++) 
        {
            GameObject answerButton = answerButtons[i];
            TextMeshProUGUI buttonTMP = answerButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonTMP.text = question.GetAnswer(i);
        }
    }
}