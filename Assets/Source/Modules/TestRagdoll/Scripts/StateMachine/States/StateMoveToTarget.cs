using UnityEngine;

public class StateMoveToTarget : CharacterState
{
    [SerializeField] private Character _character;
    [SerializeField] private float _speed;
    
    protected override void OnEnter()
    {
        CharacterAnimation.PlayAnimation();
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnTick()
    {
        Vector3 direction = _character.Target.Transform.position - _character.RigidBody.transform.position;
        direction.y = 0;
        _character.RigidBody.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _character.RigidBody.velocity = _character.RigidBody.transform.forward * (Time.deltaTime * _speed);
    }
}
