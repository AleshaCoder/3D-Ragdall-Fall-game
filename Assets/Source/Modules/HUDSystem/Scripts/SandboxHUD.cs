using UnityEngine;
using Assets.Source.Extensions.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.ItemSpawnerSystem.Scripts;
using Assets.Source.HUDSystem.Scripts.ItemsMenuSystem;
using Assets.Source.HUDSystem.Scripts.HideInterfaceSystem;
using UnityEngine.UI;
using RotationSystem;
using System;
using Assets.Source.Entities.Scripts;

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
        [SerializeField] private GameObject _placeInterface;
        [SerializeField] private GameObject _selecteInterface;

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
            _itemsSelector = new(sandboxHUDComponents.Input, sandboxHUDComponents.RayCaster, _itemsSpawner, _itemsContainer, _selectButton);
            _itemsMenu.Construct(sandboxHUDComponents.Joystick, _itemsSelector);
            _rotationMenu.Construct(sandboxHUDComponents.ItemRotator, ItemsSpawner, sandboxHUDComponents.Input);

            _resetButton.onClick.AddListener(sandboxHUDComponents.CharacterResetter.ResetCharacter);
            _inventoryButton.onClick.AddListener(_itemsSpawner.CancelItemCreation);
            _exitFromEditorButton.onClick.AddListener(_itemsSpawner.CancelItemCreation);
            ItemsSpawner.ItemPrepared += OnItemPrepared;
            ItemsSpawner.Spawned += OnItemSpawned;
            ItemsSpawner.ItemCanceled += OnItemSpawned;
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
            _inventoryButton.onClick.RemoveListener(_itemsSpawner.CancelItemCreation);
            _exitFromEditorButton.onClick.RemoveListener(_itemsSpawner.CancelItemCreation);
            ItemsSpawner.ItemPrepared -= OnItemPrepared;
            ItemsSpawner.Spawned -= OnItemSpawned;
            ItemsSpawner.ItemCanceled -= OnItemSpawned;
        }

#if UNITY_EDITOR
        #region only editor
        public void SetUpObjectsScrolls() => _itemsMenu.SetUpObjectsLists();
        #endregion
#endif
    }
}
