using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    internal sealed class GravityGunStateDrag : GravityGunState
    {
        internal GravityGunStateDrag(GravityGunHandler handler) : base(handler) { }

        internal override void Enter() => GrabbedObject.useGravity = false;
        internal override void FixedTick() => _handler.GraviGun.Drag(GrabbedObject, _handler.DragPoint);
        internal override void OnDoubleClick() => _handler.SetState(_handler.Idle);
        internal override void OnLongClick(Vector3 position) => _handler.SetState(_handler.Throw);
        internal override void Exit() => GrabbedObject.useGravity = true;
    }
}