using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Source.Extensions.Scripts;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharacterDiscriptionSubclassScroll
    {
        [SerializeField] private List<CharacterDiscriptionSubclassButton> _characterDiscriptionSubclassButtons;
        [SerializeField] private List<CharacterDiscriptionSubclass> _characterDiscriptionSubclasses;

        private CanvasGroup _contentCanvas;
        private IScrollNavigation _scrollNavigation;
        private CharacterDiscriptionSubclass _activeCharacterSubclass;

        public bool SubclassIsNull => _activeCharacterSubclass == null;

        public void Construct(CanvasGroup contentCanvas, IScrollNavigation scrollNavigation)
        {
            if (_characterDiscriptionSubclassButtons.Count != _characterDiscriptionSubclasses.Count)
                throw new ArgumentException("The number of buttons must match the number of subclasses");

            _contentCanvas = contentCanvas ?? throw new ArgumentNullException(nameof(contentCanvas));
            _scrollNavigation = scrollNavigation ?? throw new ArgumentNullException(nameof(scrollNavigation));

            _characterDiscriptionSubclassButtons.ForEach(button => button.OnClick += OnCharacterDiscriptionSubclassButtonClick);
            _characterDiscriptionSubclasses.ForEach(scroll => scroll.ObjectsScroll.Construct(_scrollNavigation));
        }

        public void Dispose()
        {
            _characterDiscriptionSubclassButtons.ForEach(button => button.OnClick -= OnCharacterDiscriptionSubclassButtonClick);
            _characterDiscriptionSubclasses.ForEach(scroll => scroll.ObjectsScroll.Dispose());
        }

        public void Hide() => _activeCharacterSubclass.ObjectsScroll.Hide();
        public void Show() => _activeCharacterSubclass.ObjectsScroll.Show();

        private void OnCharacterDiscriptionSubclassButtonClick(CharacterDiscriptionSubclassesTypes subclassType)
        {
            if(DetermineSubclass(subclassType, out CharacterDiscriptionSubclass activeSubclass))
            {
                if (SubclassIsNull)
                {
                    SelectSubclass(activeSubclass);
                }
                else
                {
                    _activeCharacterSubclass.SubclassButton.DisableSprite();
                    _activeCharacterSubclass.ObjectsScroll.Hide();

                    if (_activeCharacterSubclass == activeSubclass)
                        DeselectSubclass();
                    else
                        SelectSubclass(activeSubclass);
                }
            }
        }

        private bool DetermineSubclass(CharacterDiscriptionSubclassesTypes characterDiscriptionSubclassType, out CharacterDiscriptionSubclass activeSubclass)
        {
            activeSubclass = null;

            foreach (var subclass in _characterDiscriptionSubclasses)
            {
                if (subclass.SubclassType == characterDiscriptionSubclassType)
                    activeSubclass = subclass;
            }

            return activeSubclass != null;
        }

        private void SelectSubclass(CharacterDiscriptionSubclass subclass)
        {
            _activeCharacterSubclass = subclass;
            _activeCharacterSubclass.SubclassButton.EnableSprite();
            _activeCharacterSubclass.ObjectsScroll.Show();
            _contentCanvas.EnableGroup();
        }

        private void DeselectSubclass()
        {
            _activeCharacterSubclass = null;
            _contentCanvas.DisableGroup();
        }
    }
}
