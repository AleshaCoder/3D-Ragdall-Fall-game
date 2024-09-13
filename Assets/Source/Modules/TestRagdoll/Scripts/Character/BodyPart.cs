using RootMotion.Dynamics;
using System;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField] private PuppetMaster _master;

    private MuscleCollisionBroadcaster _collisionBroadcaster;

    private void Awake()
    {
        _health.Initialize();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.Died -= OnDied;
    }

    private void Start()
    {
        _collisionBroadcaster = GetComponent<MuscleCollisionBroadcaster>();
    }

    private void OnDied()
    {
        _master.state = PuppetMaster.State.Dead;
    }

    private void OnHealthChanged()
    {
        
    }

    public void TakeDamage(DamageArgs damageArgs)
    {
        _health.Decrease(damageArgs.Damage);
        _collisionBroadcaster.Hit(damageArgs.Unpin, damageArgs.Force, damageArgs.Point);
        Debug.Log($"{_master.transform.parent.gameObject.name} take damage");
    }
}
