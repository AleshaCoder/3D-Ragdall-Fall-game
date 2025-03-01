using Assets.Source.CameraSystem.Scripts;
using Assets.Source.Entities.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using UnityEngine;

namespace Assets.Source.InputService.Scripts
{
    public class InputHandler
    {
        private ISpawnerInput _spawner;
        private Item _dragItem;
        private IInputMap _inputMap;
        private IRayCaster _raycaster;
        private LayerMask _layerMask;

        public InputHandler(IInputMap inputMap, IRayCaster rayCaster, ISpawnerInput spawner, LayerMask layerMask)
        {
            _layerMask = layerMask;
            _raycaster = rayCaster;
            _inputMap = inputMap;
            _spawner = spawner;

            _inputMap.LongClicked += OnLongClicked;
            _inputMap.DoubleClicked += OnDoubleClick;
            _inputMap.PointerDown += OnClick;
            _inputMap.PointerUp += OnEndMove;
            _inputMap.PointerMoving += OnPress;
            _spawner.ItemSpawned += OnItemSpawned;
        }

        internal void Dispose()
        {
            _inputMap.LongClicked -= OnLongClicked;
            _inputMap.DoubleClicked -= OnDoubleClick;
            _inputMap.PointerDown -= OnClick;
            _inputMap.PointerUp -= OnEndMove;
            _inputMap.PointerMoving -= OnPress;
            _spawner.ItemSpawned -= OnItemSpawned;
        }

        private void OnPress(Vector3 position)
        {
        }

        private void OnEndMove(Vector3 position)
        {
        }

        private void OnItemSpawned(Item item)
        {
            //if (item.Center != null)
            //{
            //    _selectionCirclce.gameObject.SetActive(true);
            //    _selectionCirclce.Set(item);
            //}
        }

        private void OnClick(Vector3 position)
        {
            if (_raycaster.TryGetAny(Input.mousePosition, out RotateButton rotateButton))
                rotateButton.OnPressed();

            if (_raycaster.IsNotUIObject(position) == false && _spawner.IsActive)
            {
                MissClick();
                return;
            }

            if (_spawner.IsActive)
                return;

            MissClick();
        }

        private void OnLongClicked(Vector3 position)
        {
            if (_raycaster.IsNotUIObject(position) == false)
            {
                MissClick();
                return;
            }

            if (TryGetItem(out Item item))
            {
                _dragItem = item;
                _dragItem.StartDrag();

                //if (item.Center != null)
                //{
                //    _selectionCirclce.gameObject.SetActive(true);
                //    _selectionCirclce.Set(item);
                //}

                return;
            }

            MissClick();
        }

        private void OnDoubleClick(Vector3 position)
        {
            if (_raycaster.IsNotUIObject(position) == false)
            {
                MissClick();
                return;
            }

            if (TryGetItem(out Item item))
            {
                return;
            }

            MissClick();
        }

        private bool TryGetItem(out Item receivedItem)
        {
            if (_raycaster.TryGetHit(Vector3.zero, out RaycastHit hit, _layerMask))
            {
                if (hit.collider.TryGetComponent(out SpawnChecker checker))
                {
                    receivedItem = checker.GetComponentInParent<Item>();
                    return true;
                }
            }

            receivedItem = null;
            return false;
        }

        private void MissClick()
        {
        }
    }
}