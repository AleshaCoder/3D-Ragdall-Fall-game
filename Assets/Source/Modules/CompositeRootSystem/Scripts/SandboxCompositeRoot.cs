using System;
using UnityEngine;
using Assets.Source.HUDSystem.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.CameraSystem.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using Assets.Source.GravityGunSystem.Scripts;
using UnityEngine.UI;
using RootMotion.Demos;

namespace Assets.Source.CompositeRootSystem.Scripts
{
    public class SandboxCompositeRoot : MonoBehaviour
    {
        [SerializeField] private SandboxHUD _sandboxHUD;
        [SerializeField] private bool _isMobile;
        [SerializeField] private CameraMover _mover;
        [SerializeField] private RotateCanvas _selectionCircle;
        [SerializeField] private Camera _camera;

        [SerializeField] private MobileUserControlMelee _mobileUserControlMelee;
        [SerializeField] private CameraCharacterController _cameraCharacterController;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Button _hookButton;
        [SerializeField] private Button _kickButton;
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _killButton;
        [SerializeField] private CharacterResetter _resetter;

        [SerializeField] LayerMask _layerMask;
        [SerializeField] private GravityGunHandler _gravityGunHandler;

        private SandboxHUDComponents _hudComponents;
        private GravityGunComponents _gravityGunComponents;
        private InputManager _inputManager;
        private InputHandler _inputHandler;
        private RayCaster _rayCaster;
        private IInputMap _inputMap;
        private CameraConfigType _cameraConfigType;

        private void Awake()
        {
            _rayCaster = new(_camera);
            
            _inputMap = _isMobile ? 
                new TouchInput(_movementJoystick, _rotationJoystick, _kickButton, _hookButton, _killButton, _jumpButton)
                : 
                new KeyboardInput(KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Q, KeyCode.Space);

            _inputManager = new(_inputMap);
            _cameraConfigType = _isMobile ? CameraConfigType.Mobile : CameraConfigType.PC;
            _mover.Construct(_inputMap as ICameraInput, _cameraConfigType);
            _mobileUserControlMelee.Construct(_inputMap);
            _cameraCharacterController.Construct(_inputMap);
            InitializeGravityGun();
            InitializeHUD();
            _inputHandler = new(_inputMap, _rayCaster, _sandboxHUD.ItemsSpawner, _selectionCircle, _layerMask);
        }

        private void OnDestroy()
        {
            _sandboxHUD.Dispose();
            _gravityGunHandler.Dispose();
            _mover.Dispose();
        }

        private void Update()
        {
            if (_selectionCircle.gameObject.activeSelf)
                _selectionCircle.Tick();

            _gravityGunHandler.Tick();
            _inputManager.Tick();
            _sandboxHUD.ItemsSpawner.Tick();
            //_mover.BlockRotating(blocked: _sandboxHUD.ItemsSpawner.IsActive);
        }

        private void FixedUpdate()
        {
            _gravityGunHandler.FixedTick();
        }

        private void InitializeHUD()
        {
            _hudComponents = new()
            {
                IsMobile = _isMobile,
                Input = _inputMap ?? throw new ArgumentNullException(nameof(_inputMap)),
                RayCaster = _rayCaster ?? throw new ArgumentNullException(nameof(_rayCaster)),
                Joystick = _movementJoystick ?? throw new ArgumentNullException(nameof(_movementJoystick)),
                CharacterResetter = _resetter ?? throw new ArgumentNullException(nameof(_resetter))
            };

            _sandboxHUD.Construct(_hudComponents);
        }

        private void InitializeGravityGun()
        {
            _gravityGunComponents = new(_inputMap as IGravityGunInput, _camera, _gravityGunHandler);
            _gravityGunHandler.Construct(_gravityGunComponents);
        }
    }
}