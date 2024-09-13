using System;
using UnityEngine;
using DG.Tweening;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharacterDiscriptionSubclassesWindow
    {
        [SerializeField] private RectTransform _characterDiscriptionTransform;
        [SerializeField] private RectTransform _hidePosition;
        [SerializeField] private RectTransform _showPosition;
        [SerializeField, Min(0f)] private float _subclassesMenuOpeningSpeed = 0.5f;

        private Joystick _joystick;
        private Tween _windowTween;

        public void Construct(Joystick joystick)
        {
            _joystick = joystick ?? throw new ArgumentNullException(nameof(joystick));
        }

        public void Show()
        {
            _joystick.gameObject.SetActive(false);
            MoveWindow(_showPosition.localPosition);
        }

        public void Hide()
        {
            _joystick.gameObject.SetActive(true);
            MoveWindow(_hidePosition.localPosition);
        }

        private void MoveWindow(Vector3 position)
        {
            _windowTween?.Kill();
            _windowTween = _characterDiscriptionTransform.transform.DOLocalMove(position, _subclassesMenuOpeningSpeed).SetUpdate(true);
        }
    }
}
