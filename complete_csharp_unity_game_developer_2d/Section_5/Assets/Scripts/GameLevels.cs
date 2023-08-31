using UnityEngine;

[CreateAssetMenu]
public class GameLevels : ScriptableObject
{
    [Tooltip("This includes the scene names of all levels in the game in order of play.")]
    [SerializeField] private string[] levels;

    public string[] Levels => levels;
}
