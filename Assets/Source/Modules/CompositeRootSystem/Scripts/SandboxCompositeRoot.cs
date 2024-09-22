using System;
using UnityEngine;
using Assets.Source.HUDSystem.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.CameraSystem.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using Assets.Source.GravityGunSystem.Scripts;
using UnityEngine.UI;
using RootMotion.Demos;
using RotationSystem;
using SkinsSystem;
using DamageSystem;

namespace Assets.Source.CompositeRootSystem.Scripts
{
    public class SandboxCompositeRoot : MonoBehaviour
    {
        [SerializeField] private SandboxHUD _sandboxHUD;
        [SerializeField] private bool _isMobile;
        [SerializeField] private CameraMover _mover;
        [SerializeField] private Camera _camera;

        [SerializeField] private SkinSelector _skinSelector;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Button _hookButton;
        [SerializeField] private Button _kickButton;
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _killButton;
        
        
        [SerializeField] LayerMask _layerMask;
        [SerializeField] private GravityGunHandler _gravityGunHandler;

        private SandboxHUDComponents _hudComponents;
        private ScoreRepository _scoreRepository;
        private Damage _damage;
        private GravityGunComponents _gravityGunComponents;
        private InputManager _inputManager;
        private InputHandler _inputHandler;
        private RayCaster _rayCaster;
        private ItemRotator _itemRotator;
        private IInputMap _inputMap;
        private CameraConfigType _cameraConfigType;

        private void Awake()
        {
            _isMobile = Application.isMobilePlatform;
            _rayCaster = new(_camera);

            _inputMap = _isMobile ?
                new TouchInput(_movementJoystick, _rotationJoystick, _kickButton, _hookButton, _killButton, _jumpButton)
                :
                new KeyboardInput(KeyCode.F, KeyCode.R, KeyCode.Q, KeyCode.Space);

            _inputManager = new(_inputMap);
            _cameraConfigType = _isMobile ? CameraConfigType.Mobile : CameraConfigType.PC;
            _mover.Construct(_inputMap as ICameraInput, _cameraConfigType);
            _scoreRepository = new();

            UpdateSkin(_skinSelector.ActiveSkin);

            

            _itemRotator = new();
            InitializeGravityGun();
            InitializeHUD();

            _scoreRepository.Construct();
            _skinSelector.OnSkinChanged += UpdateSkin;

            _sandboxHUD.ItemsSpawner.ItemPrepared += _itemRotator.SetRotatable;
            _sandboxHUD.ItemsSpawner.Spawned += _itemRotator.FreeRotatable;
            _sandboxHUD.ItemsSpawner.ItemCanceled += _itemRotator.FreeRotatable;
            _inputHandler = new(_inputMap, _rayCaster, _sandboxHUD.ItemsSpawner, _layerMask);
        }

        private void UpdateSkin(Skin obj)
        {
            Skin activeSkin = _skinSelector.ActiveSkin;
            activeSkin.MobileUserControlMelee.Construct(_inputMap);
            activeSkin.CameraCharacterController.Construct(_inputMap);
            activeSkin.Damage.Construct(_scoreRepository);
            _damage = activeSkin.Damage;
        }

        private void OnDestroy()
        {
            _sandboxHUD.Dispose();
            _gravityGunHandler.Dispose();
            _mover.Dispose();

            _sandboxHUD.ItemsSpawner.ItemPrepared -= _itemRotator.SetRotatable;
            _sandboxHUD.ItemsSpawner.ItemCanceled -= _itemRotator.FreeRotatable;
            _sandboxHUD.ItemsSpawner.Spawned -= _itemRotator.FreeRotatable;
            _skinSelector.OnSkinChanged -= UpdateSkin;
        }

        private void Update()
        {
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
                ItemRotator = _itemRotator ?? throw new ArgumentNullException(nameof(_itemRotator)),
                SkinSelector = _skinSelector ?? throw new ArgumentNullException(nameof(_skinSelector)),
                Score = _scoreRepository ?? throw new ArgumentNullException(nameof(_scoreRepository)),
                Damage = _damage ?? throw new ArgumentNullException(nameof(_damage))
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