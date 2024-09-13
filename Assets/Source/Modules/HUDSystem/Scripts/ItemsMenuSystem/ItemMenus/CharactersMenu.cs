using System;
using UnityEngine;
using DG.Tweening;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharactersMenu : ItemMenu
    {
        [SerializeField] private CharacterDiscriptionWindow _characterDiscriptionWindow;
        [SerializeField] private CharacterDiscriptionButtons _characterDiscriptionButtons;
        [SerializeField] private CharacterDiscriptionSubclassesWindow _characterDiscriptionSubclassesWindow;
        [SerializeField] private CharacterDiscriptionSubclassScroll _characterDiscriptionSubclassScroll;
        [SerializeField, Min(0f)] private float _replaceMenuBarsSpeed = 0.3f;

        private CharacterDiscriptionTypeButton _activeCharacterDiscriptionTypeButton;
        private Vector3 _activeButtonVerticalPosition = new Vector3(1000, 1000);
        private Tween _windowTween;

        private bool _typeButtonIsNull => _activeCharacterDiscriptionTypeButton == null;

        public bool SubclassIsNull => _characterDiscriptionSubclassScroll.SubclassIsNull;

        public void Construct(CanvasGroup contentCanvas, IScrollNavigation scrollNavigation, Joystick joystick)
        {
            _characterDiscriptionSubclassScroll.Construct(contentCanvas, scrollNavigation);
            _characterDiscriptionSubclassesWindow.Construct(joystick);

            foreach (var characterDiscriptionTypeButton in _characterDiscriptionButtons.CharacterDiscriptionTypeButtons)
            {
                characterDiscriptionTypeButton.Construct();
                characterDiscriptionTypeButton.OnClick += OnCharacterDiscriptionTypeButtonClick;

                if (characterDiscriptionTypeButton.InitVerticalPosition.y < _activeButtonVerticalPosition.y)
                    _activeButtonVerticalPosition = characterDiscriptionTypeButton.InitVerticalPosition;
            }
        }

        public void Dispose()
        {
            _characterDiscriptionSubclassScroll.Dispose();

            foreach (var characterDiscriptionTypeButton in _characterDiscriptionButtons.CharacterDiscriptionTypeButtons)
                characterDiscriptionTypeButton.OnClick -= OnCharacterDiscriptionTypeButtonClick;
        }

        public override void Close()
        {
            base.Close();
            _characterDiscriptionWindow.Hide();

            if (_typeButtonIsNull == false)
                _characterDiscriptionSubclassesWindow.Hide();

            if (SubclassIsNull == false)
                _characterDiscriptionSubclassScroll.Hide();
        }

        public override void Open()
        {
            base.Open();
            _characterDiscriptionWindow.Show();

            if (_typeButtonIsNull == false)
                _characterDiscriptionSubclassesWindow.Show();

            if (SubclassIsNull == false)
                _characterDiscriptionSubclassScroll.Show();
        }

        private void OnCharacterDiscriptionTypeButtonClick(CharacterDiscriptionsTypes characterDiscriptionsType)
        {
            if (_characterDiscriptionButtons.TrySelectCategory(characterDiscriptionsType, out CharacterDiscriptionTypeButton selectedButton))
            {
                if (_typeButtonIsNull)
                {
                    _activeCharacterDiscriptionTypeButton = selectedButton;
                    _activeCharacterDiscriptionTypeButton.EnableSprite();
                    MoveWindow(_activeButtonVerticalPosition, _activeCharacterDiscriptionTypeButton.transform);
                    _characterDiscriptionSubclassesWindow.Show();
                }
                else
                {
                    _activeCharacterDiscriptionTypeButton.DisableSprite();
                    MoveWindow(_activeCharacterDiscriptionTypeButton.InitVerticalPosition, _activeCharacterDiscriptionTypeButton.transform);
                    _characterDiscriptionButtons.ShowCategories();
                    _activeCharacterDiscriptionTypeButton = null;
                    _characterDiscriptionSubclassesWindow.Hide();
                }
            }
        }

        private void MoveWindow(Vector3 position, Transform transform)
        {
            _windowTween?.Kill();
            _windowTween = transform.DOLocalMove(position, _replaceMenuBarsSpeed).SetUpdate(true);
        }
    }
}
