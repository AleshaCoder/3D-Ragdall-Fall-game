using System;
using UnityEngine;

[Serializable]
public class Health : IHealth
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    private bool _isDead;

    public float MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;

    public bool IsDead => _isDead;

    public event Action HealthChanged;
    public event Action Died;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public void Decrease(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke();

        if (_currentHealth == 0)
        {
            _isDead = true;
            Died?.Invoke();
        }
    }

    public void Increase(float amount)
    {
        if (_isDead)
            return;

        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke();
    }

    public void Kill() => Decrease(_currentHealth);

    public void Resurrect()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
        HealthChanged?.Invoke();
    }
}
