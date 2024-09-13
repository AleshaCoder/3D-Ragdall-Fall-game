using UnityEngine;

public abstract class CharacterState : State
{
    [field: SerializeField] protected CharacterAnimation CharacterAnimation { get; private set; }
}