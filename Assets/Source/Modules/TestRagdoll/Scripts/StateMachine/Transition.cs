using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Transition
{
    [SerializeField] private List<StateTransitionCondition> _entryCondition;

    //[field: SerializeField] public State From { get; private set; }
    [field: SerializeField] public State To { get; private set; }

    public IReadOnlyList<StateTransitionCondition> EntryConditions => _entryCondition;
}
