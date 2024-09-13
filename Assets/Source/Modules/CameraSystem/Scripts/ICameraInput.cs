using System;
using UnityEngine;

namespace Assets.Source.CameraSystem.Scripts
{
    public interface ICameraInput
    {
        public event Action<Vector2> Moving;
        public event Action<Vector2> AlternativePointerMoving;
        public event Action<Vector3> AlternativePointerUp;
    }
}