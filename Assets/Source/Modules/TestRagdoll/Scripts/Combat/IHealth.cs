using System;
using UnityEngine;

public interface IHealth
{
    public float MaxHealth { get; }
    public float CurrentHealth { get; }
    public bool IsDead { get; }

    public event Action HealthChanged;
    public event Action Died;

    public void Increase(float amount);
    public void Decrease(float amount);
    public void Kill();
    public void Resurrect();
}
