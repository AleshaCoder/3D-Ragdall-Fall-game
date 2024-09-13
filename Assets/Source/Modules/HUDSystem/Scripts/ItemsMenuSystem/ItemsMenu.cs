using System;
using UnityEngine;
using Assets.Source.Extensions.Scripts;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class ItemsMenu
    {
        [SerializeField] private CanvasGroup _contentCanvas;
        [Header("Objects menu")]
        [SerializeField] private CharactersMenu _charactersMenu;
        [SerializeField] private MeleeWeaponsMenu _meleeWeaponsMenu;
        [SerializeField] private FireWeaponMenu _fireWeaponsMenu;
        [SerializeField] private ExplosionMenu _explosionMenu;
        [SerializeField] private WehiclesMenu _wehiclesMenu;
        [SerializeField] private AdaptationsMenu _adaptationsMenu;
        [SerializeField] private WeatherMenu _weatherMenu;
        [SerializeField] private DecorationMenu _decorationMenu;
        [Header("Scroll options")]
        [SerializeField] private ScrollNavigation _scrollNavigation;
        [SerializeField] private ListsOfObjects _listsOfObjects;

        private bool _opened = false;
        private ItemMenu _lastOpenedMenu;
        private ItemsSelector _itemsSelector;

        public void Construct(Joystick joystick, ItemsSelector itemsSelector)
        {
            _itemsSelector = itemsSelector ?? throw new ArgumentNullException(nameof(itemsSelector));

            _charactersMenu.ItemMenuButton.OnClick += OnCharactersClicked;
            _meleeWeaponsMenu.ItemMenuButton.OnClick += OnMeleeWeaponsClicked;
            _fireWeaponsMenu.ItemMenuButton.OnClick += OnFireWeaponsClicked;
            _explosionMenu.ItemMenuButton.OnClick += OnExplosionClicked;
            _wehiclesMenu.ItemMenuButton.OnClick += OnWehiclesClicked;
            _adaptationsMenu.ItemMenuButton.OnClick += OnAdaptationsClicked;
            _weatherMenu.ItemMenuButton.OnClick += OnWeatherClicked;
            _decorationMenu.ItemMenuButton.OnClick += OnDecorationClicked;

            _scrollNavigation.Construct();
            _charactersMenu.Construct(_contentCanvas, _scrollNavigation, joystick);
            _meleeWeaponsMenu.Construct(_scrollNavigation);
            _fireWeaponsMenu.Construct(_scrollNavigation);
            _explosionMenu.Construct(_scrollNavigation);
            _wehiclesMenu.Construct(_scrollNavigation);
            _adaptationsMenu.Construct(_scrollNavigation);
            _weatherMenu.Construct(_scrollNavigation);
            _decorationMenu.Construct(_scrollNavigation);
        }

        public void Dispose()
        {
            _charactersMenu.ItemMenuButton.OnClick -= OnCharactersClicked;
            _meleeWeaponsMenu.ItemMenuButton.OnClick -= OnMeleeWeaponsClicked;
            _fireWeaponsMenu.ItemMenuButton.OnClick -= OnFireWeaponsClicked;
            _explosionMenu.ItemMenuButton.OnClick -= OnExplosionClicked;
            _wehiclesMenu.ItemMenuButton.OnClick -= OnWehiclesClicked;
            _adaptationsMenu.ItemMenuButton.OnClick -= OnAdaptationsClicked;
            _weatherMenu.ItemMenuButton.OnClick -= OnWeatherClicked;
            _decorationMenu.ItemMenuButton.OnClick -= OnDecorationClicked;

            _scrollNavigation.Dispose();
            _charactersMenu.Dispose();
            _meleeWeaponsMenu.Dispose();
            _fireWeaponsMenu.Dispose();
            _explosionMenu.Dispose();
            _wehiclesMenu.Dispose();
            _adaptationsMenu.Dispose();
            _weatherMenu.Dispose();
            _decorationMenu.Dispose();
        }

        private void OnCharactersClicked() => Open(_charactersMenu);
        private void OnMeleeWeaponsClicked() => Open(_meleeWeaponsMenu);
        private void OnFireWeaponsClicked() => Open(_fireWeaponsMenu);
        private void OnExplosionClicked() => Open(_explosionMenu);
        private void OnWehiclesClicked() => Open(_wehiclesMenu);
        private void OnAdaptationsClicked() => Open(_adaptationsMenu);
        private void OnWeatherClicked() => Open(_weatherMenu);
        private void OnDecorationClicked() => Open(_decorationMenu);

        private void Open(ItemMenu itemMenu)
        {
            if (TryCloseLastMenu(itemMenu))
            {
                _contentCanvas.DisableGroup();
                return;
            }

            if(_opened == false)
            {
                _contentCanvas.EnableGroup();
                _opened = true;
            }

            _lastOpenedMenu = itemMenu;
            itemMenu.Open();
        }

        private void Open(CharactersMenu charactersMenu)
        {
            if(charactersMenu.SubclassIsNull)
            {
                _contentCanvas.DisableGroup();
                _opened = false;
            }
            else
            {
                _contentCanvas.EnableGroup();
                _opened = true;
            }

            if (TryCloseLastMenu(charactersMenu))
            {
                _contentCanvas.DisableGroup();
                return;
            }

            _lastOpenedMenu = charactersMenu;
            charactersMenu.Open();
        }

        private bool TryCloseLastMenu(ItemMenu itemMenu)
        {
            _itemsSelector.UnselectPresenter();

            if (_lastOpenedMenu == itemMenu)
            {
                _lastOpenedMenu.Close();
                _lastOpenedMenu = null;
                _opened = false;
                return true;
            }

            if (_lastOpenedMenu != null)
                _lastOpenedMenu.Close();

            return false;
        }

#if UNITY_EDITOR
        #region only editor
        public void SetUpObjectsLists()
        {
            _listsOfObjects.GetLists();
            _listsOfObjects.SetSettingsToLists();

        }
        #endregion
#endif
    }
}
