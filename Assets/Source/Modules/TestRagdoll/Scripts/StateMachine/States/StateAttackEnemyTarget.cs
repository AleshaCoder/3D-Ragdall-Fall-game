using UnityEngine;

public class StateAttackEnemyTarget : CharacterState
{
    [SerializeField] private Character _character;

    protected override void OnEnter()
    {
        //CharacterAnimation.PlayAnimation();
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnTick()
    {
        Vector3 direction = _character.Target.Transform.position - _character.RigidBody.transform.position;
        direction.y = 0;
        _character.RigidBody.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _character.Weapon.Attack();
    }
}
