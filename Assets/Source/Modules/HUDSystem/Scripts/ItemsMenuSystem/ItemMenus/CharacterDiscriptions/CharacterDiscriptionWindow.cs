using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharacterDiscriptionWindow
    {
        [SerializeField] private RectTransform _characterDiscriptionTransform;
        [SerializeField] private RectTransform _hidePosition;
        [SerializeField] private RectTransform _showPosition;
        [SerializeField, Min(0f)] private float _menuOpeningSpeed = 0.5f;

        private Tween _windowTween;

        public void Show() => MoveWindow(_showPosition.localPosition);

        public void Hide() => MoveWindow(_hidePosition.localPosition);

        private void MoveWindow(Vector3 position)
        {
            _windowTween?.Kill();
            _windowTween = _characterDiscriptionTransform.transform.DOLocalMove(position, _menuOpeningSpeed).SetUpdate(true);
        }
    }
}