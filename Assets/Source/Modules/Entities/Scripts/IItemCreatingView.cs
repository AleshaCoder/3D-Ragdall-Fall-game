using RotationSystem;
using UnityEngine;

namespace Assets.Source.Entities.Scripts
{
    public interface IItemCreatingView : IRotatable, IScalable
    {
        public Quaternion Rotation { get; }
        public bool CanSpawn { get; }
        public float SpawnAngle { get; }
        public void Init();
        public void SetAngle(bool isAcceptableAngle);
        public void SetPosition(Vector3 position, Vector3 normal);
        public void Dispose(bool withDestroy = true);
    }
}