using UnityEngine;

[CreateAssetMenu(menuName = "Score")]
public class ScoreSO : ScriptableObject
{
    private int _score;

    public int Score { get => _score; set => _score = value; }
}
