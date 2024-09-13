using System;
using UnityEngine;
using Assets.Source.CameraSystem.Scripts;
using Assets.Source.GravityGunSystem.Scripts;

namespace Assets.Source.InputService.Scripts
{
    public abstract class InputMap : IInputMap, ICameraInput, IGravityGunInput, ICharacterInput
    {
        private const float SecondsForDoubleClick = 0.2f;
        private const float SecondsForLongClick = 0.8f;

        private Vector3 _pointerPosition;
        private bool _isLongClick;
        private float _timePassed = 0f;
        private float _longTimePassed = 0f;

        public event Action<Vector2> Moving;
        public event Action<Vector2> AlternativePointerMoving;
        public event Action<Vector3> AlternativePointerUp;
        public event Action<Vector3> PointerDown;
        public event Action<Vector3> PointerMoving;
        public event Action<Vector3> PointerUp;
        public event Action<Vector3> DoubleClicked;
        public event Action<Vector3> LongClicked;

        public event Action<int> Attacking;
        public event Action Jumping;
        public event Action Killing;

        public virtual void Tick(float deltaTime)
        {
            Move();
            Attack();
            Jump();
            Kill();
            HandlePointer();
            Timer(deltaTime);
        }

        protected abstract void Move();
        protected abstract void Attack();
        protected abstract void Jump();
        protected abstract void Kill();
        protected abstract void HandlePointer();
        protected void OnMoving(Vector2 direction) => Moving?.Invoke(direction);
        protected void OnAlternativeMoving(Vector2 direction) => AlternativePointerMoving?.Invoke(direction);
        protected void OnAlternativeMovingEnded(Vector3 position) => AlternativePointerUp?.Invoke(position);

        protected void OnPointerMoving(Vector3 position) => PointerMoving?.Invoke(position);

        protected void OnPointerUp(Vector3 position)
        {
            _isLongClick = false;
            _longTimePassed = 0f;
            PointerUp?.Invoke(position);
        }

        protected void OnPointerDown(Vector3 position)
        {
            _pointerPosition = position;
            _isLongClick = true;

            if (_timePassed < SecondsForDoubleClick)
            {
                DoubleClicked?.Invoke(position);
                _timePassed = SecondsForDoubleClick;
                return;
            }

            PointerDown?.Invoke(position);
            _timePassed = 0f;
        }

        protected void OnAttack(int id) => Attacking?.Invoke(id);
        protected void OnJump() => Jumping?.Invoke();
        protected void OnKill() => Killing?.Invoke();

        protected void ResetLongClick() => _isLongClick = false;

        private void Timer(float deltaTime)
        {
            if (_timePassed < SecondsForDoubleClick)
                _timePassed += deltaTime;

            if (_isLongClick)
                _longTimePassed += deltaTime;
            else
                return;

            if (_longTimePassed > SecondsForLongClick)
            {
                LongClicked?.Invoke(_pointerPosition);
                _isLongClick = false;
                _longTimePassed = 0f;
            }
        }
    }
}