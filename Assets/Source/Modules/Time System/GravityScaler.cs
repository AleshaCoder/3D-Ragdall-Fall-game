using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeSystem
{
    [Serializable]
    public class GravityScaler
    {
        [field: SerializeField] public Vector3 Gravity { get; private set; } = Physics.gravity;

        public void Scale(float scale)
        {
            Physics.gravity = Gravity * scale;
        }
    }
}
