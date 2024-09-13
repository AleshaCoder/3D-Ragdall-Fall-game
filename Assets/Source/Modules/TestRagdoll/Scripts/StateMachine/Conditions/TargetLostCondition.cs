using UnityEngine;

public class TargetLostCondition : StateTransitionCondition
{
    [SerializeField] private Character _character;

    protected override bool OnCanEntry()
    {
        if (_character.Target == null || _character.Target.IsAlive == false) return true;
        return false;
    }
}
