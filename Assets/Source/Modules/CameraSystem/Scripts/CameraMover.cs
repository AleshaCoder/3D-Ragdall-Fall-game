using UnityEngine;
using System;

namespace Assets.Source.CameraSystem.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class CameraMover : MonoBehaviour
    {
        private const float DeviationError = 0.05f;
        private const float InterpolationKoef = 1f;
        private const float DragValue = 10f;
        private const float ColliderRadius = 0.5f;

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private CameraMoverConfig[] _configs;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;

        private CameraMoverConfig _currentConfig;
        private Vector3 _targetPosition;
        private ICameraInput _input;
        private float _cameraAngle = 0f;
        private float _cameraPivotAngle = 0f;
        private bool _isRotating = false;
        private bool _isMobile = false;
        private bool _rotatingBlocked = false;

        public void Construct(ICameraInput input, CameraConfigType type)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));

            _cameraAngle = _camera.transform.localEulerAngles.x;
            _cameraPivotAngle = _cameraPivot.transform.localEulerAngles.y;
            SetConfig(type);
            AdjustPhysicalSettings();

            _input.Moving += OnMove;
            _input.AlternativePointerMoving += OnRotate;
            _input.AlternativePointerUp += OnRotateEnded;
        }

        public void Dispose()
        {
            _input.Moving -= OnMove;
            _input.AlternativePointerMoving -= OnRotate;
            _input.AlternativePointerUp -= OnRotateEnded;
        }

        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy)
                MoveRB();
        }

        private void LateUpdate()
        {
            if (gameObject.activeInHierarchy)
                Rotate();
        }

        public void BlockRotating(bool blocked) => _rotatingBlocked = blocked;

        private void SetConfig(CameraConfigType type)
        {
            if (_configs.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(_configs));

            foreach (var config in _configs)
                if (config.Type == type)
                    _currentConfig = config;

            if (_currentConfig == null)
                throw new ArgumentNullException(nameof(_currentConfig));

            _isMobile = type == CameraConfigType.Mobile;
        }

        private void AdjustPhysicalSettings()
        {
            _capsuleCollider.isTrigger = false;
            _capsuleCollider.center = Vector3.zero;
            _capsuleCollider.radius = ColliderRadius;
            _capsuleCollider.direction = 1;
            _rigidbody.mass = 0f;
            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.freezeRotation = true;
            _rigidbody.drag = DragValue;
        }

        private void MoveRB() => _rigidbody.velocity = _currentConfig.MovingSpeed * (_targetPosition.z * _camera.transform.forward + _targetPosition.x * _camera.transform.right);

        private void Rotate()
        {
            if (gameObject.activeInHierarchy == false)
                return;
            if (_rotatingBlocked && _isMobile)
                return;

            if (_isRotating == false)
                return;

            _camera.transform.localEulerAngles = Vector3.Lerp(_camera.transform.localEulerAngles, _cameraAngle * Vector3.right, InterpolationKoef);
            _cameraPivot.localEulerAngles = Vector3.Lerp(_cameraPivot.localEulerAngles, _cameraPivotAngle * Vector3.up, InterpolationKoef);
        }

        private void OnMove(Vector2 direction)
        {
            if (gameObject.activeInHierarchy)
                _targetPosition = new(ConvertValue(direction.x), _targetPosition.y, ConvertValue(direction.y));
        }

        private void OnRotateEnded(Vector3 position) => _isRotating = false;

        private void OnRotate(Vector2 value)
        {
            if (gameObject.activeInHierarchy == false)
                return;
            _isRotating = true;
            _cameraAngle -= value.y * _currentConfig.VerticalRotateSensitivity * Time.fixedDeltaTime;
            _cameraAngle = Mathf.Clamp(_cameraAngle, _currentConfig.MinYAngle, _currentConfig.MaxYAngle);
            _cameraPivotAngle += value.x * _currentConfig.HorizontalRotateSensitivity * Time.fixedDeltaTime;
        }

        private float ConvertValue(float value)
        {
            if (Mathf.Approximately(value, 0f))
                return 0f;

            value = Mathf.Clamp(value, -1f, 1f);

            if (Mathf.Abs(value) <= DeviationError)
                value = 0f;

            return value;
        }
    }
}