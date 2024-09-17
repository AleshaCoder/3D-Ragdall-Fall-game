
using UnityEngine;

namespace RotationSystem
{
    public interface IScalable
    {
        bool CanScale { get; }

        Vector3 OriginScale { get; }

        void Scale(float x);

        void UpdateOriginScale();
    }
}