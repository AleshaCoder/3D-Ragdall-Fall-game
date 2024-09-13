using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Scripts
{
    public class ItemCreatingView : MonoBehaviour, IItemCreatingView
    {
        [SerializeField] private Renderer[] _meshRenderers;
        [SerializeField] private float _spawnAngle;

        private bool _isAcceptableAngle = true;
        private List<SpawnChecker> _colliders;

        public bool CanSpawn => _colliders.Count <= 0 && _isAcceptableAngle;
        public float SpawnAngle => _spawnAngle;
        public Quaternion Rotation => transform.rotation;

        public void Init()
        {
            _colliders = new();
            SetView();
        }

        public void SetPosition(Vector3 position, Vector3 normal)
        {
            transform.position = position;
            transform.up = normal;
        }

        public void SetAngle(bool isAcceptableAngle)
        {
            _isAcceptableAngle = isAcceptableAngle;
            SetView();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SpawnChecker checker))
            {
                _colliders.Add(checker);
                SetView();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SpawnChecker checker))
            {
                _colliders.Remove(checker);
                SetView();
            }
        }

        internal void SetView()
        {
            if (CanSpawn)
            {
                foreach (var mesh in _meshRenderers)
                    mesh.material.color = Color.green;
            }
            else
            {
                foreach (var mesh in _meshRenderers)
                    mesh.material.color = Color.red;
            }
        }
    }
}