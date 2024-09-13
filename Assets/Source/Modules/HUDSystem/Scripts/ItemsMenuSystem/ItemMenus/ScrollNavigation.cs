using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class ScrollNavigation : IScrollNavigation
    {
        private const float One = 1;

        [SerializeField] private Button _forwardButton;
        [SerializeField] private Button _backButton;

        private ObjectsScroll _activeObjectsScroll;
        private bool _canMove = true;
        private Tween _windowTween;

        public bool ForwardInteractable => _forwardButton.interactable;
        public bool BackInteractable => _backButton.interactable;

        public void Construct()
        {
            _forwardButton.onClick.AddListener(MoveForward);
            _backButton.onClick.AddListener(MoveBack);
        }

        public void Dispose()
        {
            _forwardButton.onClick.RemoveListener(MoveForward);
            _backButton.onClick.RemoveListener(MoveBack);
        }

        public void SetScroll(ObjectsScroll objectsScroll)
        {
            _windowTween?.Kill();
            _activeObjectsScroll = objectsScroll ?? throw new ArgumentNullException(nameof(objectsScroll));
        }

        public void ChangeForwardButtonEnable(bool enable) => _forwardButton.interactable = enable;

        public void ChangeBackButtonEnable(bool enable) => _backButton.interactable = enable;

        private void MoveForward()
        {
            if (_canMove == false)
                return;

            double bbb = Math.Floor(Math.Abs(_activeObjectsScroll.ScrollRect.content.localPosition.x) / _activeObjectsScroll.WidthStep);
            MoveScroll((float)bbb + One);
        }

        private void MoveBack()
        {
            if (_canMove == false)
                return;

            double step = Math.Floor(Math.Abs(_activeObjectsScroll.ScrollRect.content.localPosition.x) / _activeObjectsScroll.WidthStep);
            MoveScroll((float)step - (_activeObjectsScroll.ScrollRect.content.localPosition.x % step == 0 ? One : 0));
        }

        private void MoveScroll(float position)
        {
            if (_canMove == false)
                return;

            _activeObjectsScroll.ScrollRect.StopMovement();
            _canMove = false;
            _windowTween?.Kill();
            _windowTween = _activeObjectsScroll.ScrollRect.content.transform.DOLocalMoveX(position * -_activeObjectsScroll.WidthStep, 0.3f).OnKill(() => _canMove = true).SetUpdate(true);
        }
    }
}
