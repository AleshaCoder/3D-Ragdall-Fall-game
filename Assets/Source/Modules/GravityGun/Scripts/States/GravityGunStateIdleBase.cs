using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    internal abstract class GravityGunStateIdleBase : GravityGunState
    {
        protected GravityGunStateIdleBase(GravityGunHandler handler) : base(handler) { }

        protected bool TryGetGrabbableObject(Vector3 position, out IGrabbable grabbedObject)
        {
            grabbedObject = null;

            if (Physics.Raycast(_handler.Camera.ScreenPointToRay(position), out RaycastHit hit, _handler.Range))
                if (hit.collider.TryGetComponent(out grabbedObject))
                    return true;

            return false;
        }
    }
}