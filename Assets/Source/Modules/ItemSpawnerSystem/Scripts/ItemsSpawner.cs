using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Assets.Source.Structs.Scripts;
using Assets.Source.Entities.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using static ScreenExtensions;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    [Serializable]
    public class ItemsSpawner : IItemsSpawner, ISpawnerInput
    {
        [SerializeField] private CreatedScriptableObjects _createdScriptableObjects;
        [SerializeField] private Button _removeItemButton;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Slider _distanceFromItemViewToCamera;
        [SerializeField] private LayerMask _spawnLayerMask;
        [SerializeField] private LayerMask _worldUI;

        private Item _item;
        private SpawnQueue _spawnQueue;
        private IRayCaster _raycaster;
        private Vector3 _position;
        private Vector3 _normal;
        private IItemCreatingView _itemView;
        private IInputMap _input;
        private float _spawnAngle;
        private bool _isMobile;
        private bool _canShow;

        public bool IsActive { get; private set; }

        public event Action<Item> ItemSpawned;

        public void Construct(IInputMap input, IRayCaster raycaster, bool isMobile = false)
        {
            _isMobile = isMobile;
            _raycaster = raycaster;
            _input = input;
            _spawnQueue = new();
            _removeItemButton.onClick.AddListener(CancelItemCreation);
            _spawnButton.onClick.AddListener(OnTryingSpawn);
        }

        public void Dispose()
        {
            _removeItemButton.onClick.RemoveListener(CancelItemCreation);
            _spawnButton.onClick.RemoveListener(OnTryingSpawn);
        }

        public void PrepareRecreateRagdoll(RagdollType type)
        {
            if (_createdScriptableObjects.TryGetCharacter(type, out Item item))
                CreateItem(item);
        }

        public void PrepareRecreateWeapon(WeaponType type)
        {
            if (_createdScriptableObjects.TryGetWeapon(type, out Item item))
                CreateItem(item);
        }

        public void PrepareRecreateItem(Building building)
        {
            if (_createdScriptableObjects.TryGetItem(building.Type, out Item item))
                CreateItem(item);
        }

        public void PrepareCreateRagdoll(RagdollType type)
        {
            if (_createdScriptableObjects.TryGetCharacter(type, out Item item))
                CreateItem(item);
        }

        public void PrepareCreateWeapon(WeaponType type)
        {
            if (_createdScriptableObjects.TryGetWeapon(type, out Item item))
                CreateItem(item);
        }

        public void PrepareCreateItem(ItemsType type)
        {
            if (_createdScriptableObjects.TryGetItem(type, out Item item))
            CreateItem(item);
        }

        public void CancelItemCreation() => CreateItem(null);

        private void UnshowModel(bool obj) => _canShow = false;
        private void ShowModel(float arg1, float arg2) => _canShow = true;

        public void Tick()
        {
            if (!_canShow) 
                return;

            if (_itemView == null) 
                return;

            GetPosition();

            _itemView.SetPosition(_position, _normal);
            _itemView.SetAngle(CheckAngle());
        }

        private void CreateItem(Item item)
        {
            if (_itemView != null)
            {
                _itemView.Dispose();
                _itemView = null;
            }

            _item = item;
            IsActive = item != null;
            _canShow = item != null;

            if (item == null)
                return;

            _itemView = Instantiate(_position, item.CreatingModel, Quaternion.identity);
            _itemView.Init();
            _spawnAngle = _itemView.SpawnAngle;
        }

        private void OnTryingSpawn()
        {
            if (_itemView == null)
                return;

            if (_raycaster.IsNotUIObject(ScreenCenter) == false)
                return;

            if (_itemView.CanSpawn)
            {
                SpawnItem();
                CancelItemCreation();
            }
        }

        private void SpawnItem()
        {
            var item = InstantiateItem(_position, _item, _itemView.Rotation);
            item.Init(_spawnQueue.AddItemWithIndex(item));
            ItemSpawned?.Invoke(item);
        }

        private void GetPosition()
        {
            (_position, _normal) = _raycaster.GetPositionAndNormal(ScreenCenter, _spawnLayerMask, _distanceFromItemViewToCamera.value);
        }

        private bool CheckAngle()
        {
            float angle = Vector3.SignedAngle(_normal, Vector3.up, -Vector3.forward);
            angle = Mathf.Abs(angle);

            return angle < _spawnAngle;
        }

        private T Instantiate<T>(Vector3 position, T original, Quaternion quaternion) where T : ItemCreatingView
            => Object.Instantiate(original, position, quaternion);
        private T InstantiateItem<T>(Vector3 position, T original, Quaternion quaternion) where T : Item
            => Object.Instantiate(original, position, quaternion);
    }
}
