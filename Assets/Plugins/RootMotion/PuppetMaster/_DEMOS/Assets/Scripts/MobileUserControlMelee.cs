using Assets.Source.InputService.Scripts;
using System;
using UnityEngine;

namespace RootMotion.Demos
{
    public class MobileUserControlMelee : UserControlThirdPerson
    {
        [SerializeField] private Killing _killing;
        [SerializeField] private Camera _camera;
        private IInputMap _input;
        private float _actionDelay = 1f;
        private float _time = 0f;

        public void Construct(IInputMap input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));

            _input.Moving += OnMove;
            _input.Jumping += OnJump;
            _input.Attacking += OnAttack;
            _input.Killing += _killing.Kill;
            _time = Time.time;
        }

        private void OnAttack(int index)
        {
            if (Time.time - _time > _actionDelay)
            {
                Debug.Log(Time.time - _time);
                state.actionIndex = index;
                _time = Time.time;
            }
        }

        private void OnJump()
        {
            state.jump = canJump;
        }

        protected void OnMove(Vector2 direction)
        {
            Vector3 move = _camera.transform.rotation * new Vector3(direction.x, 0f, direction.y);

            if (move != Vector3.zero)
            {
                state.move = move;
            }
            else state.move = Vector3.zero;

            float walkMultiplier = (walkByDefault ? 0.5f : 1);

            state.move *= walkMultiplier;

            //state.lookPos = transform.position + cam.forward * 100f;
        }

        protected override void Update()
        { }
    }
}
