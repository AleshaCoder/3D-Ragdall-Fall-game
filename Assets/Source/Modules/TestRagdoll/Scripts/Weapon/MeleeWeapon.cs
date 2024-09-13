using RootMotion.Dynamics;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private PuppetMaster _master;

    private bool _canAttack = true;
    private bool _canApplyDamage = false;
    private Coroutine _coroutine;

    public override void Attack()
    {
        if (_canAttack == false)
            return;

        _coroutine = StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        _canAttack = false;
        _characterAnimation.PlayAnimation();
        yield return new WaitUntil(() => _characterAnimation.Animator.GetCurrentAnimatorStateInfo(0).IsName(_characterAnimation.StateName));
        _canApplyDamage = true;
        yield return new WaitUntil(() => _characterAnimation.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && _characterAnimation.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f);
        yield return new WaitUntil(() => _characterAnimation.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f);
        _coroutine = null;
        _canApplyDamage = false;
        _canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canApplyDamage == false)
            return;

        foreach (var muscle in _master.muscles)
        {
            if (muscle.joint.gameObject == other.gameObject)
                return;
        }

        if (other.TryGetComponent(out IDamageable damageable) )
        {
            damageable.TakeDamage(new DamageArgs(Damage, UnPin, transform.forward * Force, transform.position));
        }
    }
}
