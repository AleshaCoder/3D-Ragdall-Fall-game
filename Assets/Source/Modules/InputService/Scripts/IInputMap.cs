using System;
using UnityEngine;

namespace Assets.Source.InputService.Scripts
{
    public interface IInputMap
    {
        public event Action<Vector2> Moving;
        public event Action<Vector2> AlternativePointerMoving;
        public event Action<Vector3> AlternativePointerUp;
        public event Action<Vector3> PointerMoving;
        public event Action<Vector3> PointerDown;
        public event Action<Vector3> PointerUp;
        public event Action<Vector3> DoubleClicked;
        public event Action<Vector3> LongClicked;
        event Action<int> Attacking;
        event Action Jumping;
        event Action Killing;

        public void Tick(float deltaTime);
    }
}