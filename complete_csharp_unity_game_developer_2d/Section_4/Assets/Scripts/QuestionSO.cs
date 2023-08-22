using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField, TextArea(2, 6)]
    string question = "Enter question text here.";
}
