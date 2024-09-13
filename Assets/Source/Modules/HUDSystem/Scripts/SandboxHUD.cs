using UnityEngine;
using Assets.Source.Extensions.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.ItemSpawnerSystem.Scripts;
using Assets.Source.HUDSystem.Scripts.ItemsMenuSystem;
using Assets.Source.HUDSystem.Scripts.HideInterfaceSystem;
using UnityEngine.UI;

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

        [SerializeField] private GameObject _itemsContainer;

        [Header("ResetButton")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _exitFromEditorButton;

        private ItemsSelector _itemsSelector;

        public ISpawnerInput ItemsSpawner => _itemsSpawner;

        public void Construct(SandboxHUDComponents sandboxHUDComponents)
        {
            _hideInterface.Construct();
            _itemsSpawner.Construct(sandboxHUDComponents.Input, sandboxHUDComponents.RayCaster, sandboxHUDComponents.IsMobile);
            _itemsSelector = new(sandboxHUDComponents.Input, sandboxHUDComponents.RayCaster, _itemsSpawner, _itemsContainer);
            _itemsMenu.Construct(sandboxHUDComponents.Joystick, _itemsSelector);

            _resetButton.onClick.AddListener(sandboxHUDComponents.CharacterResetter.ResetCharacter);
            _inventoryButton.onClick.AddListener(_itemsSpawner.CancelItemCreation);
            _exitFromEditorButton.onClick.AddListener(_itemsSpawner.CancelItemCreation);
        }

        public void Dispose()
        {
            _hideInterface.Dispose();
            _itemsMenu.Dispose();
            _itemsSpawner.Dispose();
            _itemsSelector.Dispose();
            _resetButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveListener(_itemsSpawner.CancelItemCreation);
            _exitFromEditorButton.onClick.RemoveListener(_itemsSpawner.CancelItemCreation);
        }

#if UNITY_EDITOR
        #region only editor
        public void SetUpObjectsScrolls() => _itemsMenu.SetUpObjectsLists();
        #endregion
#endif
    }
}
