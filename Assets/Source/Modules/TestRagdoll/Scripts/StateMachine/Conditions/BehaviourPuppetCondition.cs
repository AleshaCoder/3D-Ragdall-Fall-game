using RootMotion.Dynamics;
using UnityEngine;

public class BehaviourPuppetCondition : StateTransitionCondition
{
    [SerializeField] private BehaviourPuppet _behaviourPuppet;

    protected override bool OnCanEntry()
    {
        return _behaviourPuppet.state != BehaviourPuppet.State.Puppet;
    }
}
