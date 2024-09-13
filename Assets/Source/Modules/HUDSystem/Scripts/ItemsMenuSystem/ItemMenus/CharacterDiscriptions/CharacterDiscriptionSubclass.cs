using System;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharacterDiscriptionSubclass
    {
        [field: SerializeField] public ObjectsScroll ObjectsScroll { get; private set; }
        [field: SerializeField] public CharacterDiscriptionSubclassesTypes SubclassType { get; private set; }
        [field: SerializeField] public CharacterDiscriptionSubclassButton SubclassButton { get; private set; }
     }
}
