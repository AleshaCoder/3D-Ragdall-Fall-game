using UnityEngine;

public class CharacterProvider : MonoBehaviour, ICharacterTarget
{
    [SerializeField] private Character _character;

    public TeamType TeamType => _character.TeamType;

    public Transform Transform => _character.Transform;

    public bool IsAlive => _character.IsAlive;
}
