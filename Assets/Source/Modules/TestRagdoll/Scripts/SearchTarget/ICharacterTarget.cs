using UnityEngine;

public interface ICharacterTarget : ITarget
{
    public TeamType TeamType { get; }
}
