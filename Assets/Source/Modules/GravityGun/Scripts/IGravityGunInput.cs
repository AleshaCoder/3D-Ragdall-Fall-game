using System;
using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    public interface IGravityGunInput
    {
        public event Action<Vector3> DoubleClicked;
        public event Action<Vector3> LongClicked;
    }
}