using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Assets.Source.Structs.Scripts;
using Assets.Source.Entities.Scripts;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    [CreateAssetMenu(menuName = "Create scriptable object of all items", fileName = "CreatedItems", order = 0)]
    public class CreatedScriptableObjects : ScriptableObject
    {
        [Header("Characters")]
        [SerializeField] private List<Unit> _characters;
        [Header("Main Characters")]
        [SerializeField] private List<MainCharacter> _mainCharacters;
        [Header("Items")]
        [SerializeField] private List<Building> _items;

        public bool TryGetCharacter(RagdollType ragdollType, out Item result)
        {
            result = _characters.FirstOrDefault(p => p.Type == ragdollType);

            return result != null;
        }

        public bool TryGetMainCharacter(RagdollType ragdollType, out Item result)
        {
            result = _mainCharacters.FirstOrDefault(p => p.Type == ragdollType);

            return result != null;
        }

        public bool TryGetItem(ItemsType itemType, out Item result)
        {
            result = _items.FirstOrDefault(p => p.Type == itemType);

            return result != null;
        }
    }
}