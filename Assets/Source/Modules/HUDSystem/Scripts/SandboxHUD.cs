using UnityEngine;
using Assets.Source.Extensions.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.ItemSpawnerSystem.Scripts;
using Assets.Source.HUDSystem.Scripts.ItemsMenuSystem;
using Assets.Source.HUDSystem.Scripts.HideInterfaceSystem;
using UnityEngine.UI;
using Assets.Source.Entities.Scripts;
using SkinsSystem;
using DamageSystem;

namespace Assets.Source.HUDSystem.Scripts
{
    public class SandboxHUD : MonoBehaviour, ICoroutine
    {
        [Header("Hide interface system")]
        [SerializeField] private HideInterface _hideInterface;
        [Header("Main menus")]
        [SerializeField] private ItemsMenu _itemsMenu;
        [Header("Item spawner")]
        [SerializeField] private ItemsSpawner _itemsSpawner;

        [SerializeField] private RotationMenu _rotationMenu;

        [SerializeField] private GameObject _itemsContainer;
        [SerializeField] private GameObject _skinsContainer;
        [SerializeField] private GameObject _placeInterface;
        [SerializeField] private GameObject _selecteInterface;

        [Header("ResetButton")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _enterInEditorButton;
        [SerializeField] private Button _exitFromEditorButton;
        [SerializeField] private Button _timeScaleButton;
        [SerializeField] private Slider _timeScaleSlider;

        [SerializeField] private TMPro.TMP_Text _scoreText;
        [SerializeField] private TMPro.TMP_Text _highScoreText;
        [SerializeField] private FloatingTextDisplayerPool _damageDisplayer;
        [SerializeField] private GameObject _gizmo;

        private ItemsSelector _itemsSelector;
        private SkinSelector _skinSelector;
        private IScore _score;
        private IDamageMessage _damageMessage;

        public ISpawnerInput ItemsSpawner => _itemsSpawner;

        public void Construct(SandboxHUDComponents sandboxHUDComponents)
        {
            _skinSelector = sandboxHUDComponents.SkinSelector;
            _hideInterface.Construct();
            _itemsSpawner.Construct(sandboxHUDComponents.Input, sandboxHUDComponents.RayCaster, _gizmo, sandboxHUDComponents.IsMobile);
            _itemsSelector = new(sandboxHUDComponents.Input, sandboxHUDComponents.RayCaster, _itemsSpawner, _itemsContainer, _selectButton, _skinSelector, _skinsContainer);
            _itemsMenu.Construct(sandboxHUDComponents.Joystick, _itemsSelector);
            _rotationMenu.Construct(sandboxHUDComponents.ItemRotator, ItemsSpawner, sandboxHUDComponents.Input, _gizmo.transform);
            UpdateReset(_skinSelector.ActiveSkin);
            _score = sandboxHUDComponents.Score;
            _damageMessage = sandboxHUDComponents.Damage;

            _score.OnScoreChanged += OnScoreChanged;
            _score.OnHighScoreChanged += OnHighScoreChanged;
            _damageMessage.OnDamageMessage += _damageDisplayer.DisplayText;
            _skinSelector.OnSkinChanged += UpdateReset;
            _inventoryButton.onClick.AddListener(OnInventoryButtonClick);
            _exitFromEditorButton.onClick.AddListener(OnExitEditor);
            _enterInEditorButton.onClick.AddListener(OnEnterEditor);
            _timeScaleButton.onClick.AddListener(OnTimeScaleButtonClick);
            ItemsSpawner.ItemPrepared += OnItemPrepared;
            ItemsSpawner.Spawned += OnItemSpawned;
            ItemsSpawner.ItemCanceled += OnItemSpawned;

        }

        private void OnScoreChanged(int score) => _scoreText.text = score.ToString();
        private void OnHighScoreChanged(int score) => _highScoreText.text = score.ToString();

        private void OnExitEditor()
        {
            _itemsSpawner.CancelItemCreation();
            _skinSelector.ActiveSkin.CameraCharacterController.gameObject.SetActive(true);
            _skinSelector.ActiveSkin.CharacterMeleeDemo.enabled = true;
            _skinSelector.ActiveSkin.CharacterMeleeDemo.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void OnEnterEditor()
        {
            _skinSelector.ActiveSkin.CameraCharacterController.gameObject.SetActive(false);
            _skinSelector.ActiveSkin.CharacterMeleeDemo.enabled = false;
            _skinSelector.ActiveSkin.CharacterMeleeDemo.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void UpdateReset(Skin obj)
        {
            _damageMessage = obj.Damage;
            _damageMessage.OnDamageMessage += _damageDisplayer.DisplayText;

            _resetButton.onClick.RemoveAllListeners();
            _resetButton.onClick.AddListener(OnReset);
        }

        private void OnReset()
        {
            _skinSelector.ChangeSkin(_skinSelector.ActiveSkin.RagdollType);
        }

        private void OnInventoryButtonClick()
        {
           _itemsSpawner.CancelItemCreation();
        }

        private void OnTimeScaleButtonClick()
        {
            _timeScaleSlider.gameObject.SetActive(!_timeScaleSlider.gameObject.activeInHierarchy);
        }

        private void OnItemSpawned()
        {
            _selecteInterface.SetActive(true);
            _placeInterface.SetActive(false);
        }

        private void OnItemPrepared(IItemCreatingView obj)
        {
            _placeInterface.SetActive(true);
            _selecteInterface.SetActive(false);
        }

        public void Dispose()
        {
            _hideInterface.Dispose();
            _itemsMenu.Dispose();
            _itemsSpawner.Dispose();
            _itemsSelector.Dispose();
            _rotationMenu.Dispose();
            _resetButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveListener(OnInventoryButtonClick);
            _exitFromEditorButton.onClick.RemoveListener(OnExitEditor);
            _enterInEditorButton.onClick.AddListener(OnEnterEditor);
            _timeScaleButton.onClick.RemoveListener(OnTimeScaleButtonClick);
            ItemsSpawner.ItemPrepared -= OnItemPrepared;
            ItemsSpawner.Spawned -= OnItemSpawned;
            ItemsSpawner.ItemCanceled -= OnItemSpawned;
            _damageMessage.OnDamageMessage -= _damageDisplayer.DisplayText;
            _skinSelector.OnSkinChanged -= UpdateReset;
            _score.OnScoreChanged -= OnScoreChanged;
            _score.OnHighScoreChanged -= OnHighScoreChanged;
        }

#if UNITY_EDITOR
        #region only editor
        public void SetUpObjectsScrolls() => _itemsMenu.SetUpObjectsLists();
        #endregion
#endif
    }
}
