using UnityEngine;

public class SolidWeapon : Weapon
{
    private bool _canApplyDamage = true;

    public override void Attack() { }

    private void OnTriggerEnter(Collider other)
    {
        if (_canApplyDamage == false)
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(new DamageArgs(Damage, UnPin, transform.forward * Force, transform.position));
        }
    }
}