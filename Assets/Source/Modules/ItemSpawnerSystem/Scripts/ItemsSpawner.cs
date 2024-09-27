using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Assets.Source.Structs.Scripts;
using Assets.Source.Entities.Scripts;
using Assets.Source.InputService.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using static ScreenExtensions;
using SkinsSystem;
using TimeSystem;
using Analytics;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    [Serializable]
    public class ItemsSpawner : IItemsSpawner, ISpawnerInput
    {
        [SerializeField] private CreatedScriptableObjects _createdScriptableObjects;
        [SerializeField] private GameObject _gizmo;
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
        public event Action Spawned;
        public event Action ItemCanceled;
        public event Action<IItemCreatingView> ItemPrepared;

        public void Construct(IInputMap input, IRayCaster raycaster, GameObject gizmo, bool isMobile = false)
        {
            _isMobile = isMobile;
            _raycaster = raycaster;
            _input = input;
            _gizmo = gizmo;
            _spawnQueue = new();
            _removeItemButton.onClick.AddListener(CancelItemCreation);
            _spawnButton.onClick.AddListener(OnTryingSpawn);
        }

        public void Dispose()
        {
            _removeItemButton.onClick.RemoveListener(CancelItemCreation);
            _spawnButton.onClick.RemoveListener(OnTryingSpawn);
        }

        public void PrepareRecreateRagdoll(Unit unit)
        {
            if (_createdScriptableObjects.TryGetCharacter(unit.Type, out Item item))
            {
                var rotation = unit.transform.rotation;
                Object.Destroy(unit.gameObject);
                CreateItem(item, rotation);
                AnalyticsSender.SelectItem(unit.Type.ToString());
            }
        }

        public void PrepareRecreateCharacter(Skin character)
        {
            character.gameObject.SetActive(false);
            character.SetDefaultPositionAndRotation();
            CreateItem(character.GetComponentInChildren<MainCharacter>(), Quaternion.identity);
        }

        public void PrepareRecreateItem(Building building)
        {
            if (_createdScriptableObjects.TryGetItem(building.Type, out Item item))
            {
                Object.Destroy(building.gameObject);
                var rotation = building.transform.rotation;
                CreateItem(item, rotation);
                AnalyticsSender.SelectItem(building.Type.ToString());
            }
        }

        public void PrepareCreateRagdoll(RagdollType type)
        {
            if (_createdScriptableObjects.TryGetCharacter(type, out Item item))
            {
                CreateItem(item, Quaternion.identity);
                AnalyticsSender.SelectItem(type.ToString());
            }
        }

        public void PrepareCreateItem(ItemsType type)
        {
            if (_createdScriptableObjects.TryGetItem(type, out Item item))
            {
                AnalyticsSender.SelectItem(type.ToString());
                CreateItem(item, Quaternion.identity);
            }
        }

        public void CancelItemCreation()
        {
            CreateItem(null, Quaternion.identity);
            ItemCanceled?.Invoke();
            TimeService.Scale = 1;
        }

        private void UnshowModel(bool obj) => _canShow = false;
        private void ShowModel(float arg1, float arg2) => _canShow = true;

        public void Tick()
        {
            if (!_canShow)
                return;

            if (_itemView == null)
                return;

            GetPosition();
            _gizmo.transform.position = _position;
            _itemView.SetPosition(_position, _normal);
            _itemView.SetAngle(CheckAngle());

            if (_item is IDontDestroyableFromScene)
            {
                _item.transform.SetPositionAndRotation(_position, _itemView.Rotation);
            }
        }

        private void CreateItem(Item item, Quaternion rotation)
        {
            if (_itemView != null)
            {
                _itemView.Dispose(true);
                _itemView = null;
            }

            _gizmo.SetActive(false);
            _item = item;
            IsActive = item != null;
            _canShow = item != null;

            if (item == null)
                return;

            var itemView = Instantiate(_position, item.CreatingModel, rotation);
            _itemView = itemView;

            _removeItemButton.gameObject.SetActive(item is not IDontDestroyableFromScene);

            _itemView.Init();
            _spawnAngle = _itemView.SpawnAngle;
            _gizmo.SetActive(true);

            _gizmo.transform.eulerAngles = new(-180, 0, 0);

            ItemPrepared?.Invoke(_itemView);
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
            TimeService.Scale = 1;
            var item = _item;
            if (_item is not IDontDestroyableFromScene)
            {
                item = InstantiateItem(_position, _item, _itemView.Rotation);
                item.Init(_spawnQueue.AddItemWithIndex(item));
            }

            _item.transform.root.gameObject.SetActive(true);

            if (_item.transform.root.TryGetComponent<Skin>(out Skin skin))
            {
                skin.SetDefaultPositionAndRotation();
            }

            _gizmo.SetActive(false);
            _gizmo.transform.parent = null;
            ItemSpawned?.Invoke(item);
            AnalyticsSender.SpawnItem(item.name);
            Spawned?.Invoke();
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
