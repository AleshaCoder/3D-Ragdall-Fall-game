using RootMotion.Dynamics;
using UnityEngine;

public class StatePuppetBehaviour : CharacterState
{
    [SerializeField] private BehaviourPuppet _behaviourPuppet;

    protected override void OnEnter()
    {
        if (_behaviourPuppet.state == BehaviourPuppet.State.Puppet)
            CharacterAnimation.PlayAnimation();
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnTick()
    {
        
    }
}
