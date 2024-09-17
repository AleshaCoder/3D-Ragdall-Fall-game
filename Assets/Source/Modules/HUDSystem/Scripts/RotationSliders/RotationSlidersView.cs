using Assets.Source.InputService.Scripts;
using RotationSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts
{
    [Serializable]
    public class RotationSlidersView
    {
        [SerializeField] private Slider _xRotation;
        [SerializeField] private Slider _yRotation;
        [SerializeField] private Slider _zRotation;

        private ISpawnerInput _spawnerInput;

        public float XValue => _xRotation.value;
        public float YValue => _yRotation.value;
        public float ZValue => _zRotation.value;

        public event Action<Vector3> OnSlidersChanged;

        public void Construct(ISpawnerInput spawnerInput, IInputMap inputMap)
        {
            _xRotation.onValueChanged.AddListener(OnXChanged);
            _yRotation.onValueChanged.AddListener(OnYChanged);
            _zRotation.onValueChanged.AddListener(OnZChanged);
            inputMap.PointerUp += ResetSlidersValues;


            _spawnerInput = spawnerInput;
            _spawnerInput.ItemPrepared += ShowFor;
            _spawnerInput.Spawned += Hide;
            _spawnerInput.ItemCanceled += Hide;
        }

        private void ResetSlidersValues(Vector3 obj)
        {
            _xRotation.value = 0;
            _yRotation.value = 0;
            _zRotation.value = 0;
            OnSlidersChanged?.Invoke(Vector3.zero);
        }

        public void Dispose()
        {
            _xRotation.onValueChanged.RemoveListener(OnXChanged);
            _yRotation.onValueChanged.RemoveListener(OnYChanged);
            _zRotation.onValueChanged.RemoveListener(OnZChanged);
            _spawnerInput.ItemPrepared -= ShowFor;
            _spawnerInput.Spawned -= Hide;
            _spawnerInput.ItemCanceled -= Hide;
        }

        private void OnZChanged(float z)
        {
            OnSlidersChanged?.Invoke(new(0, 0, z));
        }

        private void OnYChanged(float y)
        {
            OnSlidersChanged?.Invoke(new(0, y, 0));
        }

        private void OnXChanged(float x)
        {
            OnSlidersChanged?.Invoke(new(x, 0, 0));
        }

        public void ShowFor(IRotatable rotatable)
        {
            _xRotation.gameObject.SetActive(rotatable.CanRotateX);
            _yRotation.gameObject.SetActive(rotatable.CanRotateY);
            _zRotation.gameObject.SetActive(rotatable.CanRotateZ);
        }

        public void Hide()
        {
            _xRotation.gameObject.SetActive(false);
            _yRotation.gameObject.SetActive(false);
            _zRotation.gameObject.SetActive(false);
        }
    }
}