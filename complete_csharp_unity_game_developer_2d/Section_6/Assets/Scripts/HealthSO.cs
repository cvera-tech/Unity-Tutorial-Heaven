using UnityEngine;

[CreateAssetMenu(menuName = "Health")]
public class HealthSO : ScriptableObject
{
    [SerializeField] private int _value = 0;
    [SerializeField] private int _maxValue = 100;

    public int Value { get => _value; set => _value = value; }
    public int MaxValue { get => _maxValue; set => _maxValue = value; }
}
