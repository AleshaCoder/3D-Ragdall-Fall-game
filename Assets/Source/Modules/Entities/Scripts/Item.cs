using RootMotion.Dynamics;
using System;
using UnityEngine;

namespace Assets.Source.Entities.Scripts
{
    [SelectionBase]
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemCreatingView CreatingModel { get; private set; }
        [field: SerializeField] public Transform Center { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public PuppetMaster PuppetMaster { get; private set; }

        private bool _isRotating;
        private float _angle;
        private int _spawnedIndex;

        public event Action<int> Disposed;

        private void OnEnable()
        {
            _isRotating = false;
        }

        private void Update()
        {
            Rotate();
        }

        private void OnDisable()
        {
            Dispose();
        }

        public void Init(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            else
                _spawnedIndex = index;
        }

        public void StartDrag()
        {
            if (PuppetMaster != null) 
                PuppetMaster.pinWeight = 0f;
        }

        public void EndDrag()
        {
            if (PuppetMaster != null) 
                PuppetMaster.pinWeight = 1.0f;
        }

        public void Rotate(float delta)
        {
            if (delta == 0f)
            {
                _isRotating = false;
                _angle = 0f;
                return;
            }

            _isRotating = true;
            _angle = delta;
        }

        private void Dispose()
        {
            Disposed?.Invoke(_spawnedIndex);
            Destroy(gameObject);
        }

        private void Rotate()
        {
            if (!_isRotating) 
                return;

            transform.RotateAround(Center.position, Vector3.up, _angle * Time.deltaTime);
        }
    }
}