using UnityEngine;

namespace Assets.Source.InputService.Scripts
{
    public class KeyboardInput : InputMap
    {
        private KeyCode _hookKey;
        private KeyCode _kickKey;
        private KeyCode _killKey;
        private KeyCode _jumpKey;

        public KeyboardInput(KeyCode hookKey, KeyCode kickKey, KeyCode killKey, KeyCode jumpKey)
        {
            _hookKey = hookKey;
            _kickKey = kickKey;
            _killKey = killKey;
            _jumpKey = jumpKey;
        }

        protected override void Move()
        {
            if (Input.GetAxis(InputConstants.Vertical) != 0 || Input.GetAxis(InputConstants.Horizontal) != 0)
                OnMoving(new(Input.GetAxis(InputConstants.Horizontal), Input.GetAxis(InputConstants.Vertical)));
        }

        protected override void HandlePointer()
        {
            if (Input.GetMouseButtonDown(0))
                OnPointerDown(Input.mousePosition);

            if (Input.GetMouseButton(0))
                OnPointerMoving(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
                OnPointerUp(Input.mousePosition);

            if (Input.GetMouseButton(1))
                OnAlternativeMoving(new(Input.GetAxis(InputConstants.MouseX) *10, Input.GetAxis(InputConstants.MouseY)*10));

            if (Input.GetMouseButtonUp(1))
                OnAlternativeMovingEnded(Input.mousePosition);
        }

        protected override void Attack()
        {
            if (Input.GetKeyDown(_hookKey))
                OnAttack(1);
            else if (Input.GetKeyDown(_kickKey))
                OnAttack(2);
        }

        protected override void Jump()
        {
            if (Input.GetKeyDown(_jumpKey))
                OnJump();
        }

        protected override void Kill()
        {
            if (Input.GetKeyDown(_killKey))
                OnKill();
        }
    }
}