using System;
using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    [Serializable]
    public class GravityGunHandler
    {
        [SerializeField] private float _range;
        [SerializeField] private float _dragForce;
        [SerializeField] private float _throwForce;
        [SerializeField] private float _distanceFromCamera;

        private IGravityGunInput _input;
        private GravityGunState _currentState;
        private GravityGunComponents _components;

        internal float Range => _range;
        internal IGrabbable CurrentGrabbedObject { get; private set; }
        internal GravityGun GraviGun { get; private set; }
        internal Camera Camera => _components.Camera;
        internal Vector3 DragPoint => Camera.transform.position + Camera.transform.forward * _distanceFromCamera;
        internal Vector3 CameraDirection => Camera.transform.forward;

        internal GravityGunState Idle => _components.Idle;
        internal GravityGunState Drag => _components.Drag;
        internal GravityGunState Throw => _components.Throw;

        public void Construct(GravityGunComponents components)
        {
            _components = components;
            _input = components.Input;
            GraviGun = new(_throwForce, _dragForce);

            _input.LongClicked += OnLongClick;
            _input.DoubleClicked += (_) => OnDoubleClick();

            SetState(Idle);
        }

        public void Dispose()
        {
            _input.LongClicked -= OnLongClick;
            _input.DoubleClicked -= (_) => OnDoubleClick();
        }

        public void Tick() => _currentState?.Tick();
        public void FixedTick() => _currentState?.FixedTick();

        internal void SetState(GravityGunState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        internal void SetObject(IGrabbable grabbedObject) => CurrentGrabbedObject = grabbedObject;
        private void OnLongClick(Vector3 position) => _currentState?.OnLongClick(position);
        private void OnDoubleClick() => _currentState?.OnDoubleClick();
    }
}