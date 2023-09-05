using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    public void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Add(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }

    public void Subtract(int amount)
    {
        Add(-amount);
        if (_currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }


}
