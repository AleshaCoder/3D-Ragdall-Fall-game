using UnityEngine;
using Assets.Source.Structs.Scripts;
using Assets.Source.GravityGunSystem.Scripts;

namespace Assets.Source.Entities.Scripts
{
    public class Building : Item, IItemType<ItemsType>, IGrabbable
    {
        [field: SerializeField] public ItemsType Type { get; private set; }
    }
}