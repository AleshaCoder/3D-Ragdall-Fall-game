using System;
using UnityEngine;
using Assets.Source.InputService.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using Assets.Source.ItemSpawnerSystem.Scripts;
using Assets.Source.HUDSystem.Scripts.MenuButtons;
using Assets.Source.Structs.Scripts;
using Assets.Source.Entities.Scripts;

namespace Assets.Source.HUDSystem.Scripts
{
    public class ItemsSelector
    {
        private IInputMap _input;
        private IRayCaster _rayCaster;
        private IItemsSpawner _itemsSpawner;
        private IButton _selectedButton;
        private ISelectableButton _temporarySelectableButton;
        private GameObject _itemsContainer;
        private UnityEngine.UI.Button _selectButton;

        public ItemsSelector(IInputMap input, IRayCaster rayCaster, IItemsSpawner itemsSpawner, GameObject itemsContainer, UnityEngine.UI.Button selectButton)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _rayCaster = rayCaster ?? throw new ArgumentNullException(nameof(rayCaster));
            _itemsSpawner = itemsSpawner ?? throw new ArgumentNullException(nameof(itemsSpawner));
            _itemsContainer = itemsContainer ?? throw new ArgumentNullException(nameof(itemsContainer));
            _selectButton = selectButton ?? throw new ArgumentNullException(nameof(selectButton));
            _input.PointerUp += OnClickEnded;
            _selectButton.onClick.AddListener(OnSelectItemButtonClick);
        }

        private void OnSelectItemButtonClick()
        {
            _rayCaster.TryGetAny(ScreenExtensions.ScreenCenter, out MainCharacter character);

            if (character != null)
            {
                _itemsSpawner.PrepareRecreateCharacter(character);
                return;
            }


            _rayCaster.TryGetAny(ScreenExtensions.ScreenCenter, out Building item);

            if (item != null)
            {
                _itemsSpawner.PrepareRecreateItem(item);
                return;
            }

            _rayCaster.TryGetAny(ScreenExtensions.ScreenCenter, out BodyPart bodyPart);

            if (bodyPart != null)
            {
                bodyPart.transform.root.TryGetComponent(out Unit unit);

                if (unit != null)
                {
                    _itemsSpawner.PrepareRecreateRagdoll(unit);
                    return;
                }
            }
        }

        public void Dispose()
        {
            _input.PointerUp -= OnClickEnded;
            _selectButton.onClick.RemoveListener(OnSelectItemButtonClick);
        }

        public void UnselectPresenter()
        {
            _itemsSpawner.CancelItemCreation();
            _temporarySelectableButton?.Deselect();
            _temporarySelectableButton = null;
        }

        private void OnClickEnded(Vector3 clickPosition)
        {
            TryCastButton(clickPosition);
        }

        private void TryCastButton(Vector3 clickPosition)
        {
            _rayCaster.TryGetUI(clickPosition, out IButton[] itemButton);

            if (itemButton.Length == 0 || itemButton[0] is IButton == false)
            {
                _selectedButton = null;
                return;
            }

            _selectedButton = itemButton[0];

            if (_selectedButton is (IBlockedButton { IsBlocked: false } or RagdollButtonPresenter or ItemsButtonPresenter or WeaponButtonPresenter) and ISelectableButton selectableButton)
                TrySelectButton(selectableButton);
        }

        private void TrySelectButton(ISelectableButton selectableButton)
        {
            if (_temporarySelectableButton == null)
            {
                SelectPresenter(selectableButton);
                return;
            }

            if (_temporarySelectableButton != selectableButton)
            {
                UnselectPresenter();
                SelectPresenter(selectableButton);
            }
            else
            {
                UnselectPresenter();
            }
        }

        private void SelectPresenter(ISelectableButton selectableButton)
        {
            //_temporarySelectableButton = selectableButton;
            //_temporarySelectableButton.Select();
            DetermineSpawnObject(_selectedButton);
            _itemsContainer.SetActive(false);
        }

        private void DetermineSpawnObject(IButton button)
        {
            switch (button)
            {
                case IButtonPresenter<RagdollType> ragdoll:
                    _itemsSpawner.PrepareCreateRagdoll(ragdoll.Type);
                    break;

                case IButtonPresenter<ItemsType> item:
                    _itemsSpawner.PrepareCreateItem(item.Type);
                    break;
            }
        }
    }
}
