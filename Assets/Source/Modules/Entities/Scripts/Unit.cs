using UnityEngine;
using Assets.Source.Structs.Scripts;

namespace Assets.Source.Entities.Scripts
{
    public class Unit : Item, IItemType<RagdollType>
    {
        [field: SerializeField] public RagdollType Type { get; private set; }
    }
}