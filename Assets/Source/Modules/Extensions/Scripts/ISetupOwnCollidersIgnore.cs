using UnityEngine;
using System.Collections.Generic;

namespace Assets.Source.Extensions.Scripts
{
    public interface ISetupOwnCollidersIgnore
    {
        public void SetIgnoreOwnCollisions(IReadOnlyList<Collider2D> selfColliders, bool ignore);
    }
}
