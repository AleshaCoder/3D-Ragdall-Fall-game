using System;
using UnityEngine;

namespace Assets.Source.InputService.Scripts
{
    public interface ICharacterInput
    {
        event Action<Vector2> Moving;
        event Action<Vector2> AlternativePointerMoving;
        event Action Jumping;
        event Action<int> Attacking;
        event Action Killing;
    }
}