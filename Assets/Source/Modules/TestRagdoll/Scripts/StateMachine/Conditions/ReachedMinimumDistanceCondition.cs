using UnityEngine;

public class ReachedMinimumDistanceCondition : StateTransitionCondition
{
    [SerializeField] private Character _character;

    protected override bool OnCanEntry()
    {
        return _character.Target == null || Vector3.Distance(_character.RigidBody.transform.position, _character.Target.Transform.position) <= _character.Weapon.MinDistanceToAttack;
    }
}
