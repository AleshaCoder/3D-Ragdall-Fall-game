using Assets.Source.InputService.Scripts;
using RotationSystem;
using System;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts
{
    [Serializable]
    public class RotationMenu
    {
        [SerializeField] private RotationSlidersView _rotationSlidersView;
        private ItemRotator _itemRotator;
        private Transform _gizmo;
        private Vector3 _gizmoDefaultRotation;

        public void Construct(ItemRotator itemRotator, ISpawnerInput spawnerInput, IInputMap inputMap, Transform gizmo)
        {
            _itemRotator = itemRotator ?? throw new ArgumentNullException(nameof(itemRotator));
            _rotationSlidersView.Construct(spawnerInput, inputMap);
            _gizmo = gizmo;
            _gizmoDefaultRotation = _gizmo.eulerAngles;
            _rotationSlidersView.OnSlidersChanged += _itemRotator.Rotate;
            _rotationSlidersView.OnSlidersChanged += OnSlidersChanged;
        }

        private void OnSlidersChanged(Vector3 vector3)
        {
            if (vector3 == Vector3.zero)
                _gizmoDefaultRotation = _gizmo.eulerAngles;
            _gizmo.eulerAngles = _gizmoDefaultRotation + new Vector3(vector3.x, vector3.y, -vector3.z);
        }

        public void Dispose()
        {
            _rotationSlidersView.OnSlidersChanged -= _itemRotator.Rotate;
            _rotationSlidersView.OnSlidersChanged -= OnSlidersChanged;
        }
    }
}