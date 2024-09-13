using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public struct CharacterDiscriptionButtons
    {
        [SerializeField] private List<CharacterDiscriptionTypeButton> _characterDiscriptionTypeButtons;

        public IReadOnlyList<CharacterDiscriptionTypeButton> CharacterDiscriptionTypeButtons => _characterDiscriptionTypeButtons;

        public bool TrySelectCategory(CharacterDiscriptionsTypes characterDiscriptionType, out CharacterDiscriptionTypeButton selectedButton)
        {
            selectedButton = null;

            foreach (var button in _characterDiscriptionTypeButtons)
            {
                if (button.CharacterDiscriptionsType == characterDiscriptionType)
                    selectedButton = button;
                else
                    button.gameObject.SetActive(false);
            }

            return selectedButton != null;
        }

        public void ShowCategories() => _characterDiscriptionTypeButtons.ForEach(b => b.gameObject.SetActive(true));
    }
}
