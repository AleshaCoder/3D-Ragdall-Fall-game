using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType _type;
    [SerializeField] private float _minDistanceToAttack;
    [SerializeField] private float _damage;
    [SerializeField] private float _unPin;
    [SerializeField] private float _force;

    public float Damage => _damage;
    public float UnPin => _unPin;
    public float Force => _force;

    public WeaponType Type => _type;
    public float MinDistanceToAttack => _minDistanceToAttack;

    public abstract void Attack();
}
