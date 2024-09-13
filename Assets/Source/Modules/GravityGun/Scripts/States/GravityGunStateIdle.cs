using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    internal sealed class GravityGunStateIdle : GravityGunStateIdleBase
    {
        internal GravityGunStateIdle(GravityGunHandler handler) : base(handler) { }

        internal override void Enter() => _handler.SetObject(null);

        internal override void OnLongClick(Vector3 position)
        {
            if (TryGetGrabbableObject(position, out IGrabbable grabbedObject))
            {
                _handler.SetObject(grabbedObject);
                _handler.SetState(_handler.Drag);
            }
        }
    }
}