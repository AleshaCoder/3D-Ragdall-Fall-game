using RootMotion.Dynamics;
using UnityEngine;

public class TargetExistsCondition : StateTransitionCondition
{
    [SerializeField] private Character _character;

    protected override bool OnCanEntry()
    {
        if (_character.Target == null) return false;
        return true;
    }
}
