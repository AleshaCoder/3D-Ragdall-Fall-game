using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Source.InputService.Scripts
{
    public class TouchInput : InputMap
    {
        private IInputJoystick _joystickMove;
        private IInputJoystick _joystickRotate;
        private Button _hookButton;
        private Button _kickButton;
        private Button _killButton;
        private Button _jumpButton;
        private Touch _touch;
        private CustomTouch _customTouch;

        public TouchInput(IInputJoystick joystickMove, IInputJoystick joystickRotate, Button kick, Button hook, Button kill, Button jump)
        {
            _joystickMove = joystickMove ?? throw new ArgumentNullException(nameof(joystickMove));
            _joystickRotate = joystickRotate ?? throw new ArgumentNullException(nameof(joystickRotate));
            _hookButton = hook;
            _kickButton = kick;
            _killButton = kill;
            _jumpButton = jump;
            _customTouch = null;

            _hookButton.onClick.AddListener(OnHook);
            _kickButton.onClick.AddListener(OnKick);
            _killButton.onClick.AddListener(OnKill);
            _jumpButton.onClick.AddListener(OnJump);
        }

        private void OnHook() => OnAttack(1);
        private void OnKick() => OnAttack(2);

        protected override void Move()
        {
            OnMoving(new(_joystickMove.Horizontal, _joystickMove.Vertical));
            OnAlternativeMoving(new(_joystickRotate.Horizontal, _joystickRotate.Vertical));
        }

        protected override void HandlePointer()
        {
            if (Input.GetMouseButtonUp(0))
                OnPointerUp(Input.mousePosition);

            if (Input.touchCount == 0)
                return;

            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    OnPointerDown(touch.position);

                    if (IsOverUI(touch))
                        ResetLongClick();
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    OnPointerMoving(touch.position);
                    ResetLongClick();
                }

                if (touch.phase == TouchPhase.Ended)
                    OnPointerUp(touch.position);
            }

            if (_customTouch == null)
                foreach (var touch in Input.touches)
                    if (IsOverUI(touch) == false && touch.phase == TouchPhase.Began)
                            _customTouch = new(touch.fingerId);

            if (_customTouch != null)
                if(TryGetTouchByFingerId(out _touch))
                    HandleTouch(_touch);
        }

        private void HandleTouch(Touch touch)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                ResetLongClick();
                OnAlternativeMoving(new(touch.deltaPosition.x, touch.deltaPosition.y));
            }

            if (touch.phase == TouchPhase.Ended)
            {
                OnAlternativeMovingEnded(touch.position);
                _customTouch = null;
            }
        }

        private bool TryGetTouchByFingerId(out Touch touch)
        {
            foreach (var checkTouch in Input.touches)
            {
                if (checkTouch.fingerId == _customTouch.FingerId)
                {
                    touch = checkTouch;
                    return true;
                }
            }

            touch = default;
            return false;
        }

        private bool IsOverUI(Touch touch) => EventSystem.current.IsPointerOverGameObject(touch.fingerId);

        protected override void Attack(){}

        protected override void Jump()
        {
        }

        protected override void Kill()
        {
        }
    }

    internal class CustomTouch
    {
        internal int FingerId { get; private set; }

        internal CustomTouch(int fingerId) => FingerId = fingerId;
    }
}