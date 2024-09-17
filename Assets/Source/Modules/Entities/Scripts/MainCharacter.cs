using UnityEngine;
using Assets.Source.Structs.Scripts;

namespace Assets.Source.Entities.Scripts
{
    public class MainCharacter : Item, IItemType<RagdollType>, IDontDestroyableFromScene
    {
        [field: SerializeField] public RagdollType Type { get; private set; }
    }
}