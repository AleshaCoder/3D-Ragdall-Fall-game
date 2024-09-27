using Assets.Source.InputService.Scripts;
using System;
using System.Collections;
using TimeSystem;
using UnityEngine;

namespace RootMotion.Demos
{
    public class MobileUserControlMelee : UserControlThirdPerson
    {
        [SerializeField] private Killing _killing;
        [SerializeField] private FlyInDead _flyInDead;
        [SerializeField] private Camera _camera;
        [SerializeField] private CollisionForcer _hook;
        [SerializeField] private CollisionForcer _foot;
        private IInputMap _input;
        private float _actionDelay = 1f;
        private float _time = 0f;
        private bool _isInited = true;

        public void Construct(IInputMap input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));

            _input.Moving += OnMove;
            _input.Jumping += OnJump;
            _input.Attacking += OnAttack;
            _input.Killing += _killing.Kill;
            _hook.enabled = false;
            _foot.enabled = false;
            _time = Time.time;
            _isInited = true;
        }

        private void OnEnable()
        {
            if (_input != null && (_isInited == false))
            {
                _input.Moving += OnMove;
                _input.Jumping += OnJump;
                _input.Attacking += OnAttack;
                _input.Killing += _killing.Kill;
                _isInited = true;
            }
        }

        private void OnDisable()
        {
            _input.Moving -= OnMove;
            _input.Jumping -= OnJump;
            _input.Attacking -= OnAttack;
            _input.Killing -= _killing.Kill;
            _isInited = false;
        }

        private void OnDestroy()
        {
            _input.Moving -= OnMove;
            _input.Jumping -= OnJump;
            _input.Attacking -= OnAttack;
            _input.Killing -= _killing.Kill;
        }

        private void OnValidate()
        {
            var cf = transform.root.GetComponentsInChildren<CollisionForcer>();
            _foot = cf[0];
            _hook = cf[1];
        }

        private void OnAttack(int index)
        {
            if (Time.time - _time > _actionDelay)
            {
                if (index == 1)
                {
                    _hook.enabled = true;
                }
                else
                {
                    _foot.enabled = true;
                }

                state.actionIndex = index;
                _time = Time.time;
            }

            StartCoroutine(WaitEndOfAttack());
        }

        private IEnumerator WaitEndOfAttack()
        {
            yield return new WaitForSeconds(1f);
            _hook.enabled = false;
            _foot.enabled = false;
        }

        private void OnJump()
        {
            state.jump = canJump;
        }

        protected void OnMove(Vector2 direction)
        {
            Vector3 move = _camera.transform.rotation * new Vector3(direction.x, 0f, direction.y) * TimeService.Scale;

            if (move != Vector3.zero)
            {
                state.move = move;
            }
            else state.move = Vector3.zero;

            float walkMultiplier = (walkByDefault ? 0.5f : 1);

            state.move *= walkMultiplier;

            _flyInDead.Move(_camera.transform.rotation * new Vector3(direction.x, 0f, direction.y));

            //state.lookPos = transform.position + cam.forward * 100f;
        }

        protected override void Update()
        { }
    }
}
