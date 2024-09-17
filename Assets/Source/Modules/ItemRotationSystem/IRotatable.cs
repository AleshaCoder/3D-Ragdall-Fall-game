
using UnityEngine;

namespace RotationSystem
{
    public interface IRotatable
    {
        bool CanRotateX { get; }
        bool CanRotateY { get; }
        bool CanRotateZ { get; }

        Vector3 OriginRotation { get; }

        void RotateX(float x);
        void RotateY(float y);
        void RotateZ(float z);

        void UpdateOriginRotation();
    }
}