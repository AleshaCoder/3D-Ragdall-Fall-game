using Assets.Source.InputService.Scripts;
using System;
using UnityEngine;

namespace RootMotion.Demos
{
    public class MobileUserControlMelee : UserControlThirdPerson
    {
        [SerializeField] private Killing _killing;
        private IInputMap _input;

        public void Construct(IInputMap input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));

            _input.Moving += OnMove;
            _input.Jumping += OnJump;
            _input.Attacking += (int index) => state.actionIndex = index;
            _input.Killing += _killing.Kill;
        }

        private void OnJump()
        {
            state.jump = canJump;
        }

        protected void OnMove(Vector2 direction)
        {
            Vector3 move = cam.rotation * new Vector3(direction.x, 0f, direction.y).normalized;

            if (move != Vector3.zero)
            {
                Vector3 normal = transform.up;
                Vector3.OrthoNormalize(ref normal, ref move);
                state.move = move;
            }
            else state.move = Vector3.zero;

            float walkMultiplier = (walkByDefault ? 0.5f : 1);

            state.move *= walkMultiplier;

            state.lookPos = transform.position + cam.forward * 100f;
        }

        protected override void Update()
        { }
    }
}
