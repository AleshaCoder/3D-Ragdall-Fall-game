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

        public void Construct(ItemRotator itemRotator, ISpawnerInput spawnerInput, IInputMap inputMap)
        {
            _itemRotator = itemRotator ?? throw new ArgumentNullException(nameof(itemRotator));
            _rotationSlidersView.Construct(spawnerInput, inputMap);
            _rotationSlidersView.OnSlidersChanged += _itemRotator.Rotate;
        }

        public void Dispose()
        {
            _rotationSlidersView.OnSlidersChanged -= _itemRotator.Rotate;
        }
    }
}