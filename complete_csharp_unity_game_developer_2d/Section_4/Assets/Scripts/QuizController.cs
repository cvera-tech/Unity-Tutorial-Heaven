using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    public void Start()
    {
        questionText.text = question.Question;
    }
}