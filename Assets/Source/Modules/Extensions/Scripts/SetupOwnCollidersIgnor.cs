using UnityEngine;
using System.Collections.Generic;

namespace Assets.Source.Extensions.Scripts
{
    public class SetupOwnCollidersIgnor : ISetupOwnCollidersIgnore
    {
        public void SetIgnoreOwnCollisions(IReadOnlyList<Collider2D> selfColliders, bool ignore)
        {
            for (int i = 0; i < selfColliders.Count; i++)
                for (int k = i + 1; k < selfColliders.Count; k++)
                    Physics2D.IgnoreCollision(selfColliders[i], selfColliders[k], ignore);
        }
    }
}
