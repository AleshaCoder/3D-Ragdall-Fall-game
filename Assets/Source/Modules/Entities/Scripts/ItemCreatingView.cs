using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Scripts
{
    public class ItemCreatingView : MonoBehaviour, IItemCreatingView
    {
        [SerializeField] private Renderer[] _meshRenderers;
        [SerializeField] private float _spawnAngle;
        [field: SerializeField] public bool CanRotateX { get; private set; } = false;
        [field: SerializeField] public bool CanRotateY { get; private set; } = true;
        [field: SerializeField] public bool CanRotateZ { get; private set; } = false;
        [field: SerializeField] public bool CanScale { get; private set; } = true;
        [HideInInspector] public Vector3 OriginRotation { get; private set; }
        [HideInInspector] public Vector3 OriginScale { get; private set; }

        private bool _isAcceptableAngle = true;
        private List<SpawnChecker> _colliders;
        private Collider _collider;

        public bool CanSpawn => _colliders.Count <= 0 && _isAcceptableAngle;
        public float SpawnAngle => _spawnAngle;
        public Quaternion Rotation => transform.rotation;

        public void RotateX(float x)
        {
            transform.eulerAngles = OriginRotation + new Vector3(x, 0, 0);
        }

        public void RotateY(float y)
        {
            transform.eulerAngles = OriginRotation + new Vector3(0, y, 0);
        }

        public void RotateZ(float z)
        {
            transform.eulerAngles = OriginRotation + new Vector3(0, 0, z);
        }

        public void Scale(float z)
        {
            transform.localScale = OriginScale * z;
        }

        public void UpdateOriginRotation()
        {
            OriginRotation = transform.eulerAngles;
        }

        public void UpdateOriginScale()
        {
            OriginRotation = transform.localScale;
        }

        public void Init()
        {
            _colliders = new();
            SetView();
            UpdateOriginRotation();
            UpdateOriginScale();
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public void SetPosition(Vector3 position, Vector3 normal)
        {
            transform.position = position;
        }

        public void SetAngle(bool isAcceptableAngle)
        {
            _isAcceptableAngle = isAcceptableAngle;
            SetView();
        }

        public void Dispose(bool withDestroy = true)
        {
            foreach (var mesh in _meshRenderers)
                mesh.material.color = Color.white;

            _collider.isTrigger = false;

            if (withDestroy)
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
                    mesh.material.color = new Color(0, 1, 0, 0.5f);
            }
            else
            {
                foreach (var mesh in _meshRenderers)
                    mesh.material.color = new Color(1, 0, 0, 0.5f);
            }
        }
    }
}