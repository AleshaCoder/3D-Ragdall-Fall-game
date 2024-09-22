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
        [SerializeField] private GameObject _hook;
        [SerializeField] private GameObject _foot;
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
            _time = Time.time;
            _hook.SetActive(false);
            _foot.SetActive(false);
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

        private void OnAttack(int index)
        {
            if (Time.time - _time > _actionDelay)
            {
                if (index == 1)
                    _hook.SetActive(true);
                else if (index == 2)
                    _foot.SetActive(true);

                state.actionIndex = index;
                _time = Time.time;

                StartCoroutine(WaitFinishAttack(index));
            }
        }

        private IEnumerator WaitFinishAttack(int index)
        {
            yield return new WaitForSeconds(1f);
            if (index == 1)
                _hook.SetActive(false);
            else if (index == 2)
                _foot.SetActive(false);
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
