using UnityEngine;
using Assets.Source.Structs.Scripts;

namespace Assets.Source.Entities.Scripts
{
    public class Weapon : Item, IItemType<WeaponType>
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
    }
}