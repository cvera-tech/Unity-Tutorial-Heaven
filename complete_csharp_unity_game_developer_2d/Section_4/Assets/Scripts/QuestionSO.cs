using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField, TextArea(2, 6)]
    string _question = "Enter question text here.";

    [SerializeField]
    string[] _answers = new string[4];

    [SerializeField]
    int _correctAnswerIndex;

    public int CorrectAnswerIndex { get => _correctAnswerIndex; }

    public string Question { get => _question; }
    
    public string GetAnswer (int index)
    {
        return _answers[index];
    }
    
}